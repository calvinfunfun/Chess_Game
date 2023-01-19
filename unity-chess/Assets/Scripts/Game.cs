using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private readonly GameObject[,] _positions = new GameObject[8, 8];
    private GameObject[] _playerBlack = new GameObject[16];
    private GameObject[] _playerWhite = new GameObject[16];

    //current turn
    private string _currentPlayer = "white";

    //Game Ending
    private bool _gameOver;

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {
        _playerWhite = new[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) };
        _playerBlack = new[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) };

        //Set all piece positions on the positions board
        for (var i = 0; i < _playerBlack.Length; i++)
        {
            SetPosition(_playerBlack[i]);
            SetPosition(_playerWhite[i]);
        }
    }

    private GameObject Create(string n, int x, int y)
    {
        var obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        var cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = n; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        var cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        _positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        _positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return _positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        return x >= 0 && y >= 0 && x < _positions.GetLength(0) && y < _positions.GetLength(1);
    }

    public string GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }

    public void NextTurn()
    {
        _currentPlayer = _currentPlayer == "white" ? "black" : "white";
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }
    
    public void Winner(string playerWinner)
    {
        _gameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}