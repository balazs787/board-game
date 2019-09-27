using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject road;

    public Crossroads crossRoad1;
    public Crossroads crossRoad2;

    public HexagonField hex1;
    public HexagonField hex2;

    bool occupied = false;
    Player player = null;


    public Crossroads GetOppositeCrossroad(Crossroads crossroad)
    {
        if (crossroad == crossRoad1)
            return crossRoad2;
        else
            return crossRoad1;
    }

    public void BuildRoad(Player player)
    {
        if (!occupied)
        {
            occupied = true;
            this.player = player;
        }
    }

}
