using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using System.Collections.Generic;
using System.IO;

public class EnvironmentSync : RealtimeComponent<EnvironmentStateModel>
{
    [SerializeField] private string environmentDataPath = "Path/To/Your/JSON.json";

    public void LoadJson()
    {
        LoadEnvironmentDataFromJSON();
    }

    private void LoadEnvironmentDataFromJSON()
    {
        string filePath = Path.Combine(Application.dataPath, environmentDataPath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            // Assuming you have defined a structure that matches your JSON
            EnvironmentData data = JsonUtility.FromJson<EnvironmentData>(json);

            foreach (var coinData in data.coins)
            {
                Vector3 position = new Vector3(coinData.Location[0], coinData.Location[1], coinData.Location[2]);
                AddCoin(position, coinData.type);
            }

            foreach (var powerUpData in data.powerUps)
            {
                Vector3 position = new Vector3(powerUpData.Location[0], powerUpData.Location[1], powerUpData.Location[2]);
                AddPowerUp(position, powerUpData.type);
            }
        }
        else
        {
            Debug.LogError("Environment data file not found.");
        }
    }

    private void AddCoin(Vector3 position, string type)
    {
        model.AddCoin(position, type);
    }

    private void AddPowerUp(Vector3 position, string type)
    {
        model.AddPowerUp(position, type);
    }

    public void RemoveCoin(int index)
    {
        model.RemoveCoin(index);
    }

    public void RemovePowerUp(int index)
    {
        model.RemovePowerUp(index);
    }

    public RealtimeArray<CoinModel> GetCoins()
    {
        return model.getCoins();
    }

    public RealtimeArray<PowerUpModel> GetPowerUps()
    {
        return model.getPowerUps();
    }

    // Define the structure to match your JSON data
    [System.Serializable]
    private class EnvironmentData
    {
        public CoinData[] coins;
        public PowerUpData[] powerUps;
    }

    [System.Serializable]
    private class CoinData
    {
        public float[] Location;
        public string type;
    }

    [System.Serializable]
    private class PowerUpData
    {
        public float[] Location;
        public string type;
    }
}
