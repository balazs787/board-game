using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeSquare :  SquareField
{
    private bool _occupied;
    public Player player;

    public void Put(TicTacToePlayer player)
    {
        _occupied = true;
        this.player = player;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = player.sign;
    }

    public bool CheckNeighbours(Player player)
    {
        return (
                    this.player == player &&
                    (edges.top != null && edges.bottom != null && edges.top.GetComponent<TicTacToeSquare>().player==player && edges.bottom.GetComponent<TicTacToeSquare>().player == player) ||
                    (edges.left != null && edges.right != null && edges.left.GetComponent<TicTacToeSquare>().player==player && edges.right.GetComponent<TicTacToeSquare>().player == player) ||
                    (vertexes.topLeft != null && vertexes.bottomRight != null && vertexes.topLeft.GetComponent<TicTacToeSquare>().player==player && vertexes.bottomRight.GetComponent<TicTacToeSquare>().player == player) ||
                    (vertexes.topRight != null && vertexes.bottomLeft != null && vertexes.topRight.GetComponent<TicTacToeSquare>().player==player && vertexes.bottomLeft.GetComponent<TicTacToeSquare>().player == player)
               );
    }

    public bool GetOccupied()
    {
        return _occupied;
    }
}
