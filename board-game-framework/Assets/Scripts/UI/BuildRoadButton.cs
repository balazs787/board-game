using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRoadButton : MonoBehaviour
{
    public void Refresh(Player player)
    {
        if (player.CanAfford(1, 1, 0, 0, 0) && player.roads<15)
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
