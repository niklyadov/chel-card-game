using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Action<CardPosition> OnChangePosition;
    public Action<CardPosition> OnUpdateCard;

    private void Awake()
    {
        Statics.GameController = this; // Я — GameController!
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        OnChangePosition = onChangeCardPosition;
        OnUpdateCard = onUpdateCard;
    }

    private void onChangeCardPosition(CardPosition position)
    {

        // ой, карта поменяла позицию

    }

    private void onUpdateCard(CardPosition position)
    {

        // карта должна поменяться

    }

    void OnGUI() // отображаем fps и тд
    {
        GUI.Label(new Rect(0, Screen.height - 40, 300, 20), $"frames {Math.Round(1.0f / Time.deltaTime)} FPS; time {Time.deltaTime}");
    }
}
