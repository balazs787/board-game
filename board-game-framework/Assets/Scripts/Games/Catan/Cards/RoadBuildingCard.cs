using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuildingCard : MonoBehaviour, ICard
{
    private bool _playable;

    public void Play(CatanGameController gameController)
    {
        gameController.GetPlayer().freeRoads = 2;
        gameController.build.BuildThis("Road");
    }

    public void Playable()
    {
        _playable = true;
    }

    public bool GetPlayable()
    {
        return _playable;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
