using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardButton : MonoBehaviour
{
    public void Refresh(Player player)
    {
        if (player.cards.Count>0)
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
