using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    public GameObject robber;

    public CatanHexagon currentlyOn;

    public void putRobber(CatanHexagon catanHexagon)
    {
        currentlyOn.robber = false;
        currentlyOn = catanHexagon;
        currentlyOn.robber = true;
    }
}
