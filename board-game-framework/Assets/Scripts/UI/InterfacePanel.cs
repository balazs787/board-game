using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfacePanel : MonoBehaviour
{
    public ResourcePanel resourcePanel;
    public TextMeshProUGUI victoryPointsText;

    public void UpdateVictoryPoints(int victoryPoints)
    {
        victoryPointsText.text = victoryPoints.ToString();
    }
}
