using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatanAi : MonoBehaviour
{
    public CatanGameController gameController;
    public PlayCardsWindow playCardsWindow;
    public EndTurn endTurn;

    public void AiTurn(CatanPlayer player)
    {
        if (gameController.freeBuildPhase)
        {
            return;
        }

        int random = UnityEngine.Random.Range(0, 2);

        if (player.cards.Count > 0 && random == 1)
        {
            playCardsWindow.currentIndex = UnityEngine.Random.Range(0, player.cards.Count);
            playCardsWindow.PlayButton();
        }

        gameController.DiceRoll();

        if (player.advanced)
        {
            TryBuildSettlement(player, false);
            TryBuildRoad(player, false);
            TryBuildTown(player, false);

            if (player.CanAfford(0, 0, 1, 1, 1) && random == 1)
            {
                gameController.PickCard();
            }

            TryBuildSettlement(player, true);
            TryBuildRoad(player, true);
            TryBuildTown(player, true);
        }
        else
        {
            TryBuildSettlement(player, false);
            TryBuildRoad(player, false);
            TryBuildTown(player, false);

            if (player.CanAfford(0, 0, 1, 1, 1))
            {
                gameController.PickCard();
            }

            if (random == 1)
            {
                TryBuildSettlement(player, true);
                TryBuildRoad(player, true);
                TryBuildTown(player, true);
            }
        }

        

        if (endTurn.gameObject.activeSelf)
        {
            endTurn.EndTurnButton();
        }
    }

    public Resource PickLowestResource()
    {
        Resource resource = Resource.lumber;
        for (int i = 2; i <= 5; i++)
        {
            if (gameController.GetPlayer().Resources[(Resource)i] < gameController.GetPlayer().Resources[resource])
            {
                resource = (Resource)i;
            }
        }
        return resource;
    }

    public void TryBuildSettlement(CatanPlayer player, bool tryTrading)
    {
        if (player.settlements < 5 && (player.CanAfford(1, 1, 1, 1, 0) || (tryTrading && TryGetResources(1, 1, 1, 1, 0))))
        {
            gameController.build.BuildThis("Settlement");
        }
    }

    public void TryBuildRoad(CatanPlayer player, bool tryTrading)
    {
        if (player.roads < 15 && (player.CanAfford(1, 1, 0, 0, 0) || (tryTrading && TryGetResources(1, 1, 0, 0, 0))))
        {
            gameController.build.BuildThis("Road");
        }
    }

    public void TryBuildTown(CatanPlayer player, bool tryTrading)
    {
        if (player.towns < 4 && (player.CanAfford(0, 0, 2, 0, 3) || (tryTrading && TryGetResources(0, 0, 2, 0, 3))))
        {
            gameController.build.BuildThis("Town");
        }
    }

    public bool TryGetResources(int l, int b, int g, int w, int o)
    {
        var player = gameController.GetPlayer();
        int tradeRatio = player.Tradeables[Resource.none] ? 3 : 4;

        int[] resourceSum = new int[5];
        int lackAmount=0;
        int excessAmount=0;
        int[] excessTradeables = new int[5];
        int[] lackResources = new int[5];
        resourceSum[0] = player.Resources[Resource.lumber] - l;
        resourceSum[1] = player.Resources[Resource.brick] - b;
        resourceSum[2] = player.Resources[Resource.grain] - g;
        resourceSum[3] = player.Resources[Resource.wool] - w;
        resourceSum[4] = player.Resources[Resource.ore] - o;

        for (int i = 0; i < 5; i++)
        {
            if (resourceSum[i] < 0)
            {
                lackResources[i]= -resourceSum[i];
                lackAmount += -resourceSum[i];
            }
            else
            {
                if (player.Tradeables[(Resource)(i + 1)])
                {
                    excessTradeables[i] = resourceSum[i] / 2;
                }
                else
                {
                    excessTradeables[i] = resourceSum[i] / tradeRatio;
                }
                excessAmount += excessTradeables[i];
            }
        }

        if (lackAmount == 0)
        {
            return true;
        }

        if (excessAmount < lackAmount)
        {
            return false;
        }

        for (int i = 0; i < 5; i++)
        {
            while (lackResources[i] > 0)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (excessTradeables[j] > 0)
                    {
                        if(player.Tradeables[(Resource)(j + 1)])
                        {
                            player.Resources[(Resource)(j + 1)] -= 2;
                        }
                        else
                        {
                            player.Resources[(Resource)(j + 1)] -= tradeRatio;
                        }
                        lackResources[i]--;
                        excessTradeables[j]--;
                        lackAmount--;
                        player.Resources[(Resource)(i + 1)]++;
                        if (lackAmount == 0)
                        {
                            return true;
                        }
                        if (lackResources[i] == 0)
                        {
                            break;
                        }
                    }
                }
            }
        }
        return true;
    }

    public void DropResources(CatanPlayer player, int amount)
    {
        if (amount == 0)
        {
            return;
        }
        var rng = new System.Random();
        List<Resource> droppables = new List<Resource>();
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 0; j < player.Resources[(Resource)i]; j++)
            {
                droppables.Add((Resource)i);
            }
        }
        var droppablesRandomized = droppables.OrderBy(x => rng.Next()).ToList();
        for (int k = 0; k < amount; k++)
        {
            player.DeductOneResource(droppablesRandomized[k]);
        }
    }
}
