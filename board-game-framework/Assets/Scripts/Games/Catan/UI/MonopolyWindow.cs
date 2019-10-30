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

    private CatanPlayer _player;
    private Resource _resource = Resource.none;
    public Action<Resource> GetResourcesFromOthers;

    private void Start()
    {
        
    }

    public void Activate(CatanGameController gameController)
    {
        _player = (CatanPlayer)gameController.GetPlayer();
        gameObject.SetActive(true);
        playerNameText.text = _player.playerName;
        playerNameText.color = _player.color;
        resourcePanel.UpdateResources(_player.Resources);
    }

    public void PickResource(string resourceString)
    {
        Enum.TryParse(resourceString, out Resource resourceEnum);
        _resource = resourceEnum;

        if (_resource != Resource.none)
        {
            GetResourcesFromOthers?.Invoke(_resource);
        }

        gameObject.SetActive(false);
        _resource = Resource.none;
        _player = null;
    }
}
