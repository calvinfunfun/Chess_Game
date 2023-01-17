using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour{
    
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
    
    private Board _board;
    private Piece[,] _state;
    
    // Start is called before the first frame update
    private void Start()
    {
        _board.NewGame();
    }
    
    private Vector3Int _reset = new Vector3Int(0, 0, 0);
    private Vector3Int _selection1, _selection2 = new Vector3Int(0, 0, 0);
    private Piece piece;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_selection1 == _reset)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _selection1 = _board.pieceMap.WorldToCell(worldPosition);
                piece = GetPiece(_selection1);
            } 
            else
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _selection2 = _board.pieceMap.WorldToCell(worldPosition);
                
                //Convert to Switch Case for each piece type
                if (piece.IsWhite)
                {
                    
                    switch(piece.type)
                    {
                        case Piece.Type.Rook:
                            _board.pieceMap.SetTile(_selection2, W_Rook);
                            break;
                        case Piece.Type.Pawn:
                            _board.pieceMap.SetTile(_selection2, W_Pawn);
                            break;
                        case Piece.Type.Knight:
                            _board.pieceMap.SetTile(_selection2, W_Knight);
                            break;
                        case Piece.Type.Bishop:
                            _board.pieceMap.SetTile(_selection2, W_Bishop);
                            break;
                        case Piece.Type.Queen:
                            _board.pieceMap.SetTile(_selection2, W_Queen);
                            break;
                        case Piece.Type.King:
                            _board.pieceMap.SetTile(_selection2, W_King);
                            break;
                    }
                    
                }
                else
                {
                    switch(piece.type)
                    {
                        case Piece.Type.Rook:
                            _board.pieceMap.SetTile(_selection2, B_Rook);
                            break;
                        case Piece.Type.Pawn:
                            _board.pieceMap.SetTile(_selection2, B_Pawn);
                            break;
                        case Piece.Type.Knight:
                            _board.pieceMap.SetTile(_selection2, B_Knight);
                            break;
                        case Piece.Type.Bishop:
                            _board.pieceMap.SetTile(_selection2, B_Bishop);
                            break;
                        case Piece.Type.Queen:
                            _board.pieceMap.SetTile(_selection2, B_Queen);
                            break;
                        case Piece.Type.King:
                            _board.pieceMap.SetTile(_selection2, B_King);
                            break;
                    }
                }

                _selection1 = _reset;
                _selection2 = _reset;
                piece = new Piece();
            }
        }
    }

    private void Awake()
    {
        _board = GetComponentInChildren<Board>();
    }

    //Sets the properties of each piece(type, position, isWhite, hasMoved) at the beginning of the game.
    private void NewPieces()
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
                if (piece.IsWhite)
                {
                    if (!piece.HasMoved && !IsOccupied(piece.Position.x, piece.Position.y + 2))
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + 2, -1));
                    }

                    if (!IsOccupied(piece.Position.x, piece.Position.y + 1))
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + 1, -1));
                    }
                }
                else
                {
                    if (!piece.HasMoved && !IsOccupied(piece.Position.x, piece.Position.y - 2))
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y - 2, -1));
                    }

                    if (!IsOccupied(piece.Position.x, piece.Position.y - 1))
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y - 1, -1));
                    }
                }
                
                
                //Need to check if square is occupied
                break;
            }


            case Piece.Type.Knight:
            {
                //Need to check if square is occupied
                var moveSquares = new[] { 2, 1, -1, -2 };

                for (var x = 0; x < moveSquares.Length; x++)
                {
                    for (var y = 0; y < moveSquares.Length; y++)
                    {
                        if (Math.Abs(x) != Math.Abs(y) && !IsOutOfBounds(piece.Position.x + x, piece.Position.y + y) && 
                            !IsOccupied(piece.Position.x + x, piece.Position.y + y) && 
                            !piece.PossibleMoves.Contains(new Vector3Int(x, y, -1)))
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }
                    }
                }

                break;
            }
            
            case Piece.Type.Bishop:
            {
                //Check lower right diagonal
                for (var x = piece.Position.x + 1; x < 8; x++)
                {
                    var y = piece.Position.y - 1;
                    if (y < 0) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                }
                
                //Check upper right diagonal
                for (var x = piece.Position.x + 1; x < 8; x++)
                {
                    var y = piece.Position.y + 1;
                    if (y > 7) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                }
                
                //Check lower left diagonal
                for (var x = piece.Position.x - 1; x > -1; x++)
                {
                    var y = piece.Position.y - 1;
                    if (y < 0) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                }
                
                //Check upper left diagonal
                for (var x = piece.Position.x + 1; x > -1; x++)
                {
                    var y = piece.Position.y + 1;
                    if (y > 7) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                }
                
                break;
            }

            case Piece.Type.Rook:
            {
                //Horizontal Right Movement
                for (var i = piece.Position.x + 1; i < 8; i++)
                {
                    if (_state[i, piece.Position.y].IsEmpty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }

                //Horizontal Left Movement
                for (var i = piece.Position.x - 1; i > -1; i++)
                {
                    if (_state[i, piece.Position.y].IsEmpty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }
                
                //Vertical Upper Movement
                for (var i = piece.Position.y + 1; i < 8; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                }

                //Vertical Lower Movement
                for (var i = piece.Position.y; i > -1; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
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
                //Horizontal Right Movement
                for (var i = piece.Position.x + 1; i < 8; i++)
                {
                    if (_state[i, piece.Position.y].IsEmpty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }

                //Horizontal Left Movement
                for (var i = piece.Position.x - 1; i > -1; i++)
                {
                    if (_state[i, piece.Position.y].IsEmpty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1));
                }
                
                //Vertical Upper Movement
                for (var i = piece.Position.y + 1; i < 8; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                }

                //Vertical Lower Movement
                for (var i = piece.Position.y; i > -1; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1));
                }
                
                //Check lower right diagonal
                for (var x = piece.Position.x + 1; x < 8; x++)
                {
                    var y = piece.Position.y - 1;
                    if (y < 0) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                }
                
                //Check upper right diagonal
                for (var x = piece.Position.x + 1; x < 8; x++)
                {
                    var y = piece.Position.y + 1;
                    if (y > 7) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                }
                
                //Check lower left diagonal
                for (var x = piece.Position.x - 1; x > -1; x++)
                {
                    var y = piece.Position.y - 1;
                    if (y < 0) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                }
                
                //Check upper left diagonal
                for (var x = piece.Position.x + 1; x > -1; x++)
                {
                    var y = piece.Position.y + 1;
                    if (y > 7) break;
                    
                    if (_state[x, y].IsEmpty)
                    {
                        if (_state[x, y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1));
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
                        if (x != 0 && y != 0 && !IsOutOfBounds(x, y))
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x + x, piece.Position.y + y, -1));
                        }
                    }
                }
                break;
            }
                
            
            case Piece.Type.Empty:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return piece.PossibleMoves;
    }

    public Piece GetPiece(Vector3Int pos)
    {
        return _state[pos.x, pos.y];
    }
    //Checks whether a piece can capture an opponent's piece at that position
    //for opponent pieces
    private static bool IsCapturable()
    {
        return false;
    }
    
    //Checks whether a piece can move to a square (x, y) 
    //for the same colored pieces
    private bool IsOccupied(int x, int y)
    {
        return _state[x, y].IsEmpty;
    }

    //Created for the Knight and King Movements
    private static bool IsOutOfBounds(int x, int y)
    {
        return x is <= 7 and >= 0 && y is <= 7 and >= 0;
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

    public bool IsCheck()
    {
        /* Parameters: Piece (for isWhite), Vector3Int position
         * Check each direction from where they are
         * for opposite-colored bishops, rooks, queens, etc.
         * if none, return false
         */


        return false;
    }

    public bool IsCheckMate()
    {
        return false;
    }
    public bool CanEnPassant(/* needs parameters */)
    {
        return false;
    }

    
}
