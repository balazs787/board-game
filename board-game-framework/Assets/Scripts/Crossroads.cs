using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroads : MonoBehaviour
{
    public GameObject crossroads;

    public Road road1;
    public Road road2;
    public Road road3;

    public HexagonField hex1;
    public HexagonField hex2;
    public HexagonField hex3;

    bool occupied = false;
    Player player = null;

    bool city = false;

    public void buildSettlement(Player player)
    {
        if (!occupied)
        {
            if (canBuild(this))
            {
                occupied = true;
                this.player = player;
            }
        }
    }

    public void upgradeSettlement(Player player)
    {
        if (this.player == player)
        {
            if (!city)
            {
                city = true;
            }
        }
    }

    public bool canBuild(Crossroads cr)
    {
        if (!road1.getOppositeCrossroad(cr).getOccupied())
        {
            if (!road2.getOppositeCrossroad(cr).getOccupied())
            {
                if (!road3.getOppositeCrossroad(cr).getOccupied())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool getOccupied()
    {
        return occupied;
    }

    public Player getPlayer()
    {
        return player;
    }

    public bool getCity()
    {
        return city;
    }

    public void crossroadsGiveResources(Resource resource)
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

}
