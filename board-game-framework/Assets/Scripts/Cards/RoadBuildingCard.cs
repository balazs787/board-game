using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuildingCard : MonoBehaviour, ICard
{
    private bool _playable;
    public void Buy(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void Play()
    {
        throw new System.NotImplementedException();
    }

    public void Playable()
    {
        _playable = true;
    }
}
