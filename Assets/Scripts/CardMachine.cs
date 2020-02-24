using System;
using UnityEngine;

/*
Cкрипт работает на "графе" состояний
Каждое состояние представляет собой функцию, которая состоит из двух частей
  1. поведение карты при нахождении в состоянии (может отсутствовать, т.е. ничего не делать)
  2. условие(-я) перехода в другие состояния
Именно эта функция(метод), причем только одна, будет вызываться в Update()

Возможные плюсы:
    отслеживается только то, что нужно отслеживать, то, что точно не произойдет, не отслеживается
    не более 4 проверок за фрейм

Возможные минусы:
    без графа перед глазами сложно понять, что происходит
    Такая штука не гибкая
*/
public class CardMachine : MonoBehaviour
{
    private Vector3 basicPosition;
    private RectTransform rectTransform;
    // Текущее состояние
    private Action state;

    // Определяем начальное состояние - карта пассивна
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        basicPosition = rectTransform.position;
        state = Passive;
        StartState();
    }

    void Update()
    {
        state.Invoke();
    }

    //Методы-состояния
    void StartState()
    {
        GameController.CardUpdate.Invoke(GameController.CurrentCard);
    }

    //Карта пассивна - ничего не происходит. Ждем пока на неё тыкнут
    void Passive()
    {
        if (MouseDown())
            state = ActiveInCenter;
    }

    //карта находится "в руке", следует за курсором/пальцем. Ждем пока её переместят вбок или отпустят
    void ActiveInCenter()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
            state = GoToCenter;
        else if (Input.mousePosition.x > 0.8 * Screen.width)
        {
            //TODO Здесь должен быть вызван скрипт, обновляющий описание выбора Справа
            GameController.SwitchDescription[(int)GameController.GameMode](CardPosition.OnRight);
            state = GoToTheRight;
        }
        else if (Input.mousePosition.x < 0.2 * Screen.width)
        {
            //TODO Здесь должен быть вызван скрипт, обновляющий описание выбора Слева
            GameController.SwitchDescription[(int)GameController.GameMode](CardPosition.OnLeft);
            state = GoToTheLeft;
        }

        else //изменение позиции вслед за курсором
            rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, Time.deltaTime * 10);
    }

    //карта возвращается в центр до начальной точки. Или пока на неё не тыкнут
    void GoToCenter()
    {
        if (MouseDown())
            state = ActiveInCenter;
        else if (rectTransform.position.x - basicPosition.x < 0.05 && rectTransform.position.y - basicPosition.y < 0.05) //если карта почти в центре
        {
            rectTransform.position = basicPosition; //доводим до центра
            state = Passive;
        }

        else //возвращение в центр
            rectTransform.position = Vector3.Lerp(rectTransform.position, basicPosition, Time.deltaTime * 10);
    }

    //Переход карты влево. Ждём пока отпустят или вернут карту в центр
    void GoToTheLeft()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
            state = DisappearOnLeft;
        else if (Input.mousePosition.x > 0.2 * Screen.width)
            state = ActiveInCenter;

        else if (rectTransform.position.x > -0.2 * Screen.width)
        //перемещение влево
            rectTransform.position -= new Vector3(Time.deltaTime * 1500, 0f, 0f);
    }

    void GoToTheRight()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
            state = DisappearOnRight;
        else if (Input.mousePosition.x < 0.8 * Screen.width)
            state = ActiveInCenter;

        else if (rectTransform.position.x < 1.2 * Screen.width)
        //перемещение вправо
            rectTransform.position += new Vector3(Time.deltaTime * 1500, 0f, 0f);
    }

    //карта "исчезает" и просто ждём пока она уедет за экран;
    void DisappearOnLeft()
    {
        if (rectTransform.position.x < -Screen.width)
        {
            //TODO Здесь должен быть вызван какой-нибудь LeftChoice, обновляющий прогресс
            GameController.ApplyChoice[(int)GameController.GameMode](CardPosition.OnLeft);
            state = UpdateCard;
        }
        else //улетает влево
            rectTransform.position -= new Vector3(Time.deltaTime * 3000, 0f, 0f);
    }

    void DisappearOnRight()
    {
        if (rectTransform.position.x > 2 * Screen.width)
        {
            //TODO Здесь должен быть вызван какой-нибудь RightChoice, обновляющий прогресс
            GameController.ApplyChoice[(int)GameController.GameMode](CardPosition.OnRight);
            state = UpdateCard;
        }

        else // улетает вправо
            rectTransform.position += new Vector3(Time.deltaTime * 3000, 0f, 0f); 
    }

    //появление новой карты. Дожидаемся пока она появится
    void ShowNewCard()
    {
        if (rectTransform.position.y > basicPosition.y)
        {
            rectTransform.position = basicPosition;
            state = Passive;
        }

        else // тихоньео 
            rectTransform.position += new Vector3(0f, Time.deltaTime * 3000, 0f);
    }

    void UpdateCard()
    {
        //TODO Здесь должен быть вызван какой-нибудь скрипт UpdateCard, выбирающий новую рандомную карту
        GameController.ChooseNewCard();
        rectTransform.position = new Vector3(basicPosition.x, -0.6f * Screen.height, 0f);
        state = ShowNewCard;
    }

    //вспомогательные штуки
    bool MouseDown() => rectTransform.rect.Contains(Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0))
                        && Input.GetKey(KeyCode.Mouse0);
}