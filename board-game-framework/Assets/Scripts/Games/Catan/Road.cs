﻿using System.Collections;
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
    public CatanPlayer player = null;


    public Crossroads GetOppositeCrossroad(Crossroads crossroad)
    {
        if (crossroad == crossRoad1)
            return crossRoad2;
        else
            return crossRoad1;
    }

    public bool BuildRoad(CatanPlayer player)
    {
        if (CanBuild(player))
        {
            player.DeductResources(1, 1, 0, 0, 0);
            occupied = true;
            this.player = player;
            GameObject r = Instantiate(roadPrefab, transform);
            r.GetComponent<SetupRoad>().Setup(player.GetId());
            player.roads++;
            if (player.HasFreeRoads())
            {
                player.freeRoads--;
            }
            return true;
        }
        return false;
    }

    public bool CanBuild(CatanPlayer player)
    {
        return (!occupied &&
                (player.CanFreeBuild() || player.HasFreeRoads() || player.CanAfford(1, 1, 0, 0, 0)) &&
                (crossRoad1.HaveConnectedRoad(player) || crossRoad2.HaveConnectedRoad(player) || crossRoad1.GetPlayer()==player || crossRoad2.GetPlayer()==player));
    }

}
