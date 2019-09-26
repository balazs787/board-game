using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ITurnBasedGameController
{
    bool gameEnded = false;
    public Player[] players;
    public Hexmap hexmap;
    public int activePlayerId;
    public InterfacePanel interfacePanel;

    void Start()
    {
        activePlayerId = 0;
        Turn(players[activePlayerId]);
    }

    public string GetPlayerName()
    {
        return players[activePlayerId].playerName;
    }

    public Player GetPlayer()
    {
        return players[activePlayerId];
    }

    public void NextPlayer()
    {
        if (activePlayerId + 1 == players.Length) {
            activePlayerId = 0;
        }
        else
        {
            activePlayerId++;
        }
        Turn(players[activePlayerId]);
    }

    public void Turn(Player player)
    {
        if (!gameEnded)
        {
            int dr = diceRoll();

            hexmap.distributeResources(dr);

            interfacePanel.resourcePanel.UpdateResources(players[activePlayerId].resources);
            interfacePanel.UpdateVictoryPoints(players[activePlayerId].victoryPoints);
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
        gameEnded = true;
    }
}
