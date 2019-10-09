using System.Collections;
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

    public void Refresh(Player player)
    {
        resourcePanel.UpdateResources(player.Resources);
        victoryPointsText.text = player.GetVictoryPoints().ToString();
    }

    public void DropResources(Player player, int amount)
    {
        StartCoroutine(resourceDropWindow.Activate(player, amount));
        Refresh(player);
    }

    public int Roll()
    {
        int first = Random.Range(1, 7);
        int second = Random.Range(1, 7);
        rollButton.DiceRolled(first + second);
        return first + second;
    }

    public void NewTurn()
    {
        rollButton.Rollable();
    }

    public bool PlayerFinished()
    {
        return resourceDropWindow.GetAmount() == 0;
    }
}
