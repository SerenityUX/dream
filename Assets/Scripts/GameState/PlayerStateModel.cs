//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Normal.Realtime.Serialization; // Ensure you have this using directive for RealtimeModel and RealtimeProperty attributes

//public enum PowerUpType { GreenShell, RedShell, Banana, Star }
//public enum StatusEffectType { Silenced, Invincible, Blind, HalfPoints, DoublePoints }



//[RealtimeModel]
//public partial class PlayerModel
//{
//    [RealtimeProperty(1, true, true)]
//    private int _laps;

//    [RealtimeProperty(2, true, true)]
//    private int _points;

//    [RealtimeProperty(3, true, true)]
//    private int _health;


//    [RealtimeProperty(5, true, true)]
//    private int _powerUpType;

//    [RealtimeProperty(6, true, true)]
//    private int _statusEffectType;
//    [RealtimeProperty(7, true, true)]
//    private float _statusEffectDuration;

//    // Properties to access laps, points, and health
//    public int laps
//    {
//        get { return _laps; }
//        set { _laps = value; }
//    }

//    public int points
//    {
//        get { return _points; }
//        set { _points = value; }
//    }

//    public int health // Getter for health
//    {
//        get { return _health; }
//        set { _health = value; }
//    }

//    public int powerUpType
//    {
//        get { return _powerUpType; }
//        set { _powerUpType = value; }
//    }

//    public int statusEffectType
//    {
//        get { return _statusEffectType; }
//        set { _statusEffectType = value; }
//    }

//    public float statusEffectDuration
//    {
//        get { return _statusEffectDuration; }
//        set { _statusEffectDuration = value; }
//    }
//}
