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
    private Player _player;

    public IEnumerator Activate(Player player, int amount)
    {
        _player = player;
        _amount = amount;
        gameObject.SetActive(true);
        playerNameText.text = player.playerName;
        playerNameText.color = player.color;
        Refresh();

        while (_amount!=0)
        {
            yield return null;
        }

        gameObject.SetActive(false);
        _player = null;
    }

    public void Drop(string resourceString)
    {
        Enum.TryParse(resourceString, out Resource resourceEnum);
        var success = _player.DeductOneResource(resourceEnum);
        if(success)
            _amount--;
        Refresh();
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
