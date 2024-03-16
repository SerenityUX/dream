//using UnityEngine;
//using Normal.Realtime;
//using Normal.Realtime.Serialization;

//public class GameStateSync : RealtimeComponent<GameStateModel>
//{

//    private ScoreDisplay _scoreDisplay;
//    // private LeaderboardDisplay _leaderboardDisplay;

//    protected override void OnRealtimeModelReplaced(GameStateModel previousModel, GameStateModel currentModel)
//    {
//        if (previousModel != null)
//        {
//            previousModel.playerStatesDidChange -= PlayerStateDidChange;
//        }

//        if (currentModel != null)
//        {
//            if (currentModel.isFreshModel)
//            {
//                // If Model has no data, we update with local data

//            }

//            UpdateLeaderboard();
//            UpdatePersonal();

//            // Initialize the model
//            currentModel.playerStatesDidChange += PlayerStateDidChange;
//        }
//    }

//    // Public method to interact with the GameStateModel
//    public string EnterPlayer(string name)
//    {
//        int playedID = Realtime.clientID();
//        return model.EnterPlayer(playerID, name);
//    }

//    public void ExitPlayer()
//    {
//        int playedID = Realtime.clientID();
//        model.ExitPlayer(playerID);
//    }

//    public void AddPointsToPlayer(int points)
//    {
//        int playedID = Realtime.clientID();
//        // Utilize the model's method
//        model.AddPoints(playerID, points);
//    }

//    // Example method to add laps to a player
//    public void AddLapToPlayer()
//    {
//        int playedID = Realtime.clientID();
//        // Utilize the model's method
//        model.AddLap(playerID);
//    }

//    public void TakeDamage(int damage)
//    {
//        int playedID = Realtime.clientID();
//        // Utilize the model's method
//        model.TakeDamage(playerID, damage);
//    }

//    public void GetPowerUp(int powerUpType)
//    {
//        int playedID = Realtime.clientID();
//        // Check if the user has a powerup
//        if (model.playerStates[model.GetPlayerIndex(playerID)].powerUpType == 0)
//        {
//            // Utilize the model's method
//            model.AddPowerUp(playerID, powerUpType);
//        }
//    }
//    public void InflictStatusEffect(int effectType, float duration)
//    {
//        int playedID = Realtime.clientID();
//        // Utilize the model's method
//        model.AddStatusEffect(playerID, effectType, duration);
//    }

//    private void PlayerStateDidChange(PlayerStateModel model, RealtimeArray<PlayerStateModel> playerStates)
//    {
//        // Do something with the player state, eg render leaderboard
//        UpdateLeaderboard();
//        UpdatePersonal();
//    }

//    private void UpdateLeaderboard()
//    {
//        // Update the canvas
//    }

//    private void UpdatePersonal()
//    {
//        _scoreDisplay.setScore(model.GetPlayerState(Realtime.clientID()).points);
//    }

//    public RealtimeArray<PlayerStateModel> GetAllPlayerStates()
//    {
//        return model.getAllPlayerStates();
//    }

//    public RealtimeArray<IntModel> GetAllPlayerIDs()
//    {
//        return model.getAllPlayerIDs();
//    }

//    public PlayerStateModel GetPlayerState(int playerID)
//    {
//        int index = model.GetPlayerIndex(playerID); // Assuming you implement this method in your model
//        if (index != -1)
//        {
//            return model.playerStates[index];
//        }
//        else
//        {
//            Debug.LogError("Player not found.");
//            return null;
//        }
//    }


//    public void UpdateStatusEffects()
//    {
//        // Utilize the model's method
//        model.UpdateStatusEffects();
//    }
//}
