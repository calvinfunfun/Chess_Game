using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
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
    public Tilemap tileMap { get; private set; }
    
    //Sets the TileMap Object "Board" to the variable tilemap
    private void getMap(){
        tileMap = GetComponent<Tilemap>();
    }
 
    //Creates the board
    public void NewGame(Piece[ , ] state)
    {
        for (int x = 0; x < 8; x++)
        {

            tileMap.SetTile(new Vector3Int(x, 1, -1), B_Pawn);
            tileMap.SetTile(new Vector3Int(x, 6, -1), W_Pawn);

            for (int y = 0; y < 8; y++)
            {

                if (y == 0 && (x == 0 || x == 7))
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), B_Rook);
                }
                else if (y == 7 && (x == 0 || x == 7))
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), W_Rook);
                }
                else if (y == 0 && (x == 1 || x == 6))
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), B_Knight);
                }
                else if (y == 7 && (x == 1 || x == 6))
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), W_Knight);
                }
                else if (y == 0 && (x == 2 || x == 5))
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), B_Bishop);
                }
                else if (y == 7 && (x == 2 || x == 5))
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), W_Bishop);
                }
                else if (y == 7 && x == 3)
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), B_Queen);
                }
                else if (y == 7 && x == 4)
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), B_King);
                }
                else if (y == 0 && x == 3)
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), W_Queen);
                }
                else if(y == 0 && x == 4)
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), W_King);
                }
                else
                {
                    tileMap.SetTile(new Vector3Int(x, y, -1), Empty);

                }
                
                Piece piece = state[x, y];
                tileMap.SetTile(piece.position, getPiece(piece));
            }
        }
    }

    private Tile getPiece(Piece piece)
    {
        if (piece.isEmpty)
        {
            return Empty;
        }else if (piece.isWhite)
        {
            return getWhitePiece(piece);
        }
        else
        {
            return getBlackPiece(piece);
        }
    }

    private Tile getWhitePiece(Piece piece)
    {
        switch (piece.rank)
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
    
    private Tile getBlackPiece(Piece piece)
    {
        switch (piece.rank)
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
