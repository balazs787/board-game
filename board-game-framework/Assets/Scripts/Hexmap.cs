using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexmap : MonoBehaviour
{
    public List<CatanHexagon> hexagons;

    public void DistributeResources(int diceRoll)
    {
        foreach (var hex in hexagons)
        {
            if (hex.getNumber() == diceRoll)
            {
                hex.Activate();
            }
        }
    }
}
