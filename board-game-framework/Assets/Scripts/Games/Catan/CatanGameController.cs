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
    public Hexmap hexmap;
    private int _activePlayerId;
    public CatanInterfacePanel interfacePanel;
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
            interfacePanel.notificationWindow.Rob((CatanPlayer)GetPlayer());
        };
        StealResourcesAction += () =>
        {
            if (robber.GetCanSteal())
            {
                robber.Steal();
                interfacePanel.notificationWindow.Steal((CatanPlayer)GetPlayer());
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

        _activePlayerId = 0;
        Turn(GetPlayer());
    }

    private void RoadBuilt()
    {
        if (freeRoads > 0)
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

    public void DropResources(int playerIndex = 0)
    {
        if (playerIndex >= players.Length)
        {
            interfacePanel.endTurn.Hide(false);
            interfacePanel.Refresh((CatanPlayer)GetPlayer());
            PlaceRobberAction?.Invoke();
            return;
        }

        interfacePanel.endTurn.Hide(true);
        int amount = players[playerIndex].SevenRoll();

        interfacePanel.resourceDropWindow.Activate(players[playerIndex], playerIndex, amount);
    }


    void Update()
    {
        if (((CatanPlayer)GetPlayer()).NeedRefresh())
        {
            interfacePanel.Refresh((CatanPlayer)GetPlayer());
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
        Turn(GetPlayer());
    }





    public void Turn(Player player)
    {
        if (freeBuildPhase)
        {
            interfacePanel.notificationWindow.FreeBuild((CatanPlayer)GetPlayer());
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
            interfacePanel.Refresh((CatanPlayer)GetPlayer());
        }
    }

    public void PickCard()
    {
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

    }

    public void CheckVictory()
    {
        if (((CatanPlayer)GetPlayer()).GetVictoryPoints() == 10)
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
