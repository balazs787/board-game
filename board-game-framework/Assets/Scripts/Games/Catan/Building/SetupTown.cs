using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupTown : MonoBehaviour
{
    public MeshRenderer base1;
    public MeshRenderer base2;
    public MeshRenderer roof;

    public Material[] materials;


    public void Setup(int playerId)
    {
        base1.material = materials[playerId];
        base2.material = materials[playerId];
        roof.material = materials[playerId];
    }
}
