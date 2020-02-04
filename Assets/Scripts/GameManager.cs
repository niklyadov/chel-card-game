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

    public GameManager()
    {
        Defines.GameManager = this;
    }

    private void Start()
    {
        deck = Deck.CreateFromJSON(Resources.Load<TextAsset>("cards").text);
        deckSpecial = Deck.CreateFromJSON(Resources.Load<TextAsset>("special_cards").text);

        progressManager = new ProgressManager();
        CardPos = CardPosition.Passive;

        if (deck == null || deckSpecial == null)
            throw new System.Exception("Error while load deck");

        if (deck.CardList.Length == 0 || deckSpecial.CardList.Length == 0)
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
        Defines.CardBehaviour.AudioSource.PlayOneShot(Defines.CardBehaviour.Clip);

        if (CardPos == CardPosition.OnLeft)
            progressManager.ApplyChanges(currentCard.Left);
        else
            progressManager.ApplyChanges(currentCard.Right);

        Defines.VisManager.UpdateParametres(Defines.GameManager.progressManager.progresses);

        if (!CheckParametersNormal(Defines.GameManager.progressManager.progresses)) Restart();
        else currentCard = deck.GetRandom();

        Defines.VisManager.UpdateMainCard(currentCard.Icon, currentCard.Text);
        CardPos = CardPosition.Passive;
    }

    private bool CheckParametersNormal(float[] parametres)
    {
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

    public void RestartBtn()
    {
        Restart();
        Defines.VisManager.UpdateParametres(Defines.GameManager.progressManager.progresses);
    }
}
