using Normal.Realtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class IntModel
{
    [RealtimeProperty(1, true)]
    private int _value;
}
