using System;
using System.Collections;
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
    public Deck deck;
    public int freeRoads;

    public Action RoadBuiltAction;
    public Action SettlementBuiltAction;
    public Action TownBuiltAction;
    public Action KnightPlayedAction;
    public Action<int> DropResourcesAction;
    public Action PlaceRobberAction;
    public Action StealResourcesAction;
    public Action ResourcesStolenAction;

    void Start()
    {
        DropResourcesAction += playerIndex => DropResources(playerIndex);
        interfacePanel.resourceDropWindow.NextPlayerResourceDropAction += (playerIndex) =>
        {
            DropResourcesAction?.Invoke(playerIndex);
        };
        RoadBuiltAction += AwardLongestRoad;
        RoadBuiltAction += RoadBuilt;
        SettlementBuiltAction += SettlementBuilt;
        KnightPlayedAction += AwardLargestArmy;
        KnightPlayedAction += () => 
        {
            PlaceRobberAction?.Invoke();
        };
        PlaceRobberAction += () =>
        {
            robber.PlaceRobber();
            interfacePanel.notificationWindow.Rob(GetPlayer());
        };
        StealResourcesAction += () =>
        {
            if (robber.GetCanSteal())
            {
                robber.Steal();
                interfacePanel.notificationWindow.Steal(GetPlayer());
            }
            else
            {
                ResourcesStolenAction?.Invoke();
            }
        };
        ResourcesStolenAction += () =>
        {
            interfacePanel.Refresh(GetPlayer());
            interfacePanel.notificationWindow.Hide(true);
        };

        activePlayerId = 0;
        Turn(GetPlayer());
    }

    private void RoadBuilt()
    {
        if (freeRoads>0)
        {
            freeRoads--;
            build.BuildThis("Road");
        }

        if (freeBuildPhase)
        {
            NextPlayer();
        }
    }

    private void SettlementBuilt()
    {
        if (freeBuildPhase)
        {
            build.BuildThis("Road");
        }
    }

    public void DropResources(int playerIndex=0)
    {
        if (playerIndex >= players.Length)
        {
            interfacePanel.endTurn.Hide(false);
            interfacePanel.Refresh(GetPlayer());
            PlaceRobberAction?.Invoke();
            return;
        }

        interfacePanel.endTurn.Hide(true);
        int amount = players[playerIndex].SevenRoll();
        if (amount > 0)
        {
            interfacePanel.resourceDropWindow.Activate(players[playerIndex], playerIndex, amount);
        }
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
                StopAllCoroutines();
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
            GetPlayer().MakeCardsPlayable();
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

   

    

    public void Turn(Player player)
    {
        if (freeBuildPhase)
        {
            interfacePanel.notificationWindow.FreeBuild(GetPlayer());
            interfacePanel.Hide(true);
            build.BuildThis("Settlement");
        }
        else
        {
            interfacePanel.NewTurn();
            interfacePanel.notificationWindow.Hide(true);
        }
    }

    public void DiceRoll()
    {   
        int dr = interfacePanel.Roll();

        if (dr == 7)
        {
            DropResourcesAction?.Invoke(0);
        }
        else
        {
            hexmap.DistributeResources(dr);
            interfacePanel.Refresh(GetPlayer());
        }
    }

    public void PickCard()
    {
        GetPlayer().BuyCard(deck);
        if (deck.cards.Count == 0)
            interfacePanel.OutOfCards();
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

    public void AwardLargestArmy()
    {
        Player currentPlayer = GetPlayer();
        Player currentAwardHolder=null;
        currentPlayer.knights++;
        if (currentPlayer.GetKnights() >= 3) {
            foreach (var p in players)
            {
                if (p.GetLargestArmy())
                {
                    currentAwardHolder = p;
                }
            }
            if (currentAwardHolder == null)
            {
                currentPlayer.SetLargestArmy(true);
            }
            else if (currentPlayer.GetKnights() > currentAwardHolder.GetKnights())
            {
                currentAwardHolder.SetLargestArmy(false);
                currentPlayer.SetLargestArmy(true);
            }
        }
    }

    public void AwardLongestRoad()
    {
        Player currentPlayer = GetPlayer();
        Player currentAwardHolder = null;
        currentPlayer.longestRoadCount = hexmap.CheckRoadCount(GetPlayer());
        if (currentPlayer.GetLongestRoadCount() >= 3)
        {
            foreach (var p in players)
            {
                if (p.GetLongestRoad())
                {
                    currentAwardHolder = p;
                }
            }
            if (currentAwardHolder == null)
            {
                currentPlayer.SetLongestRoad(true);
            }
            else if (currentPlayer.GetLongestRoadCount() > currentAwardHolder.GetLongestRoadCount())
            {
                currentAwardHolder.SetLongestRoad(false);
                currentPlayer.SetLongestRoad(true);
            }
        }
    }
}
