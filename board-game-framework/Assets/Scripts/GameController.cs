﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ITurnBasedGameController
{
    bool gameEnded = false;
    public bool freeBuildPhase = true;
    public bool swap = false;
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
            interfacePanel.Refresh(GetPlayer());
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

        if (freeBuildPhase)
        {
            if (!swap)
            {
                if(activePlayerId + 1 == players.Length)
                {
                    swap = true;
                }
                else
                {
                    activePlayerId++;
                }
            }
            else
            {
                if (activePlayerId == 0)
                {
                    freeBuildPhase = false;
                }
                else
                {
                    activePlayerId--;
                }
            }
        }
        else
        {
            StopCoroutine(Building());
            if (activePlayerId + 1 == players.Length)
            {
                activePlayerId = 0;
            }
            else
            {
                activePlayerId++;
            }
        }

        interfacePanel.Refresh(GetPlayer());
        Turn(GetPlayer());
    }

    IEnumerator Building()
    {
        build.BuildThis("Settlement");

        while (build.GetBuilding())
        {
            yield return null;
        }

        build.BuildThis("Road");

        while (build.GetBuilding())
        {
            yield return null;
        }

        interfacePanel.endTurn.EndTurnButton();
    }

    //IEnumerator RobberPlacement()
    //{
    //    while(GetPlayer().Placing())
    //}

    public void Turn(Player player)
    {
        if (freeBuildPhase)
        {
            StartCoroutine(Building());
        }
        else if (!gameEnded)
        {
            int dr = DiceRoll();

            if (dr == 7)
            {
                foreach (var p in players)
                {
                    p.SevenRoll();
                }
                GetPlayer().PlaceRobber();
            }
            else
            {
                hexmap.distributeResources(dr);
            }
            

            interfacePanel.Refresh(GetPlayer());
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
