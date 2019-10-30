using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatanInterfacePanel : MonoBehaviour
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
    public PlayCardButton playCardButton;
    public PlayCardsWindow playCardsWindow;
    public YearOfPlentyWindow yearOfPlentyWindow;
    public MonopolyWindow monopolyWindow;
    public VictoryWindow victoryWindow;
    public TradeWindow tradeWindow;

    public void Refresh(CatanPlayer player)
    {
        if (rollButton.diceRolled)
        {
            buildRoadButton.Refresh(player);
            buildSettlementButton.Refresh(player);
            buildTownButton.Refresh(player);
            buyDevelopmentButton.Refresh(player);
        }
        playCardButton.Refresh(player);
        resourcePanel.UpdateResources(player.Resources);
        victoryPointsText.text = player.GetVictoryPoints().ToString();
        playCardsWindow.Refresh(player);
    }

    public void SetInteractable(bool interactable)
    {
        buildRoadButton.SetInteractable(interactable);
        buildSettlementButton.SetInteractable(interactable);
        buildTownButton.SetInteractable(interactable);
        buyDevelopmentButton.SetInteractable(interactable);
        tradeWindow.SetInteractable(interactable);
    }

    public int Roll()
    {
        int first = Random.Range(1, 7);
        int second = Random.Range(1, 7);
        rollButton.DiceRolled(first + second);
        endTurn.Hide(false);
        tradeWindow.SetInteractable(true);
        return first + second;
    }

    public void Hide(bool hide)
    {
        gameObject.SetActive(!hide);
        endTurn.Hide(hide);
    }

    public void NewTurn()
    {
        playCardsWindow.Close();
        Hide(false);
        SetInteractable(false);
        endTurn.Hide(true);
        rollButton.Rollable();
    }

    public void OutOfCards()
    {
        buyDevelopmentButton.outOfCards = true;
    }

    public void OpenYearOfPlentyWindow(CatanPlayer player)
    {
        yearOfPlentyWindow.Activate(player);
    }

    public void OpenMonopolyWindow(CatanGameController gameController)
    {
        monopolyWindow.Activate(gameController);
    }

    public void GameEnded(CatanPlayer player)
    {
        victoryWindow.Open(player);
        Hide(true);
    }
}
