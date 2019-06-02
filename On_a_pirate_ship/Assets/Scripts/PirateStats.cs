using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateStats : ScriptableObject
{
    public Dictionary<string, int> pirateStatistics = new Dictionary<string, int>()
    {
        {"Food", 50},
        {"Sanity", 50},
        {"Health", 50}
    };
}
