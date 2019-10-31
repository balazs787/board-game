using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryWindow : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public GameObject backToMenuButton;

    public void Open(Player player)
    {
        gameObject.SetActive(true);
        backToMenuButton.SetActive(true);
        victoryText.text = $"Congratulations!\n{player.playerName} won";
        victoryText.color = player.color;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
