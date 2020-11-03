using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Tile : MonoBehaviour
{
    [SerializeField] Color pressedColor = new Color(0, 255, 0);
    [SerializeField] Color notPressedColor = new Color(255, 0, 0);

    GameHandler gamehandler;
    SpriteRenderer spriteRenderer;

    List<PlayerController> players = new List<PlayerController>();

    // Start is called before the first frame update
    void Start()
    {
        gamehandler = FindObjectOfType<GameHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (players.Count > 0)
            spriteRenderer.color = pressedColor;
        else
            spriteRenderer.color = notPressedColor;
    }

    public void AddToPlayerList(PlayerController player)
    {
        players.Add(player);
        gamehandler.CheckPressedTiles();
    }

    public void RemoveFromPlayerList(PlayerController player)
    {
        players.Remove(player);
    }

    public bool GetPressed()
    {
        return players.Count > 0;
    }
}
