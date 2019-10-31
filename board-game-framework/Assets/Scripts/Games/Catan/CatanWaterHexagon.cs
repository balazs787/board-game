using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatanWaterHexagon : HexagonField
{
    public bool trading;
    public Resource resource;
    public GameObject[] connections = new GameObject[6];
    public bool[] connectionsActive = new bool[6];

    private void Start()
    {
        if (trading)
            Instantiate(Resources.Load<GameObject>("Models/trade" + resource.ToString()), transform);

        for (int i = 0; i < connections.Length; i++)
        {
            connections[i].SetActive(connectionsActive[i]);
        }
    }
    public void AddTradeableToPlayer(Crossroads crossroads, CatanPlayer player)
    {
        if (!trading)
            return;

        if ((crossroads == vertexes.top && connectionsActive[0]) ||
            (crossroads == vertexes.topRight && connectionsActive[1]) ||
            (crossroads == vertexes.bottomRight && connectionsActive[2]) ||
            (crossroads == vertexes.bottom && connectionsActive[3]) ||
            (crossroads == vertexes.bottomLeft && connectionsActive[4]) ||
            (crossroads == vertexes.topLeft && connectionsActive[5]))
        {
            player.AddTradeable(resource);
        }
    }
}
