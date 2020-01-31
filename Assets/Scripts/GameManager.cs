using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Deck deck = DeckLoader.Load();

    private void Update()
    {
        Debug.Log(deck.CardList[0].Text);
    }

    private static Sprite LoadSprite(string filePath)
    {

        if (!File.Exists(filePath))
            return null;

        var texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(filePath));

        return Sprite.Create(texture, new Rect(
            new Vector2(0, 0),
            new Vector2(64, 64)),
            new Vector2(0, 0));
    }
}
