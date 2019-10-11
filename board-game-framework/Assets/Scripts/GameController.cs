﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ITurnBasedGameController
{
    bool gameEnded = false;
    public bool freeBuildPhase = true;
    public bool swap;
    public Player[] players;
    public Hexmap hexmap;
    public int activePlayerId;
    public InterfacePanel interfacePanel;
    public Build build;
    public Robber robber;
    

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
        if (build.GetBuilding() || robber.GetPlacingRobber() || robber.GetStealing())
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

    IEnumerator RobberPlacement()
    {
        robber.PlaceRobber();

        while (robber.GetPlacingRobber())
        {
            yield return null;
        }

        robber.Steal();

        while (robber.GetStealing())
        {
            yield return null;
        }

        interfacePanel.Refresh(GetPlayer());
    }

    IEnumerator DropResources()
    {
        interfacePanel.endTurn.Hide(true);
        foreach (var p in players)
        {
            int amount = p.SevenRoll();
            if (amount > 0)
            {
                interfacePanel.DropResources(p, amount);
            }

            while (!interfacePanel.PlayerFinished())
            {
                yield return null;
            }
        }

        interfacePanel.endTurn.Hide(false);
        interfacePanel.Refresh(GetPlayer());
        StartCoroutine(RobberPlacement());

    }

    public void Turn(Player player)
    {
        if (freeBuildPhase)
        {
            interfacePanel.Hide(true);
            StartCoroutine(Building());
        }
        else
        {
            interfacePanel.NewTurn();
        }
    }

    public void DiceRoll()
    {
        
        int dr = interfacePanel.Roll();

        if (dr == 7)
        {
            StartCoroutine(DropResources());
        }
        else
        {
            hexmap.DistributeResources(dr);
            interfacePanel.Refresh(GetPlayer());
        }


        
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
