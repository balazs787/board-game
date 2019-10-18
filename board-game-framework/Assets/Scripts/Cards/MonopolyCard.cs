using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyCard : MonoBehaviour, ICard
{
    private bool _playable;

    public void Play(GameController gameController)
    {
        gameController.interfacePanel.OpenMonopolyWindow(gameController);
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
