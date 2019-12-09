using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyCard : MonoBehaviour, ICard
{
    private bool _playable;

    public void Play(CatanGameController gameController)
    {
        if (gameController.GetPlayer().Ai)
        {
            Resource r = gameController.catanAi.PickLowestResource();
            gameController.interfacePanel.monopolyWindow.GetResourcesFromOthers?.Invoke(r);
            return;
        }

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
