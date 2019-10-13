using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    void Buy(Player player);

    void Playable();

    void Play();

    GameObject GetGameObject();
}
