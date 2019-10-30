using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearOfPlentyCard : MonoBehaviour, ICard
{
    private bool _playable;
    public YearOfPlentyWindow yearOfPlentyWindow;

    public void Play(GameController gameController)
    {
        gameController.interfacePanel.OpenYearOfPlentyWindow(gameController.GetPlayer());
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
