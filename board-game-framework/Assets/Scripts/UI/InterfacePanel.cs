﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfacePanel : MonoBehaviour
{
    public ResourcePanel resourcePanel;
    public TextMeshProUGUI victoryPointsText;
    public EndTurn endTurn;
    public ResourceDropWindow resourceDropWindow;
    public RollButton rollButton;
    public NotificationWindow notificationWindow;
    public BuildRoadButton buildRoadButton;
    public BuildSettlementButton buildSettlementButton;
    public BuildTownButton buildTownButton;
    public BuyDevelopmentButton buyDevelopmentButton;

    public void Refresh(Player player)
    {
        buildRoadButton.Refresh(player);
        buildSettlementButton.Refresh(player);
        buildTownButton.Refresh(player);
        buyDevelopmentButton.Refresh(player);
        resourcePanel.UpdateResources(player.Resources);
        victoryPointsText.text = player.GetVictoryPoints().ToString();
    }

    public void DropResources(Player player, int amount)
    {
        StartCoroutine(resourceDropWindow.Activate(player, amount));
    }

    public int Roll()
    {
        int first = Random.Range(1, 7);
        int second = Random.Range(1, 7);
        rollButton.DiceRolled(first + second);
        endTurn.Hide(false);          
        return first + second;
    }

    public void Hide(bool hide)
    {
        gameObject.SetActive(!hide);
        endTurn.Hide(hide);
    }

    public void NewTurn()
    {
        Hide(false);
        endTurn.Hide(true);
        rollButton.Rollable();
    }

    public bool PlayerFinished()
    {
        return resourceDropWindow.GetAmount() == 0;
    }
}
