using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyCard : MonoBehaviour, ICard
{
    private bool _playable;

    public void Play(GameController gameController)
    {
        throw new System.NotImplementedException();
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
