using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField]
    private Text mainDescription;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private Text choiceDescrioption;

    private Vector3 basicScale;
    private Vector3 basicPos;
    private Transform cardTransform;
    private bool mouseDn;
    private bool cardIsChanging;
    private float _maxX = 3.5f;

    void Start()
    {
        cardTransform = this.gameObject.GetComponent<Transform>();
        basicScale = cardTransform.localScale;
        basicPos = cardTransform.position;
    }

    private void Update()
    {
        var x = Input.mousePosition.x;

        //Смена карты
        if (cardIsChanging)
        {
            Disappear();
            if (cardTransform.position.x < -5 || cardTransform.position.x > 5)
            {
                cardIsChanging = false;
                cardTransform.position = basicPos;
                UpdateCard();
            }
            return;
        }

        if (x >= Screen.width * 0.25f && x <= Screen.width * 0.75f) //возвращение карты на середину
            Center();
        else if (mouseDn) //Сдвиг карты влево или вправо        
            FollowFinger(x);
    }

    private void OnMouseDown()
    {
        if (cardIsChanging) return;
        //увеличение при нажатии
        cardTransform.localScale = cardTransform.localScale * 1.2f;
        mouseDn = true;
    }

    private void OnMouseUp()
    {
        //уменьшение при нажатии
        cardTransform.localScale = basicScale;
        mouseDn = false;

        if (Input.mousePosition.x > Screen.width * 0.8f || Input.mousePosition.x < Screen.width * 0.2f)
            cardIsChanging = true;
    }

    private void FollowFinger(float x)
    {
        if (x > Screen.width * 0.75f && cardTransform.position.x < _maxX)
            cardTransform.Translate(8 * Time.deltaTime, 0, 0);
        else if (x < Screen.width * 0.25f && cardTransform.position.x > -_maxX)
            cardTransform.Translate(-8 * Time.deltaTime, 0, 0);
    }

    private void Disappear()
    {
        if (cardTransform.position.x >= 0)
            cardTransform.Translate(8 * Time.deltaTime, 0, 0);
        else
            cardTransform.Translate(-8 * Time.deltaTime, 0, 0);
    }

    private void Center()
    {
        if (cardTransform.position.x < -0.1)
            cardTransform.Translate(8 * Time.deltaTime, 0, 0);
        else if (cardTransform.position.x > 0.1)
            cardTransform.Translate(-8 * Time.deltaTime, 0, 0);
        else
            cardTransform.position = basicPos;
    }

    private void UpdateCard()
    {
        mainDescription.text = "main descr";
        icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(@"Sprites/zaborchik");
    }
}
