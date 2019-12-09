using System;
using UnityEngine;

public class ClickedItem : MonoBehaviour
{
    public Action<GameObject> SendClickedItem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                SendClickedItem?.Invoke(hit.transform?.gameObject);
            }
        }
    }
}
