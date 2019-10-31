using UnityEngine;

public class TicTacToeGameController : MonoBehaviour, ITurnBasedGameController
{
    bool gameEnded = false;
    public TicTacToePlayer[] players;
    public Squaremap squaremap;
    public ClickedItem clickedItem;
    public VictoryWindow victoryWindow;
    public TurnWindow turnWindow;
    private int _activePlayerId;


    void Start()
    {
        clickedItem.SendClickedItem += (clickedGameObject) => PutMark(clickedGameObject);
        _activePlayerId = 0;
        Turn(GetPlayer());
    }

    public void PutMark(GameObject gameObj)
    {
        if (gameObj.tag == "Square" && !gameObj.GetComponentInParent<TicTacToeSquare>().GetOccupied() && !gameEnded)
        {
            gameObj.GetComponentInParent<TicTacToeSquare>().Put((TicTacToePlayer)GetPlayer());
            CheckVictory();
            NextPlayer();
        }
    }


    public string GetPlayerName()
    {
        return players[_activePlayerId].playerName;
    }

    public Player GetPlayer()
    {
        return players[_activePlayerId];
    }

    public void NextPlayer()
    {

        {
            if (_activePlayerId + 1 == players.Length)
            {
                _activePlayerId = 0;
            }
            else
            {
                _activePlayerId++;
            }
        }
        Turn(GetPlayer());
    }





    public void Turn(Player player)
    {
        turnWindow.Refresh(GetPlayer());
    }


    public void GameEnd()
    {
        turnWindow.Close();
        victoryWindow.Open(GetPlayer());
        gameEnded = true;
    }

    public void GameStart()
    {

    }

    public void CheckVictory()
    {
        foreach (var sq in squaremap.squares)
        {
            if (sq.CheckNeighbours(GetPlayer()))
            {
                GameEnd();
            }
        }
    }
}
