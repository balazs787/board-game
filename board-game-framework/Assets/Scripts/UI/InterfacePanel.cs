using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfacePanel : MonoBehaviour
{
    public ResourcePanel resourcePanel;
    public TextMeshProUGUI victoryPointsText;
    public EndTurn endTurn;

    public void UpdateVictoryPoints(Player player)
    {
        victoryPointsText.text = player.GetVictoryPoints().ToString();
    }
}
