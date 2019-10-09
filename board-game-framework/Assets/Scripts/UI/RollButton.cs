using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollButton : MonoBehaviour
{
    public GameObject dices;
    public GameObject rollText;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Rollable()
    {
        gameObject.GetComponent<Button>().interactable = true;
        gameObject.SetActive(true);
        dices.SetActive(true);
        rollText.SetActive(false);
    }

    public void DiceRolled(int roll)
    {
        gameObject.GetComponent<Button>().interactable = false;
        dices.SetActive(false);
        rollText.SetActive(true);
        rollText.GetComponent<TextMeshProUGUI>().text = roll.ToString();
    }
}
