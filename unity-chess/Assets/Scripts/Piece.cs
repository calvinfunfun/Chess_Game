using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Piece
{
    public enum Type
    {
        Pawn,
        Rook,
        Bishop,
        Knight,
        Queen,
        King,
        Empty,
    }

    public Vector3Int Position;
    public Dictionary<Vector3Int, bool> PossibleMoves;
    public bool HasMoved;
    public Type type;
    public bool IsWhite;
    public bool IsEmpty;
    public bool DoubleSpace;

}
