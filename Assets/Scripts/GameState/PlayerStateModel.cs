using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime.Serialization; // Ensure you have this using directive for RealtimeModel and RealtimeProperty attributes

public enum PowerUpType { GreenCarrot, RedCarrot, CarrotPeels, GoldenCarrot }
public enum StatusEffectType { Silenced, Invincible, Blind, HalfPoints, DoublePoints }



[RealtimeModel]
public partial class PlayerStateModel
{
    [RealtimeProperty(1, true, true)]
    private int _laps;

    [RealtimeProperty(2, true, true)]
    private int _points;

    [RealtimeProperty(3, true, true)]
    private int _health;

    [RealtimeProperty(4, true, true)]
    private string _name;


    [RealtimeProperty(5, true, true)]
    private int _powerUpType;

    [RealtimeProperty(6, true, true)]
    private int _statusEffectType;
    [RealtimeProperty(7, true, true)]
    private float _statusEffectDuration;
}
