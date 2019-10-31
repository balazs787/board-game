using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    void Playable();

    bool GetPlayable();

    void Play(CatanGameController gameController);

    GameObject GetGameObject();
}
