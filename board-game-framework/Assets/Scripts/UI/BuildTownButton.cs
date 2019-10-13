using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTownButton : MonoBehaviour
{
    public void Refresh(Player player)
    {
        if (player.CanAfford(0, 0, 2, 0, 3) && player.towns<4)
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
