using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCard : MonoBehaviour, ICard
{
    private bool _playable;
    public void Play(GameController gameController)
    {
        StartCoroutine(gameController.RobberPlacement());
        gameController.AddKnight();
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
