using Normal.Realtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class PowerUpModel
{
    [RealtimeProperty(1, true, true)]
    private Vector3 _position;

    [RealtimeProperty(2, true, true)]
    private string _type;

    // Additional properties or methods as needed
}
