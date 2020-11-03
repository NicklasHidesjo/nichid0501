using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Tile[] tiles = null;



    public void CheckPressedTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (!tiles[i].GetPressed())
                return;
            Debug.Log("you have beaten the level");
        }
    }

}
