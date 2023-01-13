using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private Board _board;
    private Piece[,] _state;
    
    // Start is called before the first frame update
    void Start()
    {
        _board.NewGame(_state);
    }

    private void Awake()
    {
        _board = GetComponentInChildren<Board>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Sets the properties of each piece(type, position, isWhite, hasMoved)
    private void GeneratePieces()
    {
        for (int x = 0; x < 8; x++)
        {

            //tilemap.SetTile(new Vector3Int(x, 6, -1), B_Pawn);
            //tilemap.SetTile(new Vector3Int(x, 1, -1), W_Pawn);

            for (int y = 0; y < 8; y++) {
                if (y == 7)
                {
                    switch (x) 
                    {
                        case 0 or 7:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), B_Rook);
                            break;
                        case 1 or 6:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), B_Knight);
                            break;
                        case 2 or 5:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), B_Bishop);
                            break;
                        case 3:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), B_Queen);
                            break;
                        case 4:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), B_King);
                            break;
                    }
                }else if (y == 0)
                {
                    switch (x)
                    {
                        case 0 or 7:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), W_Rook);
                            break;
                        case 1 or 6:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), W_Knight);
                            break;
                        case 2 or 5:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), W_Bishop);
                            break;
                        case 3:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), W_Queen);
                            break;
                        case 4:
                            //tilemap.SetTile(new Vector3Int(x, y, -1), W_King);
                            break;
                    }
                }
            }
        }
    }
    
    //Gets all the possible moves of each piece when clicked
    public List<Vector3Int> GetPieceMoves(Piece piece)
    {
        List<Vector3Int> possibleMoves = new List<Vector3Int>();
        
        switch (piece.type)
        {
            case Piece.Type.Pawn:
            {
                //Check for captures
                if (!piece.HasMoved /* Need to check if square is occupied */)
                {
                    possibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + 2, -1));
                }
                
                //Need to check if square is occupied
                possibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + 1, -1));
                break;
            }


            case Piece.Type.Knight:
            {
                //Need to check if square is occupied
                int[] x = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
                int[] y = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

                for (int i = 0; i < 8; i++)
                {
                    if (!isOutOfBounds(piece, x[i], y[i]))
                    {
                        possibleMoves.Add(new Vector3Int(piece.Position.x + x[i], piece.Position.y + y[i], -1));
                    }
                }

                break;
            }

            case Piece.Type.Bishop:
            {
                break;
            }

            case Piece.Type.Rook:
            {
                break;
            }

            case Piece.Type.Queen:
            {
                break;
            }

            case Piece.Type.King:
            {
                //Account for castling and check if occupied
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x != 0 && y != 0 && !isOutOfBounds(piece, x, y))
                        {
                            possibleMoves.Add(new Vector3Int(piece.Position.x + x, piece.Position.y + y, -1));
                        }
                    }
                }
                break;
            }
                
            
            case Piece.Type.Empty:
                break;
            
        }

        return possibleMoves;
    }
    
    //Checks whether a piece can capture an opponent's piece at that position
    //for opponent pieces
    private bool isCapturable(Vector3Int pos)
    {
        return false;
    }

    //My attempt at creating an isOccupied method
    //for the same colored pieces
    private bool isOccupied(Vector3Int pos)
    {
        return false;
    }

    //Created for the Knight and King Movements
    private bool isOutOfBounds(Piece piece, int x, int y)
    {
        return false;
    }
    
    public Piece.Type PawnPromotion(Piece piece)
    {
        return Piece.Type.Empty;
    }

    public bool CanCastleKingSide()
    {
        return false;
    }

    public bool CanCastleQueenSide()
    {
        return false;
    }

    public bool IsInCheck(Piece piece, Vector3Int position)
    {
        /* Parameters: Piece (for isWhite), Vector3Int position
         * Check each direction from where they are
         * for opposite-colored bishops, rooks, queens, etc.
         * if none, return false
         */

        for (int x = 0; x + position.x < 8 /* OR statement finding a piece obstructing the line */; x++)
        {
            //Code for Tile at position in tilemap
        }
        
        
        return false;
    }
    
    public bool CanEnPassant(/* needs parameters */)
    {
        return false;
    }

    
}
