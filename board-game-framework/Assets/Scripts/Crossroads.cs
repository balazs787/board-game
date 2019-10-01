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
    public GameObject currentBuilding;
    bool city = false;

    public bool BuildSettlement(Player player)
    {
        if (CanBuild(this, player))
        {
            occupied = true;
            this.player = player;
            GameObject s = Instantiate(settlementPrefab, transform);
            s.GetComponent<BuildSettlement>().Setup(player.GetId());
            currentBuilding = s;
            player.AddVictoryPoint();
            player.settlements++;
            return true;
        }
        return false;
    }

    public bool UpgradeSettlement(Player player)
    {
        if (this.player == player && !city && player.DeductResources(0, 0, 2, 0, 3))
        {
            Destroy(currentBuilding);
            GameObject t = Instantiate(townPrefab, transform);
            t.GetComponent<BuildTown>().Setup(player.GetId());
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
                (player.FreeBuild() ||
                (HaveConnectedRoad(player) && player.DeductResources(1, 1, 1, 1, 0))));
    }

    public void CrossroadsGiveResources(Resource resource)
    {
        if (occupied)
        {
            if (city)
            {
                player.GivePlayerResources(resource, 2);
            }
            else
            {
                player.GivePlayerResources(resource, 1);
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

    
    public bool HaveConnectedRoad(Player player)
    {
        return (road1?.player == player || road2?.player == player || road3?.player == player);
    }
}
