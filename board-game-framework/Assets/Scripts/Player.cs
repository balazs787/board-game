using System;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public Resources resources = new Resources();
    public int victoryPoints;
    public Color color;
    public string playerName;
    public int id;

    public void GivePlayerResources(Resource resource, int amount)
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
    }

    public void Trade(string giveType, int giveAmount, string getType, int getAmount)
    {
        Enum.TryParse(giveType, out Resource resourceGive);
        GivePlayerResources(resourceGive, -giveAmount);

        Enum.TryParse(getType, out Resource resourceGet);
        GivePlayerResources(resourceGet, getAmount);
    }

    public bool DeductResources(int l, int b, int g, int w, int o)
    {
        if((resources.lumber - l >= 0) &&
           (resources.brick - b >= 0) &&
           (resources.grain - g >= 0) &&
           (resources.wool - w >= 0) &&
           (resources.ore - o >= 0))
        {
            resources.lumber -= l;
            resources.brick -= b;
            resources.grain -= g;
            resources.wool -= w;
            resources.ore -= o;

            return true;
        }
        return false;
    }

    public void AddVictoryPoint()
    {
        victoryPoints++;
    }

    public int GetVictoryPoints()
    {
        return victoryPoints;
    }
    public int GetId()
    {
        return id;
    }
}
