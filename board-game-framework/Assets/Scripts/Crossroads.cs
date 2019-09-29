using System.Collections;
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
    public Player player = null;
    bool city = false;

    public bool BuildSettlement(Player player)
    {
        if (!occupied && CanBuild(this))
        {
            occupied = true;
            this.player = player;
            GameObject s = Instantiate(settlementPrefab, transform);
            s.GetComponent<BuildSettlement>().Setup(player.GetId());
            return true;
        }
        return false;
    }

    public void UpgradeSettlement(Player player)
    {
        if (this.player == player)
        {
            if (!city)
            {
                city = true;
            }
        }
    }

    public bool CanBuild(Crossroads cr)
    {
        if ((road1 == null || !road1.GetOppositeCrossroad(cr).GetOccupied()) && 
            (road2 == null || !road2.GetOppositeCrossroad(cr).GetOccupied()) && 
            (road3 == null || !road3.GetOppositeCrossroad(cr).GetOccupied()))
        {
                return true;
            }
        return false;
    }

    public void CrossroadsGiveResources(Resource resource)
    {
        if (occupied)
        {
            if (city)
            {
                player.givePlayerResources(resource, 2);
            }
            else
            {
                player.givePlayerResources(resource, 1);
            }
        }
    }

    public bool GetOccupied()
    {
        return occupied;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public bool GetCity()
    {
        return city;
    }

    

}
