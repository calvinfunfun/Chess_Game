using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    //Piece Type
    public Tile W_Knight;
    public Tile W_Bishop;
    public Tile W_Queen;
    public Tile W_King;
    public Tile W_Rook;
    public Tile W_Pawn;
    public Tile B_Knight;
    public Tile B_Bishop;
    public Tile B_Queen;
    public Tile B_King;
    public Tile B_Rook;
    public Tile B_Pawn;
    public Tile empty;
    public Tilemap pieceMap;
    public Tilemap highlightMap;
    
    //Sets the pieceMap Object "Board" to the variable pieceMap
    private void GetMap(){
        pieceMap = GetComponent<Tilemap>();
        highlightMap = GetComponent<Tilemap>();
    }
 
    //Creates the board
    public void NewGame()
    {
        for (var x = 0; x < 8; x++)
        {

            pieceMap.SetTile(new Vector3Int(x, 6, -1), B_Pawn);
            pieceMap.SetTile(new Vector3Int(x, 1, -1), W_Pawn);

            for (var y = 0; y < 8; y++) {
                if (y == 7)
                {
                    switch (x) 
                    {
                        case 0 or 7:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), B_Rook);
                            break;
                        case 1 or 6:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), B_Knight);
                            break;
                        case 2 or 5:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), B_Bishop);
                            break;
                        case 3:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), B_Queen);
                            break;
                        case 4:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), B_King);
                            break;
                    }
                }
                else if (y == 0)
                {
                    switch (x)
                    {
                        case 0 or 7:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), W_Rook);
                            break;
                        case 1 or 6:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), W_Knight);
                            break;
                        case 2 or 5:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), W_Bishop);
                            break;
                        case 3:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), W_Queen);
                            break;
                        case 4:
                            pieceMap.SetTile(new Vector3Int(x, y, -1), W_King);
                            break;
                    }
                }
                else if (y == 6)
                {
                    pieceMap.SetTile(new Vector3Int(x, y, -1), B_Pawn);
                }
                else if (y == 1)
                {
                    pieceMap.SetTile(new Vector3Int(x, y, -1), W_Pawn);
                }
                else
                {
                    pieceMap.SetTile(new Vector3Int(x, y, -1), empty);
                }
            }
        }
    }

    public void UpdateBoard(Piece[,] board)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Piece piece = board[x, y];
                pieceMap.SetTile(piece.Position, GetTile(piece));
                if (piece.DoubleSpace)
                {
                    piece.DoubleSpace = false;
                }
            }
        }
    }

    public Tile GetTile(Piece piece)
    {
        switch (piece.type)
        {
            case Piece.Type.Pawn:
                if (piece.IsWhite)
                {
                    return W_Pawn;
                }
                else
                {
                    return B_Pawn;
                }
            
            case Piece.Type.Knight:
                if (piece.IsWhite)
                {
                    return W_Knight;
                }
                else
                {
                    return B_Knight;
                }
            
            case Piece.Type.Bishop:
                if (piece.IsWhite)
                {
                    return W_Bishop;
                }
                else
                {
                    return B_Bishop;
                }
            
            case Piece.Type.Rook:
                if (piece.IsWhite)
                {
                    return W_Rook;
                }
                else
                {
                    return B_Rook;
                }
            
            case Piece.Type.Queen:
                if (piece.IsWhite)
                {
                    return W_Queen;
                }
                else
                {
                    return B_Queen;
                }
            
            case Piece.Type.King:
                if (piece.IsWhite)
                {
                    return W_King;
                }
                else
                {
                    return B_King;
                }
            
            case Piece.Type.Empty:
                return empty;
            
            default:
                return null;
        }
    }
}
