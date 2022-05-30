using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fuel Data", menuName = "New Fuel")]
public class FuelData : ScriptableObject
{
    public float currentFuel;
    public float maxFuel = 1.0f;
    [Range(0.00f,2.00f)] public float addFuel;
    [Range(0.00f,0.09f)] public float usedRate;

}
