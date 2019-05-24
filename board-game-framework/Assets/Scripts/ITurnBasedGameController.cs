using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnBasedGameController : IGameController
{
    void turn(Player player);
    int diceRoll();
}
