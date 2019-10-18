using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonopolyWindow : MonoBehaviour
{
    public ResourcePanel resourcePanel;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI amountText;

    private Player _player;
    private Resource _resource=Resource.none;

    public IEnumerator Activate(GameController gameController)
    {
        _player = gameController.GetPlayer();
        gameObject.SetActive(true);
        playerNameText.text = _player.playerName;
        playerNameText.color = _player.color;
        resourcePanel.UpdateResources(_player.Resources);

        while (_resource == Resource.none)
        {
            yield return null;
        }

        foreach (var p in gameController.players)
        {
            if (gameController.GetPlayer() != p)
            {
                int amount = p.Resources[_resource];
                _player.GivePlayerResources(_resource, amount);
                p.GivePlayerResources(_resource, -amount);
            }
        }

        

        resourcePanel.UpdateResources(_player.Resources);
        gameObject.SetActive(false);
        _resource = Resource.none;
        _player = null;
    }

    public void PickResource(string resourceString)
    {
        Enum.TryParse(resourceString, out Resource resourceEnum);
        _resource = resourceEnum;
    }
}
