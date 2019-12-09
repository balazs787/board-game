using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Players
{
    public static int activePlayers = 2;

    public static string[] names = { "player1", "player2", "player3", "player4" };
    public static bool[] ais = new bool[4];
    public static bool[] advancedais = new bool[4];
    public static Color[] colors = { Color.blue, Color.red, Color.green, Color.yellow };
}
