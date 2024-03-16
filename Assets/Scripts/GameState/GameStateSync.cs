using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

public class GameStateSync : RealtimeComponent<GameStateModel>
{

    protected override void OnRealtimeModelReplaced(GameStateModel previousModel, GameStateModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.playerStatesDidChange -= PlayerStateDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                // If Model has no data, we update with local data

            }

            UpdateLeaderboard();
            UpdatePersonal();

            // Initialize the model
            currentModel.playerStatesDidChange += PlayerStateDidChange;
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

    private void PlayerStateDidChange(PlayerStateModel model, RealtimeArray<PlayerStateModel> playerStates)
    {
        // Do something with the player state, eg render leaderboard
        UpdateLeaderboard();
        UpdatePersonal();
    }

    private void UpdateLeaderboard()
    {
        // Update the canvas
    }

    private void UpdatePersonal()
    {
        // Update the personal stats
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
