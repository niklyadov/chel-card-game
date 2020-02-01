using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Deck deck;
    ProgressManager progressManager;

    Card currentCard;

    public GameManager()
    {
        Defines.GameManager = this;
        deck = DeckLoader.Load();
        progressManager = new ProgressManager();

        if (deck.CardList.Length == 0)
            throw new System.Exception("Error: empty deck");

    }

    private void Start()
    {

        foreach (var item in deck.CardList)
        {
            Debug.Log(item.ToString()); ////// PRINT DECK
        }
        Debug.Log("-----------------------------");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) LeftChoise();
        if (Input.GetKeyDown(KeyCode.D)) RightChoise();

    }

    public void LeftChoise() 
    {

        if (currentCard == null)
        {
            UpdateCard();
            return;
        }

        progressManager.ApplyChanges(currentCard.Left);
    }

    public void RightChoise()
    {
        if (currentCard == null)
        {
            UpdateCard();
            return;
        }

        progressManager.ApplyChanges(currentCard.Right);

    }

    public void UpdateCard(bool replace = true)
    {
        if (currentCard != null && replace || currentCard == null) 
        {
            currentCard = deck.GetRandom();
        }
        
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
