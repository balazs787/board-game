using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnWindow : MonoBehaviour
{
    public TextMeshProUGUI turnText;

    public void Refresh(Player player)
    {
        turnText.text = $"{player.playerName}'s turn";
        turnText.color = player.color;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
