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
    public Tile Empty;
    public Tilemap tilemap { get; private set; }
    
    //Sets the TileMap Object "Board" to the variable tilemap
    private void getMap(){
        tilemap = GetComponent<Tilemap>();
    }
 
    //Creates the board
    public void NewGame(Piece[ , ] state)
    {
        for (int x = 0; x < 8; x++)
        {

            tilemap.SetTile(new Vector3Int(x, 1, -1), B_Pawn);
            tilemap.SetTile(new Vector3Int(x, 6, -1), W_Pawn);

            for (int y = 0; y < 8; y++) {
                if (x == 0)
                {
                    switch (y) 
                    {
                        case 0 or 7:
                            tilemap.SetTile(new Vector3Int(x, y, -1), B_Rook);
                            break;
                        case 1 or 6:
                            tilemap.SetTile(new Vector3Int(x, y, -1), B_Knight);
                            break;
                        case 2 or 5:
                            tilemap.SetTile(new Vector3Int(x, y, -1), B_Bishop);
                            break;
                        case 3:
                            tilemap.SetTile(new Vector3Int(x, y, -1), B_Queen);
                            break;
                        case 4:
                            tilemap.SetTile(new Vector3Int(x, y, -1), B_King);
                            break;
                    }
                }else if (x == 8)
                {
                    switch (y)
                    {
                        case 0 or 7:
                            tilemap.SetTile(new Vector3Int(x, y, -1), W_Rook);
                            break;
                        case 1 or 6:
                            tilemap.SetTile(new Vector3Int(x, y, -1), W_Knight);
                            break;
                        case 2 or 5:
                            tilemap.SetTile(new Vector3Int(x, y, -1), W_Bishop);
                            break;
                        case 3:
                            tilemap.SetTile(new Vector3Int(x, y, -1), W_Queen);
                            break;
                        case 4:
                            tilemap.SetTile(new Vector3Int(x, y, -1), W_King);
                            break;
                    }
                }
            }
        }
    }

    private Tile getPiece(Piece piece)
    {
        if (piece.IsEmpty)
        {
            return Empty;
        }else if (piece.IsWhite)
        {
            return GetWhitePiece(piece);
        }
        else
        {
            return GetBlackPiece(piece);
        }
    }

    private Tile GetWhitePiece(Piece piece)
    {
        switch (piece.Rank)
        {
            case 'P': 
                return W_Pawn;
            case 'N': 
                return W_Knight;
            case 'B': 
                return W_Bishop;
            case 'R': 
                return W_Rook;
            case 'Q': 
                return W_Queen;
            case 'K':
                return W_King;
            default: 
                return Empty;
        }
    }
    
    private Tile GetBlackPiece(Piece piece)
    {
        switch (piece.Rank)
        {
            case 'P': 
                return B_Pawn;
            case 'N': 
                return B_Knight;
            case 'B': 
                return B_Bishop;
            case 'R': 
                return B_Rook;
            case 'Q': 
                return B_Queen;
            case 'K':
                return B_King;
            default: 
                return Empty;
        }
    }
}
