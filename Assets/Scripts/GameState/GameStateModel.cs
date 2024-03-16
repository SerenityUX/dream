using Normal.Realtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class GameStateModel
{
    [RealtimeProperty(1, true, false)]
    private RealtimeDictionary<PlayerStateModel> _playerstates;

    // 0 = waiting, 1 = playing, 2 = finished
    [RealtimeProperty(2, true, false)]
    private int _gameState;

    [RealtimeProperty(3, true, false)]
    private double _startTime;

    public string EnterPlayer(uint playerID, string playerName)
    {
        if (!_playerstates.ContainsKey(playerID))
        {
            var newPlayerState = new PlayerStateModel
            {
                health = 100,
                laps = 0,
                points = 0,
                name = playerName,
                powerUpType = 0,
                statusEffectType = 0,
                statusEffectDuration = 0,
                ready = false
            };
            if (_playerstates.Count == 0)
            {
                newPlayerState.first = true;
            }
            else
            {
                newPlayerState.first = false;
            }
            _playerstates.Add(playerID, newPlayerState);
            return "Player added.";
        }
        else
        {
            Debug.LogError("Player already exists.");
            return "Player already exists.";
        }
    }

    public void AddPoints(uint playerID, int points)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            playerState.points += points;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void AddLap(uint playerID)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            playerState.laps++;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void TakeDamage(uint playerID, int damage)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            playerState.health -= damage;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void AddStatusEffect(uint playerID, int effectType, float duration)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            playerState.statusEffectType = effectType;
            playerState.statusEffectDuration = duration;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void AddPowerUp(uint playerID, int powerUpType)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            playerState.powerUpType = powerUpType;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void UsePowerUp(uint playerID)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            playerState.powerUpType = 0;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public void ExitPlayer(uint playerID)
    {
        if (_playerstates.ContainsKey(playerID))
        {
            _playerstates.Remove(playerID);
        }
        else
        {
            Debug.LogError("Invalid player ID.");
        }
    }

    public PlayerStateModel GetPlayerState(uint playerID)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
        {
            return playerState;
        }
        else
        {
            Debug.LogError("Invalid player ID.");
            return null;
        }
    }
}
