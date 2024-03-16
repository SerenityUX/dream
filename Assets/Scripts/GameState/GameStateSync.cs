using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

public class GameStateSync : RealtimeComponent<GameStateModel>
{

    protected override void OnRealtimeModelReplaced(GameStateModel previousModel, GameStateModel currentModel)
    {
        if (previousModel != null)
        {
            // Here, you could unregister from events on the previous model
            // if you had any events to unregister.
        }

        if (currentModel != null)
        {
            // Here, you would register for events on the current model
            // if you have any events to listen to.
        }
    }

    // Public method to interact with the GameStateModel
    public string EnterPlayer(int playerID)
    {
        return model.EnterPlayer(playerID);
    }

    public void ExitPlayer(int playerID)
    {
        model.ExitPlayer(playerID);
    }

    public void AddPointToPlayer(int playerID)
    {
        // Utilize the model's method
        model.AddPoint(playerID);
    }

    // Example method to add laps to a player
    public void AddLapToPlayer(int playerID)
    {
        // Utilize the model's method
        model.AddLap(playerID);
    }

    public void DealDamageToPlayer(int playerID, int damage)
    {
        // Utilize the model's method
        model.TakeDamage(playerID, damage);
    }

    public void GivePlayerPowerUp(int playerID, int powerUpType)
    {
        // Check if the user has a powerup
        if (model.playerStates[model.GetPlayerIndex(playerID)].powerUpType == 0)
        {
            // Utilize the model's method
            model.AddPowerUp(playerID, powerUpType);
        }
    }

    public void AddStatusEffectToPlayer(int playerID, int effectType, float duration)
    {
        // Utilize the model's method
        model.AddStatusEffect(playerID, effectType, duration);
    }

    public RealtimeArray<PlayerStateModel> GetAllPlayerStates()
    {
        return model.getAllPlayerStates();
    }

    public RealtimeArray<IntModel> GetAllPlayerIDs()
    {
        return model.getAllPlayerIDs();
    }

    public PlayerStateModel GetPlayerState(int playerID)
    {
        int index = model.GetPlayerIndex(playerID); // Assuming you implement this method in your model
        if (index != -1)
        {
            return model.playerStates[index];
        }
        else
        {
            Debug.LogError("Player not found.");
            return null;
        }
    }


    public void UpdateStatusEffects()
    {
        // Utilize the model's method
        model.UpdateStatusEffects();
    }
}
