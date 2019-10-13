using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCard : MonoBehaviour, ICard
{
    private bool _playable;
    public void Play(Player player)
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
