using System;
using System.Collections.Generic;
using UnityEngine;

public partial class CatanPlayer : Player
{
    public bool Ai;
    public bool advanced;

    public int victoryPoints;
    public int freeBuilds = 4;
    public int freeRoads = 0;
    public int roads = 0;
    public int settlements = 0;
    public int towns = 0;
    public int knights = 0;
    public bool largestArmy;
    public int longestRoadCount;
    public bool longestRoad;
    public List<ICard> cards = new List<ICard>();
    public Dictionary<Resource, int> Resources;
    public Dictionary<Resource, bool> Tradeables;

    public Action NeedRefreshAction;

    private void Awake()
    {
        Resources = new Dictionary<Resource, int>() {
            { Resource.lumber, 0 },
            { Resource.brick, 0 },
            { Resource.grain, 0 },
            { Resource.wool, 0 },
            { Resource.ore, 0 },
            { Resource.none, 0 }
        };

        Tradeables = new Dictionary<Resource, bool>()
        {
            { Resource.lumber, false },
            { Resource.brick, false },
            { Resource.grain, false },
            { Resource.wool, false },
            { Resource.ore, false },
            { Resource.none, false }
        };
    }


    public void GivePlayerResources(Resource resource, int amount)
    {
        Resources[resource] += amount;
        NeedRefreshAction?.Invoke();
    }

    public void Trade(string giveType, int giveAmount, string getType, int getAmount)
    {
        Enum.TryParse(giveType, out Resource resourceGive);
        GivePlayerResources(resourceGive, -giveAmount);

        Enum.TryParse(getType, out Resource resourceGet);
        GivePlayerResources(resourceGet, getAmount);

        NeedRefreshAction?.Invoke();
    }

    public void BuyCard(Deck deck)
    {
        DeductResources(0, 0, 1, 1, 1);
        cards.Add(deck.GetCard());
    }

    public int SevenRoll()
    {
        return GetResourcesSum() > 7 ? GetResourcesSum() / 2 : 0;
    }


    public bool CanAfford(int l, int b, int g, int w, int o)
    {
        if ((Resources[Resource.lumber] - l >= 0) &&
           (Resources[Resource.brick] - b >= 0) &&
           (Resources[Resource.grain] - g >= 0) &&
           (Resources[Resource.wool] - w >= 0) &&
           (Resources[Resource.ore] - o >= 0))
        {
            return true;
        }
        return false;
    }

    public bool CanAfford(Resource resource, int amount)
    {
        return Resources[resource] >= amount;
    }

    public void DeductResources(int l, int b, int g, int w, int o)
    {
        if (freeBuilds > 0)
        {
            freeBuilds--;
            return;
        }

        Resources[Resource.lumber] -= l;
        Resources[Resource.brick] -= b;
        Resources[Resource.grain] -= g;
        Resources[Resource.wool] -= w;
        Resources[Resource.ore] -= o;

        NeedRefreshAction?.Invoke();
    }

    public bool DeductOneResource(Resource resource)
    {
        if (CanAfford(resource, 1))
        {
            GivePlayerResources(resource, -1);
            return true;
        }

        return false;
    }

    internal void Drop()
    {
        Resources[Resource.lumber] = 0;
        Resources[Resource.brick] = 0;
        Resources[Resource.grain] = 0;
        Resources[Resource.wool] = 0;
        Resources[Resource.ore] = 0;
    }

    public void GivePlayerRandomResource(CatanPlayer player)
    {
        if (GetResourcesSum() == 0)
        {
            return;
        }

        int randomInt = UnityEngine.Random.Range(1, 6);
        while (!CanAfford((Resource)randomInt, 1))
        {
            randomInt = UnityEngine.Random.Range(1, 6);
        }

        DeductOneResource((Resource)randomInt);
        player.GivePlayerResources((Resource)randomInt, 1);
    }

    public void MakeCardsPlayable()
    {
        foreach (var c in cards)
        {
            c.Playable();
        }
    }

    public void AddTradeable(Resource resource)
    {
        Tradeables.Remove(resource);
        Tradeables.Add(resource, true);
    }

    public void AddVictoryPoint()
    {
        victoryPoints++;
        NeedRefreshAction?.Invoke();
    }

    public int GetResourcesSum()
    {
        return Resources[Resource.lumber] + Resources[Resource.brick] + Resources[Resource.wool] + Resources[Resource.grain] + Resources[Resource.ore];
    }

    public int GetVictoryPoints()
    {
        return victoryPoints;
    }

    public bool CanFreeBuild()
    {
        return freeBuilds > 0;
    }

    public bool HasFreeRoads()
    {
        return freeRoads > 0;
    }

    public void SetLargestArmy(bool value)
    {
        if (!value)
        {
            victoryPoints -= 2;
        }
        else
        {
            victoryPoints += 2;
        }
        largestArmy = value;
        NeedRefreshAction?.Invoke();
    }

    public bool GetLargestArmy()
    {
        return largestArmy;
    }

    public int GetKnights()
    {
        return knights;
    }

    public bool GetLongestRoad()
    {
        return longestRoad;
    }

    public int GetLongestRoadCount()
    {
        return longestRoadCount;
    }

    public void SetLongestRoad(bool value)
    {
        if (!value)
        {
            victoryPoints -= 2;
        }
        else
        {
            victoryPoints += 2;
        }
        longestRoad = value;
        NeedRefreshAction?.Invoke();
    }
}
