using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    //Piece Type
    public Tile whiteKnight;
    public Tile whiteBishop;
    public Tile whiteQueen;
    public Tile whiteKing;
    public Tile whiteRook;
    public Tile whitePawn;
    public Tile blackKnight;
    public Tile blackBishop;
    public Tile blackQueen;
    public Tile blackKing;
    public Tile blackRook;
    public Tile blackPawn;
    public Tile empty;
    public Tilemap pieceMap;
    public Tilemap highlightMap;
    
    //Sets the pieceMap Object "Board" to the variable pieceMap
    private void Awake(){
        pieceMap = GetComponent<Tilemap>();
    }
 
    //Creates the board
    public void NewGame(Piece[,] board)
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
                                                                
                                pieceMap.SetTile(piece.Position, blackRook);
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
                                                                
                                pieceMap.SetTile(piece.Position, blackKnight);
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
                                                                
                                pieceMap.SetTile(piece.Position, blackBishop);
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
                                                                
                                pieceMap.SetTile(piece.Position, blackQueen);
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
                                                                
                                pieceMap.SetTile(piece.Position, blackKing);
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
                                                                
                                pieceMap.SetTile(piece.Position, whiteRook);
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
                                                                
                                pieceMap.SetTile(piece.Position, whiteKnight);
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
                                                                
                                pieceMap.SetTile(piece.Position, whiteBishop);
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
                                                                
                                pieceMap.SetTile(piece.Position, whiteQueen);
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
                                                                
                                pieceMap.SetTile(piece.Position, whiteKing);
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
                        
                        pieceMap.SetTile(piece.Position, blackPawn);
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
                        
                        pieceMap.SetTile(piece.Position, whitePawn);
                        break;
                    }
                    
                    default:
                        pieceMap.SetTile(new Vector3Int(x, y, -1), empty);
                        break;
                }
            }
        }
    }

    public void UpdateBoard(Piece[,] board)
    {
        for (var x = 0; x < 8; x++)
        {
            for (var y = 0; y < 8; y++)
            {
                var piece = board[x, y];
                pieceMap.SetTile(piece.Position, GetTile(piece));
                if (piece.DoubleSpace)
                {
                    piece.DoubleSpace = false;
                }
            }
        }
    }

    private Tile GetTile(Piece piece)
    {
        return piece.type switch
        {
            Piece.Type.Pawn => piece.IsWhite ? whitePawn : blackPawn,
            Piece.Type.Knight => piece.IsWhite ? whiteKnight : blackKnight,
            Piece.Type.Bishop => piece.IsWhite ? whiteBishop : blackBishop,
            Piece.Type.Rook => piece.IsWhite ? whiteRook : blackRook,
            Piece.Type.Queen => piece.IsWhite ? whiteQueen : blackQueen,
            Piece.Type.King => piece.IsWhite ? whiteKing : blackKing,
            Piece.Type.Empty => empty,
            _ => null
        };
    }
}
