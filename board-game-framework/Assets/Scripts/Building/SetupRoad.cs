using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupRoad : MonoBehaviour
{
    public MeshRenderer road;

    public Material[] materials;


    public void Setup(int playerId)
    {
        road.material = materials[playerId];
    }
}
