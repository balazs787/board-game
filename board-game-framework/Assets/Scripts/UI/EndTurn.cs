using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI playerTurnText;

    public void EndTurnButton()
    {
        gameController.NextPlayer();
        playerTurnText.text = gameController.GetPlayerName()+"'s Turn";
        playerTurnText.color = gameController.GetPlayer().color;
    }
}
