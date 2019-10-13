using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPointCard : MonoBehaviour, ICard
{
    private bool _playable = true;

    public void Play(Player player)
    {
        player.AddVictoryPoint();
    }

    public void Playable()
    {
        //no action needed
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
