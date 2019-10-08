﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroads : MonoBehaviour
{
    public GameObject settlementPrefab;
    public GameObject townPrefab;

    public Road road1;
    public Road road2;
    public Road road3;

    public HexagonField hex1;
    public HexagonField hex2;
    public HexagonField hex3;

    private bool occupied = false;
    private Player _player = null;
    public GameObject currentBuilding;
    bool city = false;

    public bool BuildSettlement(Player player)
    {
        if (CanBuild(this, player))
        {
            player.DeductResources(1, 1, 1, 1, 0);
            occupied = true;
            _player = player;
            GameObject s = Instantiate(settlementPrefab, transform);
            s.GetComponent<SetupSettlement>().Setup(player.GetId());
            currentBuilding = s;
            player.AddVictoryPoint();
            if (player.settlements == 0)
            {
                player.GivePlayerResources(hex1.gameObject.GetComponent<CatanHexagon>().resource, 1);
                player.GivePlayerResources(hex2.gameObject.GetComponent<CatanHexagon>().resource, 1);
                player.GivePlayerResources(hex3.gameObject.GetComponent<CatanHexagon>().resource, 1);
            }
            player.settlements++;
            return true;
        }
        return false;
    }

    public bool UpgradeSettlement(Player player)
    {
        if (_player == player && !city && player.CanAfford(0, 0, 2, 0, 3))
        {
            Destroy(currentBuilding);
            player.DeductResources(0, 0, 2, 0, 3);
            GameObject t = Instantiate(townPrefab, transform);
            t.GetComponent<SetupTown>().Setup(player.GetId());
            player.AddVictoryPoint();
            city = true;
            return true;
        }
        return false;
    }

    public bool CanBuild(Crossroads cr, Player player)
    {
        bool neighbouringCrossroadsFree = ((road1 == null || !road1.GetOppositeCrossroad(cr).GetOccupied()) &&
            (road2 == null || !road2.GetOppositeCrossroad(cr).GetOccupied()) &&
            (road3 == null || !road3.GetOppositeCrossroad(cr).GetOccupied()));

        return (!occupied && neighbouringCrossroadsFree &&
                (player.CanFreeBuild() ||
                (HaveConnectedRoad(player) && player.CanAfford(1, 1, 1, 1, 0))));
    }

    public void CrossroadsGiveResources(Resource resource)
    {
        if (occupied)
        {
            if (city)
            {
                _player.GivePlayerResources(resource, 2);
            }
            else
            {
                _player.GivePlayerResources(resource, 1);
            }
        }
    }

    public bool StealResource(Player player)
    {
        if(_player==null || _player == player)
        {
            return false;
        }

        _player.GivePlayerRandomResource(player);
        return true;
    }

    public bool GetOccupied()
    {
        return occupied;
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public bool GetCity()
    {
        return city;
    }

    
    public bool HaveConnectedRoad(Player player)
    {
        return road1?.player == player || road2?.player == player || road3?.player == player;
    }
}
