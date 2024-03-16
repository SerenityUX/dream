using Normal.Realtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class EnvironmentStateModel
{
    [RealtimeProperty(1, true, true)]
    private RealtimeArray<CoinModel> _coins;

    [RealtimeProperty(2, true, true)]
    private RealtimeArray<PowerUpModel> _powerUps;

    public void AddCoin(Vector3 position, string type)
    {
        CoinModel coin = new CoinModel();
        coin.position = position;
        coin.type = type;
        coin.active = true;

        _coins.Add(coin);
    }

    public void AddPowerUp(Vector3 position, string type)
    {
        PowerUpModel powerUp = new PowerUpModel();
        powerUp.position = position;
        powerUp.type = type;
        powerUp.active = true;

        _powerUps.Add(powerUp);
    }

    public void RemoveCoin(int index)
    {
        // Set to inactive instead of removing to avoid index issues
        _coins[index].active = false;
    }

    public void RemovePowerUp(int index)
    {
        // Set to inactive instead of removing to avoid index issues
        _powerUps[index].active = false;
    }

    public RealtimeArray<CoinModel> getCoins()
    {
        return _coins;
    }

    public RealtimeArray<PowerUpModel> getPowerUps()
    {
        return _powerUps;
    }
}
