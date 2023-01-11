using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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

    private bool isCapturable(Vector3Int pos)
    {
        return false;
    }

    //My attempt at creating an isOccupied method
    private bool isOccupied(Vector3Int pos)
    {
        return false;
    }

    //Created for the Knight and King Movements
    private bool isOutOfBounds(Piece piece, int x, int y)
    {
        return false;
    }

}
