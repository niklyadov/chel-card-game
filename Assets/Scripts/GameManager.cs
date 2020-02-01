using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Deck deck = null;
    ProgressManager progressManager;

    public Card currentCard;

    public GameManager()
    {
        Defines.GameManager = this;
        

    }

    private void Start()
    {
        deck = DeckLoader.Load();
        progressManager = new ProgressManager();

        if (deck == null)
            throw new System.Exception("Error while load deck");

        if (deck.CardList.Length == 0)
            throw new System.Exception("Error: empty deck");

        foreach (var item in deck.CardList)
        {
            Debug.Log(item.ToString()); ////// PRINT DECK
        }
        Debug.Log("-----------------------------");

        UpdateCard(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) LeftChoise();
        if (Input.GetKeyDown(KeyCode.D)) RightChoise();

    }

    public void LeftChoise() 
    {
        progressManager.ApplyChanges(currentCard.Left);
        UpdateCard();
    }

    public void RightChoise()
    {
        progressManager.ApplyChanges(currentCard.Right);
        UpdateCard(true);
    }

    public void UpdateCard(bool replace = true)
    {
        if (currentCard != null && replace || currentCard == null) 
        {
            currentCard = deck.GetRandom();
        }

        Debug.Log(currentCard.ToString());
        
    }

    public void OnProgressChange(Progress progress, float value) 
    {
        foreach (var item in deck.CardList)
        {
            Debug.Log(" >> ."  + progress.ToString() + "  " + value); ////// PRINT DECK
        }
    }

    private Sprite LoadSprite(string filePath)
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
