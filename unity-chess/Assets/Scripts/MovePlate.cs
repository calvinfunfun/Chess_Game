using UnityEngine;

public class MovePlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chess piece that was tapped to create this MovePlate
    private GameObject _reference;
    
    //Location on the board
    private int _matrixX;
    private int _matrixY;

    //false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            //Set to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Destroy the victim Chess piece
        if (attack)
        {
            var cp = controller.GetComponent<Game>().GetPosition(_matrixX, _matrixY);

            switch (cp.name)
            {
                case "white_king":
                    controller.GetComponent<Game>().Winner("black");
                    break;
                case "black_king":
                    controller.GetComponent<Game>().Winner("white");
                    break;
            }

            Destroy(cp);
        }

        //Set the Chess piece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(_reference.GetComponent<Chessman>().GetXBoard(), 
            _reference.GetComponent<Chessman>().GetYBoard());

        //Move reference chess piece to this position
        _reference.GetComponent<Chessman>().SetXBoard(_matrixX);
        _reference.GetComponent<Chessman>().SetYBoard(_matrixY);
        _reference.GetComponent<Chessman>().SetCoords();

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(_reference);

        //Switch Current Player
        controller.GetComponent<Game>().NextTurn();

        //Destroy the move plates including self
        _reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        _matrixX = x;
        _matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        _reference = obj;
    }

    public GameObject GetReference()
    {
        return _reference;
    }
}