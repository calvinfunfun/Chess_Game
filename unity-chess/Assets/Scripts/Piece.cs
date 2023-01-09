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
    }

    private Vector3Int Position;
    public Type type;

}
