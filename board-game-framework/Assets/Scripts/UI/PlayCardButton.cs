using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardButton : MonoBehaviour
{
    public void Refresh(Player player)
    {
        SetInteractable(player.cards.Count > 0);
    }

    public void SetInteractable(bool interactable)
    {
        gameObject.GetComponent<Image>().color = interactable ? Color.white : Color.gray;
        gameObject.GetComponentInChildren<Button>().interactable = interactable ? true : false;
    }
}
