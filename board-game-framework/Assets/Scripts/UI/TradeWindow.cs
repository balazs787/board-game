using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TradeWindow : MonoBehaviour
{
    public GameController gameController;
    public TMP_InputField giveAmountText;
    public TMP_InputField getAmountText;
    public TextMeshProUGUI giveTypeText;
    public TextMeshProUGUI getTypeText;

   public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void AcceptTrade()
    {
        //TODO: trade
        gameController.GetPlayer().Trade(giveTypeText.text, Int32.Parse(giveAmountText.text), getTypeText.text,Int32.Parse(getAmountText.text));
        gameObject.SetActive(false);
        gameController.interfacePanel.resourcePanel.UpdateResources(gameController.GetPlayer().Resources);
    }
}
