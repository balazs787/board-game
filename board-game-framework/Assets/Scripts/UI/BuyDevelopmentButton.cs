using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDevelopmentButton : MonoBehaviour
{
    public bool outOfCards;
    public void Refresh(Player player)
    {
        if (player.CanAfford(0, 0, 1, 1, 1) && !outOfCards)
        {
            gameObject.GetComponent<Image>().color = Color.white;
            gameObject.GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.gray;
            gameObject.GetComponentInChildren<Button>().interactable = false;
        }
    }
}
