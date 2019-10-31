using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryWindow : MonoBehaviour
{
    public TextMeshProUGUI victoryText;

    public void Open(Player player)
    {
        gameObject.SetActive(true);
        victoryText.text = $"Congratulations!\n{player.playerName} won";
        victoryText.color = player.color;
    }
}
