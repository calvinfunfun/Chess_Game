using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public Sprite square;
    public TileMap tileMap { get; private set; }

    //Sets the TileMap Object "Board" to the variable tilemap
    private void getMap(){
        tilemap = GetComponent<Tilemap>();
    }
 
    //Creates the board
    public void CreateBoard(){

        for(int x = 0; x < 8; x++){
            for(int y = 0; y < 8; y++){
                tilemap.SetTile(new Vector3Int(x, t, 0), new square());
            }
        }
    }
}
