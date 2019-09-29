using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoad : MonoBehaviour
{
    public MeshRenderer road;

    public Material[] materials;


    public void Setup(int playerId)
    {
        road.material = materials[playerId];
    }
}
