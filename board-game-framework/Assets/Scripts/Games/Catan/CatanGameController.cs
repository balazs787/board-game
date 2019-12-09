using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatanGameController : MonoBehaviour, ITurnBasedGameController, IDice
{
    bool gameEnded = false;
    public bool freeBuildPhase = true;
    public bool initialRoundOrder;
    public CatanPlayer[] players;
    public GameObject player;
    public CatanAi catanAi;
    public Hexmap hexmap;
    private int _activePlayerId;
    public CatanInterfacePanel interfacePanel;
    public Build build;
    public Robber robber;
    public Deck deck;
    public bool endTurn;
    public int turn = 0;

    public Action RoadBuiltAction;
    public Action SettlementBuiltAction;
    public Action TownBuiltAction;
    public Action KnightPlayedAction;
    public Action<int> DropResourcesAction;
    public Action PlaceRobberAction;
    public Action StealResourcesAction;
    public Action ResourcesStolenAction;

    private void Awake()
    {
        players = new CatanPlayer[Players.activePlayers];
        for (int i = 0; i < Players.activePlayers; i++)
        {
            GameObject p = Instantiate(player, transform);
            players[i] = p.GetComponent<CatanPlayer>();
            players[i].id = i;
            players[i].playerName = Players.names[i];
            players[i].Ai = Players.ais[i];
            players[i].advanced = Players.advancedais[i];
            players[i].color = Players.colors[i];
        }
    }
    void Start()
    {
        DropResourcesAction += playerIndex => DropResources(playerIndex);
        interfacePanel.resourceDropWindow.NextPlayerResourceDropAction += (playerIndex) =>
        {
            DropResourcesAction?.Invoke(playerIndex);
        };
        RoadBuiltAction += AwardLongestRoad;
        RoadBuiltAction += () => 
        {
            if (GetPlayer().HasFreeRoads())
            {
                build.BuildThis("Road");
            }

            if (freeBuildPhase)
            {
                NextPlayer();
            }
        };
        SettlementBuiltAction += () =>
        {
            if (freeBuildPhase)
            {
                build.BuildThis("Road");
            }
        };
        KnightPlayedAction += AwardLargestArmy;
        KnightPlayedAction += () =>
        {
            PlaceRobberAction?.Invoke();
        };
        PlaceRobberAction += () =>
        {
            robber.PlaceRobber();
            if (!GetPlayer().Ai) 
            { 
                interfacePanel.notificationWindow.Rob((CatanPlayer)GetPlayer());
            }
        };
        StealResourcesAction += () =>
        {
            if (robber.GetCanSteal())
            {
                robber.Steal();
                if (!GetPlayer().Ai)
                {
                    interfacePanel.notificationWindow.Steal((CatanPlayer)GetPlayer());
                }
            }
            else
            {
                ResourcesStolenAction?.Invoke();
            }
        };
        ResourcesStolenAction += () =>
        {
            interfacePanel.Refresh((CatanPlayer)GetPlayer());
            interfacePanel.notificationWindow.Hide(true);
        };
        interfacePanel.monopolyWindow.GetResourcesFromOthers += (resource) =>
        {
            foreach (var p in players)
            {
                if (GetPlayer() != p)
                {
                    int amount = p.Resources[resource];
                    GetPlayer().GivePlayerResources(resource, amount);
                    p.GivePlayerResources(resource, -amount);
                }
            }
            interfacePanel.resourcePanel.UpdateResources(GetPlayer().Resources);
        };

        foreach (var p in players)
        {
            p.NeedRefreshAction += () => interfacePanel.Refresh((CatanPlayer)GetPlayer());
        }

        GameStart();
    }

    public void DropResources(int playerIndex = 0)
    {
        interfacePanel.Hide(true);
        if (playerIndex >= players.Length)
        {
            if (!GetPlayer().Ai)
            {
                interfacePanel.Hide(false);
            }
            interfacePanel.endTurn.Hide(false);
            interfacePanel.Refresh((CatanPlayer)GetPlayer());
            PlaceRobberAction?.Invoke();
            return;
        }

        interfacePanel.endTurn.Hide(true);
        int amount = players[playerIndex].SevenRoll();

        if (players[playerIndex].Ai)
        {
            catanAi.DropResources(players[playerIndex], amount);
            amount = 0;
        }

        interfacePanel.resourceDropWindow.Activate(players[playerIndex], playerIndex, amount);
    }


    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (endTurn)
        {
            Turn(GetPlayer());
        }
    }


    public string GetPlayerName()
    {
        return players[_activePlayerId].playerName;
    }

    public CatanPlayer GetPlayer()
    {
        return players[_activePlayerId];
    }

    public void NextPlayer()
    {
        if (build.GetBuilding() || robber.GetPlacingRobber() || robber.GetStealing())
            return;

        if (freeBuildPhase)
        {
            if (!initialRoundOrder)
            {
                if (_activePlayerId + 1 == players.Length)
                {
                    initialRoundOrder = true;
                }
                else
                {
                    _activePlayerId++;
                }
            }
            else
            {
                if (_activePlayerId == 0)
                {
                    freeBuildPhase = false;
                }
                else
                {
                    _activePlayerId--;
                }
            }
        }
        else
        {
            ((CatanPlayer)GetPlayer()).MakeCardsPlayable();
            if (_activePlayerId + 1 == players.Length)
            {
                _activePlayerId = 0;
            }
            else
            {
                _activePlayerId++;
            }
        }

        interfacePanel.Refresh((CatanPlayer)GetPlayer());
        endTurn = true;
    }





    public void Turn(Player player)
    {
        turn++;
        Debug.Log("turn: " + turn);
        endTurn = false;
        if (freeBuildPhase)
        {
            interfacePanel.notificationWindow.FreeBuild((CatanPlayer)GetPlayer());
            interfacePanel.Hide(true);
            build.BuildThis("Settlement");
        }
        else if (GetPlayer().Ai)
        {
            catanAi.AiTurn(GetPlayer());
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
            interfacePanel.Refresh((CatanPlayer)GetPlayer());
        }
    }

    public void PickCard()
    {
        Debug.Log("PickCard");
        if (interfacePanel.buyDevelopmentButton.outOfCards)
        {
            return;
        }

        ((CatanPlayer)GetPlayer()).BuyCard(deck);
        if (deck.cards.Count == 0)
            interfacePanel.OutOfCards();
    }

    public void GameEnd()
    {
        interfacePanel.GameEnded((CatanPlayer)GetPlayer());
        gameEnded = true;
    }

    public void GameStart()
    {
        _activePlayerId = 0;
        endTurn = true;
    }

    public void CheckVictory()
    {
        if (((CatanPlayer)GetPlayer()).GetVictoryPoints() >= 10)
            GameEnd();
    }

    public void AwardLargestArmy()
    {
        CatanPlayer currentPlayer = (CatanPlayer)GetPlayer();
        CatanPlayer currentAwardHolder = null;
        currentPlayer.knights++;
        if (currentPlayer.GetKnights() >= 3)
        {
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
        CatanPlayer currentPlayer = (CatanPlayer)GetPlayer();
        if (currentPlayer.roads < 3)
        {
            return;
        }
        CatanPlayer currentAwardHolder = null;
        currentPlayer.longestRoadCount = hexmap.CheckRoadCount((CatanPlayer)GetPlayer());
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
