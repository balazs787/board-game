using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceDropWindow : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI amountText;
    public ResourcePanel resourcePanel;

    private int _amount;
    private CatanPlayer _player;
    private int _playerIndex;

    public Action<int> NextPlayerResourceDropAction;

    private void Start()
    {
        NextPlayerResourceDropAction += (_) =>
        {
            if (_amount != 0)
            {
                return;
            }

            gameObject.SetActive(false);
            _player = null;
            _playerIndex = 0;
        };
    }

    public void Activate(CatanPlayer player, int playerIndex, int amount)
    {
        if (amount == 0)
        {
            NextPlayerResourceDropAction?.Invoke(++playerIndex);
            return;
        }

        _player = player;
        _playerIndex = playerIndex;
        _amount = amount;
        gameObject.SetActive(true);
        playerNameText.text = player.playerName;
        playerNameText.color = player.color;
        Refresh();
    }

    public void Drop(string resourceString)
    {
        Enum.TryParse(resourceString, out Resource resourceEnum);
        var success = _player.DeductOneResource(resourceEnum);
        if (success)
        {
            _amount--;
            Refresh();
            if (_amount == 0)
            {
                NextPlayerResourceDropAction?.Invoke(++_playerIndex);
            }
        }
    }

    public void Refresh()
    {
        resourcePanel.UpdateResources(_player.Resources);
        amountText.text = $"Pick {_amount} resources to drop";
    }

    public int GetAmount()
    {
        return _amount;
    }
}
