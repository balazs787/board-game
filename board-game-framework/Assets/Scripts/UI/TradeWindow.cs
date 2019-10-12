using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeWindow : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI giveAmountText;
    public TextMeshProUGUI getAmountText;
    public TextMeshProUGUI giveTypeText;
    public TextMeshProUGUI getTypeText;
    private Resource _giveType;
    private Resource _getType;

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        OnGetTypeChanged();
        OnGiveTypeChanged();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void OnGiveTypeChanged()
    {
        Enum.TryParse(giveTypeText.text, out _giveType);
        getAmountText.text = "0";
        giveAmountText.text = "0";
    }

    public void OnGetTypeChanged()
    {
        Enum.TryParse(getTypeText.text, out _getType);
        getAmountText.text = "0";
        giveAmountText.text = "0";
    }

    public void AcceptTrade()
    {
        //TODO: trade
        gameController.GetPlayer().Trade(giveTypeText.text, Int32.Parse(giveAmountText.text), getTypeText.text,Int32.Parse(getAmountText.text));
        gameObject.SetActive(false);
        gameController.interfacePanel.Refresh(gameController.GetPlayer());
    }

    public void IncreaseAmount()
    {
        var get = Int32.Parse(getAmountText.text)+1;
        var multiplier = 4;
        if (gameController.GetPlayer().Tradeables[_giveType])
        {
            multiplier = 2;
        }
        else if(gameController.GetPlayer().Tradeables[Resource.none])
        {
            multiplier = 3;
        }

        if(gameController.GetPlayer().CanAfford(_giveType, get * multiplier))
        {
            getAmountText.text = get.ToString();
            giveAmountText.text = (get * multiplier).ToString();
        }
    }


    public void DecreaseAmount()
    {
        if (Int32.Parse(getAmountText.text) > 0)
        {
            var get = Int32.Parse(getAmountText.text)-1;
            

            var multiplier = 4;
            if (gameController.GetPlayer().Tradeables[_giveType])
            {
                multiplier = 2;
            }
            else if (gameController.GetPlayer().Tradeables[Resource.none])
            {
                multiplier = 3;
            }

            getAmountText.text = (get).ToString();
            giveAmountText.text = (get * multiplier).ToString();
        }
    }
}
