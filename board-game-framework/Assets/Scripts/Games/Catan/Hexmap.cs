using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexmap : MonoBehaviour
{
    public List<CatanHexagon> hexagons;
    public List<Crossroads> crossroads;
    public List<Road> roads;

    public void DistributeResources(int diceRoll)
    {
        foreach (var hex in hexagons)
        {
            if (hex.getNumber() == diceRoll)
            {
                hex.Activate();
            }
        }
    }

    public int CheckRoadCount(CatanPlayer player)
    {
        int maxCount=0;
        List<Road> longestRoad;
        foreach (var r in roads)
        {
            if (r.player == player)
            {
                longestRoad = new List<Road>();
                longestRoad.Add(r);
                int currentCount=RoadCount(player, longestRoad, 1, 1,r.crossRoad1);
                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                }
                currentCount = RoadCount(player, longestRoad, 1, 1, r.crossRoad2);
                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                }
            }  
        }
        return maxCount;
    }

    public int RoadCount(CatanPlayer player, List<Road> longestRoad, int count, int maxCount, Crossroads lastCrossroads)
    {
        int currentCount = count;
        List<Road> newLongestRoad = longestRoad;
        List<Road> localRoads = new List<Road>();
        if(lastCrossroads == newLongestRoad[longestRoad.Count - 1].crossRoad1)
        {
            localRoads.Add(newLongestRoad[longestRoad.Count - 1].crossRoad1?.road1);
            localRoads.Add(newLongestRoad[longestRoad.Count - 1].crossRoad1?.road2);
            localRoads.Add(newLongestRoad[longestRoad.Count - 1].crossRoad1?.road3);
        }
        else
        {
            localRoads.Add(newLongestRoad[longestRoad.Count - 1].crossRoad2?.road1);
            localRoads.Add(newLongestRoad[longestRoad.Count - 1].crossRoad2?.road2);
            localRoads.Add(newLongestRoad[longestRoad.Count - 1].crossRoad2?.road3);
        }
        

        foreach (var lr in localRoads)
        {
            if(lr!=null && lr.player==player && !newLongestRoad.Contains(lr) && (lastCrossroads.GetPlayer()==player || lastCrossroads.GetPlayer()==null))
            {
                newLongestRoad.Add(lr);
                currentCount = RoadCount(player, newLongestRoad, count+1, maxCount,newLongestRoad[count].GetOppositeCrossroad(lastCrossroads));
            }
            if (currentCount > maxCount)
            {
                maxCount = currentCount;
            }
        }

        return maxCount;
    }
}
