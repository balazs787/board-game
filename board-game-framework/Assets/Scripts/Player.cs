using System;
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

    private bool _placingRobber;
    private bool _needRefresh;

    private void Start()
    {
        //TODO: delete this
        GivePlayerResources(Resource.brick, 10);
        GivePlayerResources(Resource.lumber, 10);
        GivePlayerResources(Resource.wool, 10);
        GivePlayerResources(Resource.grain, 10);
        GivePlayerResources(Resource.ore, 10);
    }

    public void GivePlayerResources(Resource? resource, int amount)
    {
        switch (resource)
        {
            case Resource.lumber:
                resources.lumber += amount;
                break;
            case Resource.brick:
                resources.brick += amount;
                break;
            case Resource.grain:
                resources.grain += amount;
                break;
            case Resource.wool:
                resources.wool += amount;
                break;
            case Resource.ore:
                resources.ore += amount;
                break;
            default:
                break;
        }
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

    public void SevenRoll()
    {
        var currentResources = resources.lumber + resources.brick + resources.wool + resources.grain + resources.ore;
        if (currentResources > 7)
        {
            DropResources(currentResources / 2);
        }
    }

    public void DropResources(int amount)
    {

    }

    public void PlaceRobber()
    {
        _placingRobber = true;
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
