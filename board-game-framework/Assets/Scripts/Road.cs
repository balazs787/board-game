using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefab;

    public Crossroads crossRoad1;
    public Crossroads crossRoad2;

    public HexagonField hex1;
    public HexagonField hex2;

    bool occupied = false;
    public Player player = null;


    public Crossroads GetOppositeCrossroad(Crossroads crossroad)
    {
        if (crossroad == crossRoad1)
            return crossRoad2;
        else
            return crossRoad1;
    }

    public bool BuildRoad(Player player)
    {
        if (!occupied && player.DeductResources(1, 1, 0, 0, 0))
        {
            occupied = true;
            this.player = player;
            GameObject r = Instantiate(roadPrefab, transform);
            r.GetComponent<BuildRoad>().Setup(player.GetId());
            return true;
        }
        return false;
    }

}
