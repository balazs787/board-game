using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSettlementButton : MonoBehaviour
{
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.CanAfford(1, 1, 1, 1, 0) && player.settlements < 5);
    }

    public void SetInteractable(bool interactable)
    {
        gameObject.GetComponent<Image>().color = interactable ? Color.white : Color.gray;
        gameObject.GetComponentInChildren<Button>().interactable = interactable ? true : false;
    }
}
