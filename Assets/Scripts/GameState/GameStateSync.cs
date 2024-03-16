using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using System.Collections.Generic;
using System;

public class GameStateSync : RealtimeComponent<GameStateModel>
{
    private void Start()
    {
        Invoke("EnterPlayerDelayed", 3f); // Calls EnterPlayerDelayed after 5 seconds
    }

    private void EnterPlayerDelayed()
    {
        EnterPlayer("Bill");
        AddPointsToPlayer(1);

    }
    // Update function, called once per frame
    private void Update()
    {
        if (!realtime.connected)
        {
            return;
        }

        if (model.gameState == 0 || model.gameState == 2)
        {
            return;
        }

        if (Time > 63)
        {
            model.gameState = 2;
        }

        // Only the first user can update the status effects
        if (model.playerstates[(uint)realtime.clientID].first)
        {
            UpdateStatusEffects();
        }

        // Get all the player states
        RealtimeDictionary<PlayerStateModel> playerStates = GetAllPlayerStates();

        // Get current player's ID

    }

    protected override void OnRealtimeModelReplaced(GameStateModel previousModel, GameStateModel currentModel)
    {
        if (currentModel.isFreshModel)
        {
            currentModel.gameState = 0;
        }
    }

    public float Time
    {
        get
        {
            // Return 0 if we're not connected to the room yet.
            if (model == null) return 0.0f;

            // Make sure the stopwatch is running
            if (model.startTime == 0.0) return 0.0f;

            // Calculate how much time has passed
            return (float)(realtime.room.time - model.startTime);
        }
    }

    public void ReadyUp()
    {
        uint playerID = (uint)realtime.clientID;
        model.playerstates[playerID].ready = true;

        // Check if all players are ready
        bool allReady = true;

        var enumerator = model.playerstates.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentPlayer = enumerator.Current;
            // currentPlayer.Key is the uint ID, currentPlayer.Value is the PlayerStateModel
            var playerState = currentPlayer.Value;

            if (!playerState.ready)
            {
                allReady = false;
                break;
            }
        }

        if (allReady)
        {
            // Start the game
            model.gameState = 1;
            model.startTime = realtime.room.time;
        }

    }

    // Public method to interact with the GameStateModel
    public string EnterPlayer(string name)
    {
        Debug.Log(realtime.clientID);

        if (realtime.clientID < 0)
        {
            return null;
        }
        uint playerID = (uint)realtime.clientID;

        return model.EnterPlayer(playerID, name);
    }

    public void ExitPlayer()
    {
        uint playerID = (uint)realtime.clientID;
        model.ExitPlayer(playerID);
    }

    public void AddPointsToPlayer(int points)
    {
        uint playerID = (uint)realtime.clientID;
        // Utilize the model's method
        Debug.Log(realtime.clientID);
        if (model.playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            if ((StatusEffectType)playerState.statusEffectType == StatusEffectType.DoublePoints)
            {
                playerState.points += points * 2;
            }
            else if ((StatusEffectType)playerState.statusEffectType == StatusEffectType.HalfPoints)
            {
                playerState.points += points / 2;
            }
            else
            {
                playerState.points += points;
            }
        }
        else
        {
            Debug.Log("Invalid player ID.");
            Debug.Log(playerID);
        }
    }

    // Example method to add laps to a player
    public void AddLapToPlayer()
    {
        // Utilize the model's method
        uint playerID = (uint)realtime.clientID;
        model.AddLap(playerID);
    }

    public void TakeDamage(int damage)
    {
        // Utilize the model's method
        uint playerID = (uint)realtime.clientID;
        model.TakeDamage(playerID, damage);
    }

    public void GainPowerUp(int powerUpType)
    {
        // Utilize the model's method
        uint playerID = (uint)realtime.clientID;
        model.AddPowerUp(playerID, powerUpType);
    }

    public void AddStatusEffect(int effectType, float duration)
    {
        uint playerID = (uint)realtime.clientID;
        if (model.playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            var CastedType = (StatusEffectType)effectType;
            if (CastedType == StatusEffectType.DoublePoints || CastedType == StatusEffectType.Invincible)
            {
                playerState.statusEffectType = effectType;
                playerState.statusEffectDuration = duration;
                playerState.statusEffectStartTime = (float)realtime.room.time;
                return;
            }
            else if ((StatusEffectType)playerState.statusEffectType == StatusEffectType.Invincible)
            {
                return;
            }

            playerState.statusEffectType = effectType;
            playerState.statusEffectDuration = duration;
            playerState.statusEffectStartTime = (float)realtime.room.time;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void UpdateStatusEffects()
    {
        var enumerator = model.playerstates.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentPlayer = enumerator.Current;
            // currentPlayer.Key is the uint ID, currentPlayer.Value is the PlayerStateModel
            var playerState = currentPlayer.Value;

            if (playerState.statusEffectType != 0)
            {
                var timePassed = (float)(realtime.room.time - playerState.statusEffectStartTime);

                if (timePassed > playerState.statusEffectDuration)
                {
                    playerState.statusEffectType = 0;
                    playerState.statusEffectDuration = 0;
                }
            }
        }
    }

    public RealtimeDictionary<PlayerStateModel> GetAllPlayerStates()
    {


        return model.playerstates;
    }

    public List<Tuple<string, int>> GetPlayerNamesAndPoints()
    {
        List<Tuple<string, int>> playerNamesAndPoints = new List<Tuple<string, int>>();

        var enumerator = model.playerstates.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentPlayer = enumerator.Current;
            // currentPlayer.Key is the uint ID, currentPlayer.Value is the PlayerStateModel
            var playerState = currentPlayer.Value;

            playerNamesAndPoints.Add(new Tuple<string, int>(playerState.name, playerState.points));
        }

        // Sort so the player with the most points is first
        playerNamesAndPoints.Sort((x, y) => y.Item2.CompareTo(x.Item2));

        return playerNamesAndPoints;
    }

    public int GetPlayerPoints()
    {
        uint playerID = (uint)realtime.clientID;
        if (model.playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            return playerState.points;
        }
        else
        {
            Debug.Log(realtime.clientID);
            return -1;
        }
    }
}
