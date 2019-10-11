using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatanTradeHexagon : CatanWaterHexagon
{
    public Resource resource;
    public GameObject[] connections = new GameObject[6];
    public bool[] connectionsActive = new bool[6];

    private void Start()
    {
        Instantiate(Resources.Load<GameObject>("Models/trade"+resource.ToString()), transform);
        for (int i = 0; i < connections.Length; i++)
        {
            connections[i].SetActive(connectionsActive[i]);
        }
    }
    public void AddTradeableToPlayer(Player player)
    {
        player.AddTradeable(resource);
    }
}
