using UnityEngine;

public enum BombType
{
    Column,
    Row,
    Adjacent,
    Color
}
public class Bomb : GamePiece
{
    public BombType bombType;   
   
}
