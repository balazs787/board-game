using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ITurnBasedGameController
{
    bool gameEnded = false;
    Player[] players;
    public Hexmap hexmap;
    

    public void nextPlayer(Player player)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == player)
            {
                turn(players[i++]);
            }
        }
    }

    public void turn(Player player)
    {
        if (!gameEnded)
        {
            int dr = diceRoll();

            hexmap.distributeResources(dr);

            nextPlayer(player);
        }
    }

    public int diceRoll()
    {
        int first = Random.Range(1, 6);
        int second = Random.Range(1, 6);
        return first + second;
    } 

    public void gameStart()
    {
        
    }

    public void gameEnd()
    {
        gameEnded = false;
    }
}
