using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnBasedGameController : IGameController
{
    void Turn(Player player);
    void NextPlayer();
}
