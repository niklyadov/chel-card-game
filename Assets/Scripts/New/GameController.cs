using System;
using UnityEngine;

public static class GameController
{
    public static Action<CardPosition> SwitchDescription = OnSwitchDescription;
    public static Action<CardPosition> ApplyChoice = OnApplyChoice;

    public static Card CurrentCard;

    private static Deck deck;
    private static Deck specialDeck;

    static GameController ()
    {
        Application.targetFrameRate = 60;

        deck = Deck.CreateFromJSON(Resources.Load<TextAsset>("cards").text);
        specialDeck = Deck.CreateFromJSON(Resources.Load<TextAsset>("special_cards").text);

        if (deck == null || specialDeck == null)
        {
            throw new Exception("Error while load deck");
        }
    }

    private static void OnSwitchDescription(CardPosition position)
    {

    }

    private static void OnApplyChoice(CardPosition position)
    {

    }

    public static void ChooseNewCard()
    {

    }

    /*
    private void OnChangeCardPosition(CardPosition position)
    {
        if (position == CardPosition.Passive)
            return;

        // ой, карта поменяла позицию

        if (CurrentCard != null)
        {
            designController.UpdateDescription(position == CardPosition.OnLeft ? CurrentCard.LeftLabel : CurrentCard.RightLabel);
        }
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

   
    }*/
}

