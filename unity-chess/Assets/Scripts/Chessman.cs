using UnityEngine;
using UnityEngine.Serialization;

public class Chessman : MonoBehaviour
{
    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject movePlate;

    //Position for this Chess piece on the Board
    //The correct position will be set later
    private int _xBoard = -1;
    private int _yBoard = -1;

    //Variable for keeping track of the player it belongs to "black" or "white"
    private string _player;

    //References to all the possible Sprites that this Chess piece could be
    [FormerlySerializedAs("black_queen")] public Sprite blackQueen;
    [FormerlySerializedAs("black_knight")] public Sprite blackKnight;
    [FormerlySerializedAs("black_bishop")] public Sprite blackBishop;
    [FormerlySerializedAs("black_king")] public Sprite blackKing;
    [FormerlySerializedAs("black_rook")] public Sprite blackRook;
    [FormerlySerializedAs("black_pawn")] public Sprite blackPawn;
    [FormerlySerializedAs("white_queen")] public Sprite whiteQueen;
    [FormerlySerializedAs("white_knight")] public Sprite whiteKnight;
    [FormerlySerializedAs("white_bishop")] public Sprite whiteBishop;
    [FormerlySerializedAs("white_king")] public Sprite whiteKing;
    [FormerlySerializedAs("white_rook")] public Sprite whiteRook;
    [FormerlySerializedAs("white_pawn")] public Sprite whitePawn;

    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Take the instantiated location and adjust transform
        SetCoords();

        //Choose correct sprite based on piece's name
        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = blackQueen; _player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = blackKnight; _player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = blackBishop; _player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = blackKing; _player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = blackRook; _player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = blackPawn; _player = "black"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = whiteQueen; _player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = whiteKnight; _player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = whiteBishop; _player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = whiteKing; _player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = whiteRook; _player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn; _player = "white"; break;
        }
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = _xBoard;
        float y = _yBoard;

        //Adjust by variable offset
        x *= 1f;
        y *= 1f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return _xBoard;
    }

    public int GetYBoard()
    {
        return _yBoard;
    }

    public void SetXBoard(int x)
    {
        _xBoard = x;
    }

    public void SetYBoard(int y)
    {
        _yBoard = y;
    }

    private void OnMouseUp()
    {
        if (controller.GetComponent<Game>().IsGameOver() ||
            controller.GetComponent<Game>().GetCurrentPlayer() != _player) return;
        //Remove all move plates relating to previously selected piece
        DestroyMovePlates();

        //Create new MovePlates
        InitiateMovePlates();
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        var movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        foreach (var t in movePlates)
        {
            Destroy(t); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    private void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen" or "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight" or "white_knight":
                LMovePlate();
                break;
            case "black_bishop" or "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king" or "white_king":
                SurroundMovePlate();
                break;
            case "black_rook" or "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(_xBoard, _yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(_xBoard, _yBoard + 1);
                break;
        }
    }

    private void LineMovePlate(int xIncrement, int yIncrement)
    {
        var sc = controller.GetComponent<Game>();

        var x = _xBoard + xIncrement;
        var y = _yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>()._player != _player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    private void LMovePlate()
    {
        PointMovePlate(_xBoard + 1, _yBoard + 2);
        PointMovePlate(_xBoard - 1, _yBoard + 2);
        PointMovePlate(_xBoard + 2, _yBoard + 1);
        PointMovePlate(_xBoard + 2, _yBoard - 1);
        PointMovePlate(_xBoard + 1, _yBoard - 2);
        PointMovePlate(_xBoard - 1, _yBoard - 2);
        PointMovePlate(_xBoard - 2, _yBoard + 1);
        PointMovePlate(_xBoard - 2, _yBoard - 1);
    }

    private void SurroundMovePlate()
    {
        PointMovePlate(_xBoard, _yBoard + 1);
        PointMovePlate(_xBoard, _yBoard - 1);
        PointMovePlate(_xBoard - 1, _yBoard + 0);
        PointMovePlate(_xBoard - 1, _yBoard - 1);
        PointMovePlate(_xBoard - 1, _yBoard + 1);
        PointMovePlate(_xBoard + 1, _yBoard + 0);
        PointMovePlate(_xBoard + 1, _yBoard - 1);
        PointMovePlate(_xBoard + 1, _yBoard + 1);
    }

    private void PointMovePlate(int x, int y)
    {
        var sc = controller.GetComponent<Game>();
        if (!sc.PositionOnBoard(x, y)) return;
        var cp = sc.GetPosition(x, y);

        if (cp == null)
        {
            MovePlateSpawn(x, y);
        }
        else if (cp.GetComponent<Chessman>()._player != _player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    private void PawnMovePlate(int x, int y)
    {
        var sc = controller.GetComponent<Game>();
        var doubleMove = false;
        if (!sc.PositionOnBoard(x, y)) return;
        if (sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
        }

        if (sc.PositionOnBoard(x + 1, y) 
            && sc.GetPosition(x + 1, y) != null 
            && sc.GetPosition(x + 1, y).GetComponent<Chessman>()._player != _player)
        {
            MovePlateAttackSpawn(x + 1, y);
        }

        if (sc.PositionOnBoard(x - 1, y) 
            && sc.GetPosition(x - 1, y) != null 
            && sc.GetPosition(x - 1, y).GetComponent<Chessman>()._player != _player)
        {
            MovePlateAttackSpawn(x - 1, y);
        }
    }

    private void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        
        //Set actual unity values
        var mp = Instantiate(movePlate, new Vector3(x - 2.3f, y - 2.3f, -0.001f), Quaternion.identity);
        
        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;
        
        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        var mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    private void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        var mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        var mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}