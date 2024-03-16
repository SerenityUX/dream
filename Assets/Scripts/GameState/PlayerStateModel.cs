using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime.Serialization; // Ensure you have this using directive for RealtimeModel and RealtimeProperty attributes

public enum PowerUpType { GreenCarrot, RedCarrot, CarrotPeels, GoldenCarrot }
public enum StatusEffectType { Silenced, Invincible, Blind, HalfPoints, DoublePoints }



[RealtimeModel]
public partial class PlayerStateModel
{
    [RealtimeProperty(1, true, false)]
    private int _laps;

    [RealtimeProperty(2, true, false)]
    private int _points;

    [RealtimeProperty(3, true, false)]
    private int _health;

    [RealtimeProperty(4, true, false)]
    private string _name;


    [RealtimeProperty(5, true, false)]
    private int _powerUpType;

    [RealtimeProperty(6, true, false)]
    private int _statusEffectType;
    [RealtimeProperty(7, true, false)]
    private float _statusEffectDuration;

    [RealtimeProperty(8, true, false)]
    private bool _first;

    [RealtimeProperty(9, true, false)]
    private bool _ready;

    [RealtimeProperty(10, true, false)]
    private float _statusEffectStartTime;
}
