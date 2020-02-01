using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum CardPosition
{
    OnLeft,
    OnRight,
    Passive
}

public class GameManager : MonoBehaviour
{
    Deck deck = null;
    Deck deckSpecial = null;
    public ProgressManager progressManager;

    public Card currentCard;
    public CardPosition CardPos;
    private bool GameMode;

    public GameManager()
    {
        Defines.GameManager = this;

    }

    private void Start()
    {
        //////ЭТУ ЖЕСТЬ НАДО КАК-ТО ИСПРАВИТЬ
        Defines.WorkPath = Path.Combine(Defines.Path, "Resources");
        Defines.CardsDataPath = Path.Combine(Defines.WorkPath, "cards.json");
        Defines.SpecialCardsDataPath = Path.Combine(Defines.WorkPath, "special_cards.json");
        Defines.SpritesPath = Path.Combine(Defines.WorkPath, "Sprites");

        Debug.Log(Defines.CardsDataPath);

        deck = DeckLoader.Load(Defines.CardsDataPath);
        deckSpecial = DeckLoader.Load(Defines.SpecialCardsDataPath);   //специальные карты

        foreach (var item in deckSpecial.CardList)
        {
            Debug.Log(item.ToString());
        }

        progressManager = new ProgressManager();
        CardPos = CardPosition.Passive;
        GameMode = true; //ПОТОМ ПОМЕНЯТЬ

        if (deck == null)
            throw new System.Exception("Error while load deck");

        if (deck.CardList.Length == 0)
            throw new System.Exception("Error: empty deck");
        Restart();
        currentCard = deckSpecial.CardList[0];
    }

    private void Update()
    {
        if (CardPos != CardPosition.Passive)
                ExecuteChoice();
    }


    private void ExecuteChoice()
    {
        if (CardPos == CardPosition.OnLeft)
        {
            Debug.Log("Left Choice");
            progressManager.ApplyChanges(currentCard.Left);
        }
        else
        {
            Debug.Log("Right Choice");
            progressManager.ApplyChanges(currentCard.Right);
        }
        Defines.VisManager.UpdateParametres();

        //добавить проверку на превышение параметров !!!
        if (!CheckParameterNormal())
        {
            Restart();
        }
        else
            currentCard = deck.GetRandom();
        Defines.VisManager.UpdateMainCard(currentCard.Icon, currentCard.Text);
        CardPos = CardPosition.Passive;
    }

    private bool CheckParameterNormal()
    {
        var parametres = Defines.GameManager.progressManager.progresses;
        var ok = false;

        if (parametres[0] <= 0)
            currentCard = deckSpecial.CardList[1];
        else if (parametres[0] >= 100)
            currentCard = deckSpecial.CardList[2];
        else if (parametres[1] <= 0)
            currentCard = deckSpecial.CardList[3];
        else if (parametres[1] >= 100)
            currentCard = deckSpecial.CardList[4];
        else if (parametres[2] <= 0)
            currentCard = deckSpecial.CardList[5];
        else if (parametres[2] >= 100)
            currentCard = deckSpecial.CardList[6];
        else if (parametres[3] <= 0)
            currentCard = deckSpecial.CardList[7];
        else if (parametres[3] >= 100)
            currentCard = deckSpecial.CardList[8];
        else ok = true;

        return ok;
    }

    private void Restart()
    {
        Defines.GameManager.progressManager.progresses = new float[] { 50f, 30f, 50f, 50f };
    }

    //из сложного: добавить начало игры
}
