using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

public class GameStateSync : RealtimeComponent<GameStateModel>
{
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
        uint playerID = (uint)realtime.clientID;
        PlayerStateModel playerState = model.playerstates[playerID];
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
        model.AddPoints(playerID, points);
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

    public void InflictStatusEffect(int effectType, float duration)
    {
        // Utilize the model's method
        uint playerID = (uint)realtime.clientID;
        model.AddStatusEffect(playerID, effectType, duration);
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
}
