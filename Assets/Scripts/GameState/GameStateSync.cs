using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

public class GameStateSync : RealtimeComponent<GameStateModel>
{
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
        // Utilize the model's method
        model.UpdateStatusEffects();
    }

    public RealtimeDictionary<PlayerStateModel> GetAllPlayerStates()
    {
        return model.playerstates;
    }
}
