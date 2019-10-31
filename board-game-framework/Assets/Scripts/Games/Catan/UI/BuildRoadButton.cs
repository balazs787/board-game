using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRoadButton : MonoBehaviour
{
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.CanAfford(1, 1, 0, 0, 0) && player.roads < 15);
    }

    public void SetInteractable(bool interactable)
    {
        gameObject.GetComponent<Image>().color = interactable ? Color.white : Color.gray;
        gameObject.GetComponentInChildren<Button>().interactable = interactable ? true : false;
    }
}
