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
    public Build build;

    void Start()
    {
        activePlayerId = 0;
        Turn(GetPlayer());
    }
    void Update()
    {
        if (GetPlayer().NeedRefresh())
        {
            interfacePanel.resourcePanel.UpdateResources(GetPlayer().resources);
            interfacePanel.UpdateVictoryPoints(GetPlayer());
        }
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
        if (build.GetBuilding())
            return;

        if (activePlayerId + 1 == players.Length) {
            activePlayerId = 0;
        }
        else
        {
            activePlayerId++;
        }
        Turn(GetPlayer());
    }

    public void Turn(Player player)
    {
        if (!gameEnded)
        {
            int dr = DiceRoll();
            
            hexmap.distributeResources(dr);
        }
    }

    public int DiceRoll()
    {
        int first = Random.Range(1, 7);
        int second = Random.Range(1, 7);
        return first + second;
    } 

    public void GameEnd()
    {
        gameEnded = true;
    }

    public void GameStart()
    {
        
    }

    public void CheckVictory()
    {
        if (GetPlayer().GetVictoryPoints() == 10)
            GameEnd();
    }
}
