using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject playCatanButton;
    public GameObject playTicTacToeButton;
    public GameObject settingsButton;
    public GameObject confrimSettingsButton;
    public GameObject settingsPanel;
    public GameObject catanOptionsPanel;

    private int currentEdit;

    public void SetCurrentEdit(int edit)
    {
        currentEdit = edit;
    }

    public void SetName(TextMeshProUGUI name)
    {
        Players.names[currentEdit] = name.text;
    }

    public void SetAi(Toggle toggle)
    {
        Players.ais[currentEdit] = toggle.isOn;
    }

    public void SetAdvanced(Toggle toggle)
    {
        Players.advancedais[currentEdit] = toggle.isOn;
    }


    public void PlayCatan()
    {
        playCatanButton.SetActive(false);
        playTicTacToeButton.SetActive(false);
        settingsButton.SetActive(false);
        catanOptionsPanel.SetActive(true);
    }

    public void ChoosePlayerAndStart(int numberOfPlayers)
    {
        Players.activePlayers = numberOfPlayers;
        SceneManager.LoadScene("Catan");
    }

    public void PlayTicTacToe()
    {
        SceneManager.LoadScene("TicTacToe");
    }

    public void OpenSettings()
    {
        playCatanButton.SetActive(false);
        playTicTacToeButton.SetActive(false);
        settingsButton.SetActive(false);
        confrimSettingsButton.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void ConfirmSettings()
    {
        playCatanButton.SetActive(true);
        playTicTacToeButton.SetActive(true);
        settingsButton.SetActive(true);
        confrimSettingsButton.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void BackButton()
    {
        catanOptionsPanel.SetActive(false);
        playCatanButton.SetActive(true);
        playTicTacToeButton.SetActive(true);
        settingsButton.SetActive(true);
    }
}
