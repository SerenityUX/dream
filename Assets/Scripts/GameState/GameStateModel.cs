using Normal.Realtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class GameStateModel
{
    [RealtimeProperty(1, true, true)]
    private RealtimeArray<PlayerStateModel> _playerStates;
    // This list simulates a dictionary's keys for player IDs
    [RealtimeProperty(2, true, true)]
    private RealtimeArray<IntModel> _playerIDs;

    public RealtimeArray<PlayerStateModel> getPlayerStates()
    {
        return _playerStates;
    }

    public RealtimeArray<IntModel> getPlayerIDs()
    {
        return _playerIDs;
    }

    public string EnterPlayer(int playerID, string playerName)
    {

        if (!ContainsPlayerID(playerID))
        {
            IntModel playerIdModel = new IntModel();
            playerIdModel.value = playerID;
            var newPlayerState = new PlayerStateModel();
            newPlayerState.health = 100;
            newPlayerState.laps = 0;
            newPlayerState.points = 0;
            newPlayerState.name = playerName;
            newPlayerState.powerUpType = 0;
            newPlayerState.statusEffectType = 0;
            newPlayerState.statusEffectDuration = 0;
            _playerStates.Add(newPlayerState);
            _playerIDs.Add(playerIdModel);
            return "Player added.";
        }
        else
        {
            Debug.LogError("Player already exists.");
            return "Player already exists.";
        }
    }

    public void AddPoints(int playerID, int points)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            _playerStates[index].points += points;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void AddLap(int playerID)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            _playerStates[index].laps++;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void TakeDamage(int playerID, int damage)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            _playerStates[index].health -= damage;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void AddStatusEffect(int playerID, int effectType, float duration)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            _playerStates[index].statusEffectType = effectType;
            _playerStates[index].statusEffectDuration = duration;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void AddPowerUp(int playerID, int powerUpType)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            _playerStates[index].powerUpType = powerUpType;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void UsePowerUp(int playerID)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            _playerStates[index].powerUpType = 0; // Assuming 0 signifies 'no power-up'
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void ExitPlayer(int playerID)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            // Create a new realtimearray and remove the player from the list
            RealtimeArray<PlayerStateModel> newPlayerStates = new RealtimeArray<PlayerStateModel>();
            RealtimeArray<IntModel> newPlayerIDs = new RealtimeArray<IntModel>();

            for (int i = 0; i < _playerStates.Count; i++)
            {
                if (i != index)
                {
                    newPlayerStates.Add(_playerStates[i]);
                    newPlayerIDs.Add(_playerIDs[i]);
                }
            }

            _playerStates = newPlayerStates;
            _playerIDs = newPlayerIDs;

        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }


    // This method decrements the duration of status effects for all players
    public void UpdateStatusEffects()
    {
        for (int i = 0; i < _playerStates.Count; i++)
        {
            if (_playerStates[i].statusEffectDuration > 0)
            {
                // Decrement the status effect duration
                _playerStates[i].statusEffectDuration -= Time.deltaTime;

                // Check if the status effect has expired
                if (_playerStates[i].statusEffectDuration <= 0)
                {
                    // Reset status effect type and duration
                    _playerStates[i].statusEffectType = 0; // Assuming 0 signifies 'no effect'
                    _playerStates[i].statusEffectDuration = 0;
                }
            }
        }
    }

    public bool ContainsPlayerID(int playerID)
    {
        foreach (IntModel intModel in _playerIDs)
        {
            if (intModel.value == playerID) // Assuming `value` is the public property in IntModel
            {
                return true;
            }
        }
        return false;
    }

    public int GetPlayerIndex(int playerID)
    {
        for (int i = 0; i < _playerIDs.Count; i++)
        {
            if (_playerIDs[i].value == playerID)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetPlayerState(int playerID)
    {
        int index = GetPlayerIndex(playerID);
        if (index != -1)
        {
            return _playerStates[index];
        }
        else
        {
            Debug.LogError("Player not found.");
            return -1;
        }
    }

    public RealtimeArray<PlayerStateModel> getAllPlayerStates()
    {
        return _playerStates;
    }

    public RealtimeArray<IntModel> getAllPlayerIDs()
    {
        return _playerIDs;
    }

}
