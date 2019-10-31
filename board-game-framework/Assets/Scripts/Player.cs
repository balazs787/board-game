using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public Color color;
    public string playerName;
    public int id;

    public int GetId()
    {
        return id;
    }
}
