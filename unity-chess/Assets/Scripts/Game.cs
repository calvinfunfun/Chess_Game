using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Game : MonoBehaviour
{

    private Board _board;
    private Piece[,] _state;
    
    // Start is called before the first frame update
    private void Start()
    {
        _board.NewGame();
    }

    private void Awake()
    {
        _board = GetComponentInChildren<Board>();
    }
    // Update is called once per frame

    //Sets the properties of each piece(type, position, isWhite, hasMoved)
    private void GeneratePieces()
    {
        for (var x = 0; x < 8; x++)
        {
            for (var y = 0; y < 8; y++)
            {
                switch (y)
                {
                    case 7:
                        
                        switch (x)
                        {
                            case 0 or 7:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Rook,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = false
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 1 or 6:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Knight,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = false
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 2 or 5:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Bishop,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = false
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 3:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Queen,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = false
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 4:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.King,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = false
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                        }

                        break;
                    
                    case 0:
                        
                        switch (x)
                        {
                            case 0 or 7:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Rook,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = true
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 1 or 6:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Knight,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = true
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 2 or 5:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Bishop,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = true
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 3:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.Queen,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = true
                                };
                                _state[x, y] = piece;
                                break;
                            }
                            
                            case 4:
                            {
                                var piece = new Piece
                                {
                                    type = Piece.Type.King,
                                    Position = new Vector3Int(x, y, -1),
                                    IsEmpty = false,
                                    IsWhite = true
                                };
                                _state[x, y] = piece;
                                break;
                            }
                        }
                        break;

                    case 6:
                    {
                        var piece = new Piece
                        {
                            Position = new Vector3Int(x, y, -1),
                            IsWhite = false,
                            type = Piece.Type.Pawn,
                            IsEmpty = false
                        };
                        _state[x, y] = piece;
                        break;
                    }


                    case 1:
                    {
                        var piece = new Piece
                        {
                            Position = new Vector3Int(x, y, -1),
                            IsWhite = true,
                            type = Piece.Type.Pawn,
                            IsEmpty = false
                        };
                        _state[x, y] = piece;
                        break;
                    }
                        

                    default:
                    {
                        var piece = new Piece
                        {
                            Position = new Vector3Int(x, y, -1),
                            type = Piece.Type.Empty,
                            IsEmpty = true
                        };
                        _state[x, y] = piece;
                        break;
                    }
                }
            }
        }
    }

    //Gets all the possible moves of each piece 
    public List<Vector3Int> GetPieceMoves(Piece piece)
    {

        switch (piece.type)
        {
            case Piece.Type.Pawn:
            {
                //Check for captures
                if (!piece.HasMoved /* Need to check if square is occupied */)
                {
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + 2, -1));
                }
                
                //Need to check if square is occupied
                piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + 1, -1));
                break;
            }


            case Piece.Type.Knight:
            {
                //Need to check if square is occupied
                var moveSquares = new int[] { 2, 1, -1, -2 };

                for (var x = 0; x < moveSquares.Length; x++)
                {
                    for (var y = 0; y < moveSquares.Length; y++)
                    {
                        if (Math.Abs(x) != Math.Abs(y) && !IsOutOfBounds(piece, piece.Position.x + x, piece.Position.y + y) && 
                            !IsOccupied(piece, piece.Position.x + x, piece.Position.y + y) && 
                            !piece.PossibleMoves.Contains(new Vector3Int(x, y, -1)))
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }
                    }
                }

                break;
            }
            //Unfinished
            case Piece.Type.Bishop:
            {
                //Check upper right diagonal
                for (var i = math.max(piece.Position.x, piece.Position.y) + 1; i < 8; i++)
                {
                    var checkSquare = piece.Position.x > piece.Position.y
                        ? _state[i, piece.Position.y + i]
                        : _state[piece.Position.x + i, i];
                    if (checkSquare.type != Piece.Type.Empty)
                    {
                        if (checkSquare.IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(checkSquare.Position.x, checkSquare.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(checkSquare.Position.x, checkSquare.Position.y, -1));
                }
                
                //Check 
                for (var i = math.min(piece.Position.x, piece.Position.y) - 1; i > -1; i++)
                {
                    
                }

                for (var i = math.max(piece.Position.x, piece.Position.y) - 1; i > -1; i++)
                {
                    var checkSquare = piece.Position.x > piece.Position.y
                        ? _state[i, piece.Position.y + i]
                        : _state[piece.Position.x + i, i];
                    if (checkSquare.type != Piece.Type.Empty)
                    {
                        if (checkSquare.IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(checkSquare.Position.x, checkSquare.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(checkSquare.Position.x, checkSquare.Position.y, -1));
                }
                
                //Check bottom right diagonal
                for (var i = math.min(piece.Position.x, piece.Position.y) + 1; i < 8; i++)
                {
                    var checkSquare = piece.Position.x > piece.Position.y
                        ? _state[i, piece.Position.y + i]
                        : _state[piece.Position.x + i, i];
                    if (checkSquare.type != Piece.Type.Empty)
                    {
                        if (checkSquare.IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(checkSquare.Position.x, checkSquare.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(checkSquare.Position.x, checkSquare.Position.y, -1));
                }
                break;
            }

            case Piece.Type.Rook:
            {
                //Horizontal Movement
                for (var i = piece.Position.x + 1; i < 8; i++)
                {
                    if (_state[i, piece.Position.y].type != Piece.Type.Empty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }

                for (var i = piece.Position.x - 1; i > -1; i++)
                {
                    if (_state[i, piece.Position.y].type != Piece.Type.Empty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }
                
                //Vertical Movement
                for (var i = piece.Position.y + 1; i < 8; i++)
                {
                    if (_state[piece.Position.x, i].type != Piece.Type.Empty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                }

                for (var i = piece.Position.y; i > -1; i++)
                {
                    if (_state[piece.Position.x, i].type != Piece.Type.Empty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                }
                break;
            }

            case Piece.Type.Queen:
            {
                //Horizontal Movement
                for (var i = piece.Position.x + 1; i < 8; i++)
                {
                    if (_state[i, piece.Position.y].type != Piece.Type.Empty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }

                for (var i = piece.Position.x - 1; i > -1; i++)
                {
                    if (_state[i, piece.Position.y].type != Piece.Type.Empty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }
                
                //Vertical Movement
                for (var i = piece.Position.y + 1; i < 8; i++)
                {
                    if (_state[piece.Position.x, i].type != Piece.Type.Empty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                }

                for (var i = piece.Position.y; i > -1; i++)
                {
                    if (_state[piece.Position.x, i].type != Piece.Type.Empty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                }
                break;
            }

            case Piece.Type.King:
            {
                //Account for castling and check if occupied
                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        if (x != 0 && y != 0 && !IsOutOfBounds(piece, x, y))
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x + x, piece.Position.y + y, -1));
                        }
                    }
                }
                break;
            }
                
            
            case Piece.Type.Empty:
                break;
            
        }

        return piece.PossibleMoves;
    }
    
    //Checks whether a piece can capture an opponent's piece at that position
    //for opponent pieces
    private static bool IsCapturable(Vector3Int pos)
    {
        return false;
    }

    //My attempt at creating an isOccupied method
    //for the same colored pieces
    private static bool IsOccupied(Piece piece, int x, int y)
    {
        return false;
    }

    //Created for the Knight and King Movements
    private static bool IsOutOfBounds(Piece piece, int x, int y)
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
