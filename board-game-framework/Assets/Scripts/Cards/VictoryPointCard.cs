using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPointCard : MonoBehaviour, ICard
{
    private bool _playable;
    public void Buy(Player player)
    {
        player.AddVictoryPoint();
    }

    public void Play()
    {
        //no action needed
    }

    public void Playable()
    {
        //no action needed
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
