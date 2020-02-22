using System;
using UnityEngine;
using UnityEngine.UI;
public class ProgressUpdater : MonoBehaviour
{
    [SerializeField]
    Text[] progresses;

    void Start()
    {
        GameController.ApplyChoice += UpdateProgress;

        //обновить прогресс в самом начале
        DisplayProgress();
    }
    void UpdateProgress(CardPosition position)
    {
        //выбираем соответствующий массив "последствий" для текущей карты
        float[] valuesToAdd = new float[4];
        if (position == CardPosition.OnLeft)
            valuesToAdd = GameController.CurrentCard.Left;
        else if (position == CardPosition.OnRight)
            valuesToAdd = GameController.CurrentCard.Right;

        //обновляем прогресс
        for (int i = 0; i < valuesToAdd.Length; i++)
        {
            GameController.Progress[i] += valuesToAdd[i]; //TODO добавить немного рандома;
        }

        DisplayProgress();
    }

    void DisplayProgress()
    {
        for (int i = 0; i < progresses.Length; i++)
        {
            progresses[i].text = GameController.Progress[i].ToString() + '%'; //обновляем значения на экране
            progresses[i].color = new Color(0.02f * (105 - GameController.Progress[i]), 0.02f * GameController.Progress[i], 0f);
        }
    }
}
