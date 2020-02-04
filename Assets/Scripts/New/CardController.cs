using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CardController : MonoBehaviour
{
    private GraphicRaycaster raycaster;

    public GameObject ind;

    private RectTransform rectTransform;
    private Vector3 temporaryCardPos;

    private CardPosition cardPosition;

    private (float top, float bottom) cardLimits = (0.50f, 0.40f);
    private bool fadeOut;
    private float fadeOutDeltatime;
    private int fadeOutDurationTime = 2;
    private float fadeOutMinSizePercentage = 0.75f;

    private void Awake()
    {
        raycaster = gameObject.GetComponentInParent<GraphicRaycaster>();
        rectTransform = GetComponent<RectTransform>();

    }

    private void Start()
    {
        temporaryCardPos = rectTransform.position;
        cardPosition = CardPosition.Passive;
    }

    private void Update()
    {
        // запущена анимация уменьшения размеров карты
        if (fadeOut)
        {
            // если карта уже 
            if (cardPosition == CardPosition.Passive)
            {
                fadeOutDeltatime = fadeOutDurationTime; // Зануляем счетчик
                fadeOut = false;
            } else
            {

                fadeOutDeltatime -= Time.deltaTime;

                // уменьшаем до определенного процента (задан в fadeOutminSizePercentage)
                if (fadeOutDeltatime < fadeOutDurationTime / (fadeOutMinSizePercentage * fadeOutDurationTime))
                {
                    UpdateCard();
                    ChangeCardPosition(CardPosition.Passive);
                    fadeOutDeltatime = fadeOutDurationTime; // Зануляем счетчик
                    fadeOut = false; // остановим анимацию
                }
            }
            //размеры карты зависят от текущего значения счетчика
            rectTransform.localScale = new Vector3(fadeOutDeltatime / fadeOutDurationTime, fadeOutDeltatime / fadeOutDurationTime);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (CheckMouse(raycaster) || cardPosition != CardPosition.Passive)
            {
                // двигаем за позицей курсора
                rectTransform.position = Vector3.Lerp(rectTransform.position,
                            Input.mousePosition, Mathf.PingPong(Time.time * 0.2f, 0.8f));

                //ограничение движения карты
                if (rectTransform.position.y + rectTransform.sizeDelta.y / 2 >= Screen.height * cardLimits.top)
                {
                    rectTransform.position =
                        new Vector3(rectTransform.position.x, Screen.height * cardLimits.top);
                }
                else if (rectTransform.position.y + rectTransform.sizeDelta.y / 2 < Screen.height * cardLimits.bottom)
                {
                    rectTransform.position =
                        new Vector3(rectTransform.position.x, Screen.height * cardLimits.bottom);
                }

                // изменение позиции карты в зависимости от положения
                if (rectTransform.position.x <= 50)  // слева с отступом
                {
                    ChangeCardPosition(CardPosition.OnLeft); // говорим что слева
                }
                else if (rectTransform.position.x >= Screen.width - 50) // справа с отступом
                {
                    ChangeCardPosition(CardPosition.OnRight); // говорим что справа
                }
                else
                {
                    ChangeCardPosition(CardPosition.Passive); // иначе пассивна
                }
            }
        }
        else if (cardPosition == CardPosition.Passive) // в пассивном режиме возвращаем карту на центр экрана
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, temporaryCardPos, 0.3f);
            return;
        }
        else if (cardPosition != CardPosition.Passive)
        {
            if (!fadeOut)
            {
                fadeOut = true;
                fadeOutDeltatime = fadeOutDurationTime; // Зануляем счетчик
            }
        }

        if (cardPosition == CardPosition.OnLeft)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position,
                new Vector3(Screen.width * -0.25f, rectTransform.position.y),
                Mathf.PingPong(Time.time * 0.5f, 0.5f));
        }
        else if (cardPosition == CardPosition.OnRight)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position,
                new Vector3(Screen.width * 1.25f, rectTransform.position.y),
                Mathf.PingPong(Time.time * 0.5f, 0.5f));
        }

    }

    private void ChangeCardPosition(CardPosition pos)
    {
        if (pos == cardPosition) // значение не поменялось
            return;

        Debug.Log("Change state to: " +  pos.ToString());

        cardPosition = pos; // меняем значение

        //вызываем делегат при изменении позиции
        Statics.GameController.OnChangePosition(cardPosition);

    }

    private void UpdateCard()
    {
        Debug.Log("----- Change card -----");

        // вызываем делегат при изменении обновлении карты
        Statics.GameController.OnUpdateCard(cardPosition);
    }

    /// <summary>
    /// Проверить что мышка над обьектом
    /// </summary>
    /// <param name="ray">GraphicRaycaster</param>
    /// <returns>true/false в зависимости от положения мыши</returns>
    private bool CheckMouse(GraphicRaycaster ray)
    {
        var data = new PointerEventData(EventSystem.current);
        var objects = new List<RaycastResult>();
        data.position = Input.mousePosition;
        ray.Raycast(data, objects);

        foreach (var item in objects)
            if (Equals(item.gameObject, gameObject))
                return true;

        return false;
    }
}
