using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayCatan()
    {
        SceneManager.LoadScene("Catan");
    }

    public void PlayTicTacToe()
    {
        SceneManager.LoadScene("TicTacToe");
    }
}
