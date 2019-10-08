using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public Resources resources = new Resources();
    public int victoryPoints;
    public Color color;
    public string playerName;
    public int id;
    public int freeBuilds = 4;
    public int roads = 0;
    public int settlements = 0;
    public int knights = 0;
    public Dictionary<Resource, int> rs;

    private bool _needRefresh;

    private void Awake()
    {
        rs = new Dictionary<Resource, int>() {
            { Resource.lumber, resources.lumber },
            { Resource.brick, resources.brick },
            { Resource.grain, resources.grain },
            { Resource.wool, resources.wool },
            { Resource.ore, resources.ore },
            { Resource.none, 0 } };
    }
    private void Start()
    {
        


        //TODO: delete this
        GivePlayerResources(Resource.brick, 10);
        GivePlayerResources(Resource.lumber, 10);
        GivePlayerResources(Resource.wool, 10);
        GivePlayerResources(Resource.grain, 10);
        GivePlayerResources(Resource.ore, 10); 
    }

    


    public void GivePlayerResources(Resource resource, int amount)
    {

        rs[resource]+= amount;
        _needRefresh = true;
    }

    public void Trade(string giveType, int giveAmount, string getType, int getAmount)
    {
        Enum.TryParse(giveType, out Resource resourceGive);
        GivePlayerResources(resourceGive, -giveAmount);

        Enum.TryParse(getType, out Resource resourceGet);
        GivePlayerResources(resourceGet, getAmount);

        _needRefresh = true;
    }

    public int SevenRoll()
    {
        var currentResources = resources.lumber + resources.brick + resources.wool + resources.grain + resources.ore;
        if (currentResources > 7)
        {
            return currentResources / 2;
        }
        return 0;
    }

    public void DropResources(int amount)
    {
        
    }

    public bool CanAfford(int l, int b, int g, int w, int o)
    {
        if ((resources.lumber - l >= 0) &&
           (resources.brick - b >= 0) &&
           (resources.grain - g >= 0) &&
           (resources.wool - w >= 0) &&
           (resources.ore - o >= 0))
        {
            return true;
        }
        return false;
    }

    public bool CanAfford(Resource resource, int amount)
    {
        return rs[resource] >= amount;
    }

    public void DeductResources(int l, int b, int g, int w, int o)
    {
        if (freeBuilds > 0)
        {
            freeBuilds--;
            return;
        }

        resources.lumber -= l;
        resources.brick -= b;
        resources.grain -= g;
        resources.wool -= w;
        resources.ore -= o;

        _needRefresh = true;
    }

    public void DeductOneResource(Resource resource)
    {
        GivePlayerResources(resource, -1);
    }

    public void GivePlayerRandomResource(Player player)
    {
        if (resources.lumber + resources.brick + resources.wool + resources.grain + resources.ore == 0)
        {
            return;
        }

        int randomInt = UnityEngine.Random.Range(0, 5);
        while(!CanAfford((Resource)randomInt, 1))
        {
            randomInt = UnityEngine.Random.Range(0, 5);
        }

        DeductOneResource((Resource)randomInt);
        player.GivePlayerResources((Resource)randomInt, 1);
    }

    public void AddVictoryPoint()
    {
        victoryPoints++;
        _needRefresh = true;
    }

    public int GetVictoryPoints()
    {
        return victoryPoints;
    }
    public int GetId()
    {
        return id;
    }

    public bool NeedRefresh()
    {
        if (_needRefresh)
        {
            _needRefresh = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanFreeBuild()
    {
        return freeBuilds > 0;
    }
}
