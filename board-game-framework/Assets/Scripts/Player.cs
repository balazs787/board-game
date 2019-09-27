using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public Resources resources = new Resources();
    public int victoryPoints;
    public Color color;
    public string playerName;
    public int id;

    public void givePlayerResources(Resource resource, int amount)
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
        givePlayerResources(resourceGive, -giveAmount);

        Enum.TryParse(getType, out Resource resourceGet);
        givePlayerResources(resourceGet, getAmount);
    }

    public int GetId()
    {
        return id;
    }
}
