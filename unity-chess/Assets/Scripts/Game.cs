using System;
using UnityEngine;

public class Game : MonoBehaviour{
    
    private Board _board;
    private Piece[,] _state;
    private Camera _camera;
    private Piece _selectedPiece;

    // Start is called before the first frame update
    public void Start()
    {
        _camera = Camera.main;
        _board.NewGame(_state);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Gets the position of the tile
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var squarePos = _board.pieceMap.WorldToCell(worldPosition);
            var piece = GetPiece(squarePos);
            if (!piece.IsEmpty)
            {
                _selectedPiece = piece;
            }

            if (!_selectedPiece.IsEmpty && _selectedPiece.Position != squarePos)
            {
                MovePiece(_selectedPiece, squarePos);
                _selectedPiece = new Piece();
            }
        }
    }

    private void Awake()
    {
        _board = GetComponentInChildren<Board>();
    }
    
    //Gets all the possible moves of each piece 
    //Needs Pawn Promotion. Last special Move
    //Fix up Castling through check and into check
    private void GetPieceMoves(Piece piece)
    {

        switch (piece.type)
        {
            //Regular Movement
            case Piece.Type.Pawn:
            {
                var dir = (piece.IsWhite) ? 1 : -1;
                if (!IsOccupied(piece.Position.x, piece.Position.y + dir))
                {
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + dir, -1), false);
                    if (!IsOccupied(piece.Position.x, piece.Position.y + (dir * 2)) && !piece.HasMoved)
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, piece.Position.y + (dir * 2), -1), false);
                    }
                }
                
                if (piece.Position.x != 7)
                {
                    //Capturing of a piece
                    if (_state[piece.Position.x - 1, piece.Position.y + dir].IsEmpty &&
                        _state[piece.Position.x - 1, piece.Position.y + dir].IsWhite != piece.IsWhite)
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x - 1, piece.Position.y + dir, -1), true);
                    }
                    //En Passant 
                    if (Convert.ToInt32(!piece.IsWhite) * 7 + (piece.Position.y * dir) == Convert.ToInt32(!piece.IsWhite) * 7 + (4 * dir)
                        && _state[piece.Position.x - 1, piece.Position.y].type == Piece.Type.Pawn
                        && _state[piece.Position.x - 1, piece.Position.y].DoubleSpace)
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x - 1, piece.Position.y + dir, -1), true);
                    }
                }

                if (piece.Position.x != 0)
                {
                    //Capturing of a piece
                    if (_state[piece.Position.x + 1, piece.Position.y + dir].IsEmpty &&
                        _state[piece.Position.x + 1, piece.Position.y + dir].IsWhite != piece.IsWhite)
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x + 1, piece.Position.y + dir, -1), true);
                    }
                    //En Passant
                    if (Convert.ToInt32(!piece.IsWhite) * 7 + (piece.Position.y * dir) == Convert.ToInt32(!piece.IsWhite) * 7 + (4 * dir)
                        && _state[piece.Position.x + 1, piece.Position.y].type == Piece.Type.Pawn
                        && _state[piece.Position.x + 1, piece.Position.y].DoubleSpace)
                    {
                        piece.PossibleMoves.Add(new Vector3Int(piece.Position.x + 1, piece.Position.y + dir, -1), true);
                    }
                }

                
                
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
                            !IsOccupied(piece.Position.x + x, piece.Position.y + y))
                        {
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                }

                //Horizontal Left Movement
                for (var i = piece.Position.x - 1; i > -1; i++)
                {
                    if (_state[i, piece.Position.y].IsEmpty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                }
                
                //Vertical Upper Movement
                for (var i = piece.Position.y + 1; i < 8; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
                }

                //Vertical Lower Movement
                for (var i = piece.Position.y; i > -1; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                }

                //Horizontal Left Movement
                for (var i = piece.Position.x - 1; i > -1; i++)
                {
                    if (_state[i, piece.Position.y].IsEmpty)
                    {
                        if (_state[i, piece.Position.y].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(i, piece.Position.y, -1), true);
                }
                
                //Vertical Upper Movement
                for (var i = piece.Position.y + 1; i < 8; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
                }

                //Vertical Lower Movement
                for (var i = piece.Position.y; i > -1; i++)
                {
                    if (_state[piece.Position.x, i].IsEmpty)
                    {
                        if (_state[piece.Position.x, i].IsWhite != piece.IsWhite)
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(piece.Position.x, i, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
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
                            piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                        }

                        break;
                    }
                    piece.PossibleMoves.Add(new Vector3Int(x, y, -1), true);
                }
                break;
            }

            //Needs to account for threatened squares when castling and moving
            case Piece.Type.King:
            {
                //Regular King Moves
                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        if (x != 0 && y != 0 && !IsOutOfBounds(x, y))
                        {
                            piece.PossibleMoves.Add(new Vector3Int(piece.Position.x + x, piece.Position.y + y, -1), true);
                        }
                    }
                }
                //Castling Queen side
                if (!piece.HasMoved && !_state[0, Convert.ToInt32(!piece.IsWhite) * 7].HasMoved && piece.Position.x == 4 
                    && _state[1, Convert.ToInt32(!piece.IsWhite) * 7].IsEmpty 
                    && _state[2, Convert.ToInt32(!piece.IsWhite) * 7].IsEmpty 
                    && _state[3, Convert.ToInt32(!piece.IsWhite) * 7].IsEmpty)
                {
                    piece.PossibleMoves.Add(new Vector3Int(2, Convert.ToInt32(!piece.IsWhite) * 7, -1), false);
                }
                //Castling King side
                if (!piece.HasMoved && !_state[0, Convert.ToInt32(!piece.IsWhite) * 7].HasMoved && piece.Position.x == 4 
                    && _state[5, Convert.ToInt32(!piece.IsWhite) * 7].IsEmpty 
                    && _state[6, Convert.ToInt32(!piece.IsWhite) * 7].IsEmpty)
                {
                    piece.PossibleMoves.Add(new Vector3Int(6, Convert.ToInt32(!piece.IsWhite) * 7, -1), false);
                }
                break;
            }
            
            case Piece.Type.Empty:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //Moves the piece to the new square(newPos)
    public void MovePiece(Piece piece, Vector3Int newPos)
    {
        var temp = new Vector3Int(piece.Position.x, piece.Position.y, -1);
        _state[newPos.x, newPos.y] = new Piece
        {
            Position = new Vector3Int(newPos.x, newPos.y, -1),
            type = piece.type,
            IsEmpty = false,
            IsWhite = piece.IsWhite,
            HasMoved = true
        };
        
        _state[temp.x, temp.y] = new Piece
        {
            Position = new Vector3Int(temp.x, temp.y, -1),
            type = Piece.Type.Empty,
            IsEmpty = true
        };
        //Check for Double Move Pawns
        if (_state[newPos.x, newPos.y].type == Piece.Type.Pawn && Math.Abs(piece.Position.y - newPos.y) == 2)
        {
            _state[newPos.x, newPos.y].DoubleSpace = true;
            if (!IsOutOfBounds(newPos.x + 1, newPos.y))
            {
                GetPieceMoves(_state[newPos.x + 1, newPos.y]);
            }
            if (!IsOutOfBounds(newPos.x - 1, newPos.y))
            {
                GetPieceMoves(_state[newPos.x - 1, newPos.y]);
            }
        }
        //Check for castling
        if (_state[newPos.x, newPos.y].type == Piece.Type.King && Math.Abs(piece.Position.x - newPos.x) == 2)
        {
            if (piece.Position.x > newPos.x)
            {
                CastleQueenRook(piece);
            }
            else
            {
                CastleKingRook(piece);
            }
        }
        
        GetPieceMoves(_state[newPos.x, newPos.y]);
        _board.UpdateBoard(_state);
    }

    //Castles King side Rook with King
    private void CastleKingRook(Piece piece)
    {
        var temp = _state[0, Convert.ToInt32(!piece.IsWhite) * 7].Position;
        _state[5, Convert.ToInt32(!piece.IsWhite) * 7] = new Piece
        {
            Position = new Vector3Int(5, Convert.ToInt32(!piece.IsWhite) * 7, -1),
            type = piece.type,
            IsEmpty = false,
            IsWhite = piece.IsWhite,
            HasMoved = true
        };
        
        _state[temp.x, temp.y] = new Piece
        {
            Position = new Vector3Int(temp.x, temp.y, -1),
            type = Piece.Type.Empty,
            IsEmpty = true
        };
    }

    //Castles Queen side Rook with King
    private void CastleQueenRook(Piece piece)
    {
        var temp = _state[0, Convert.ToInt32(!piece.IsWhite) * 7].Position;
        _state[3, Convert.ToInt32(!piece.IsWhite) * 7] = new Piece
        {
            Position = new Vector3Int(3, Convert.ToInt32(!piece.IsWhite) * 7, -1),
            type = piece.type,
            IsEmpty = false,
            IsWhite = piece.IsWhite,
            HasMoved = true
        };
        
        _state[temp.x, temp.y] = new Piece
        {
            Position = new Vector3Int(temp.x, temp.y, -1),
            type = Piece.Type.Empty,
            IsEmpty = true
        };
    }

    private Piece GetPiece(Vector3Int pos)
    {
        return IsOutOfBounds(pos.x, pos.y) ? _state[pos.x, pos.y] : new Piece();
    }
    
    //Checks whether the king can capture an opponent's piece at that position
    //for opponent pieces
    private bool IsCapturable()
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

    public Vector3Int WhiteKingPosition()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (_state[x, y].IsWhite && _state[x, y].type == Piece.Type.King)
                {
                    return _state[x, y].Position;
                }
            }
        }

        return new Vector3Int(0, 0, 0);
    }

    public Vector3Int BlackKingPosition()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (!_state[x, y].IsWhite && _state[x, y].type == Piece.Type.King)
                {
                    return _state[x, y].Position;
                }
            }
        }

        return new Vector3Int(0, 0, 0);
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
}
