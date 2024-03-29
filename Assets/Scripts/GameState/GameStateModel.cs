using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;


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
        Debug.Log(playerID);
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
            Debug.Log("Player already exists.");
            return "Player already exists.";
        }
    }

    public void AddPoints(uint playerID, int points)
    {
        if (_playerstates.TryGetValue(playerID, out PlayerStateModel playerState))
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
            if ((StatusEffectType)playerState.statusEffectType != StatusEffectType.Invincible)
            {
                playerState.health -= damage;
            }

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
            if ((StatusEffectType)playerState.statusEffectType == StatusEffectType.Silenced)
            {
                return;
            }
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
            if ((StatusEffectType)playerState.statusEffectType != StatusEffectType.Silenced)
            {
                playerState.powerUpType = 0;
            }

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
