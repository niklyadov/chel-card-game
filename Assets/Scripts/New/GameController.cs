using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Action<CardPosition> ChangePosition;
    public Action<CardPosition> UpdateCard;

//    private ProgressController progressController;
//    private DesignController designController;

    public Card CurrentCard;

    private Deck deck;
    private Deck specialDeck;

    private void Awake()
    {
		Application.targetFrameRate = 60;
		// Statics.GameController = this; // Я — GameController!
		/*
		 progressController = GetComponent<ProgressController>();
		 designController = GetComponent<DesignController>();
		 */
	}

    private void Start()
    {
        /*
        ChangePosition = OnChangeCardPosition;
        UpdateCard = OnUpdateCard;

        deck = Deck.CreateFromJSON(Resources.Load<TextAsset>("cards").text);
        specialDeck = Deck.CreateFromJSON(Resources.Load<TextAsset>("special_cards").text);

        if (deck == null || specialDeck == null)
        {
            throw new Exception("Error while load deck");
        }

        progressController.SetValues(new float[] { 50, 30, 50, 50 });
        */
    }

    private void OnChangeCardPosition(CardPosition position)
    {
        /*
        if (position == CardPosition.Passive)
            return;

        // ой, карта поменяла позицию

        if (CurrentCard != null)
        {
            designController.UpdateDescription(position == CardPosition.OnLeft ? CurrentCard.LeftLabel : CurrentCard.RightLabel);
        }
        */
    }

    private void OnUpdateCard(CardPosition position)
    {
        /*
        if (position == CardPosition.Passive)
            return;

        //применяем значения
        if (CurrentCard != null)
        {
            progressController.ApplyChanges(position == CardPosition.OnLeft ? CurrentCard.Left : CurrentCard.Right);
        }

        // карта должна поменяться
        CurrentCard = null;

        var values = progressController.GetValues();

        int checks = 0;
        for (int i = 0; i < values.Length; i++)
        {
            checks++;
            if (values[i] <= 0)
            {
                CurrentCard = specialDeck.GetCard(checks);
                break;
            }

            checks++;
            if (values[i] > 100)
            {
                CurrentCard = specialDeck.GetCard(checks);
                break;
            }
        }

        //специальная карта не была применена
        if (CurrentCard == null)
        {
            CurrentCard = deck.GetRandom();
        }

        designController.UpdateMainCard(CurrentCard.Icon, CurrentCard.Text);

    */
    }

    void OnGUI() // отображаем fps и тд
    {
        GUI.Label(new Rect(0, Screen.height - 40, 300, 20), $"frames {Math.Round(1.0f / Time.deltaTime)} FPS, time {Time.deltaTime}");
    }
}
