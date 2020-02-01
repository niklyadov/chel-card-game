using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private Transform cardTransform;
    private Vector3 basicScale;
    private Vector3 basicPos;

    private bool mouseDn;
    private bool cardIsChanging;
    private float _maxX = 3.5f;
    private bool updated = false;


    public void SetPause(bool to) 
    {
        Debug.Log("Pause = " + to);
        BoxCollider2D collider2D = gameObject.GetComponent<BoxCollider2D>();
        collider2D.enabled = !to;
    }

    void Start()
    {

        Defines.CardBehaviour = this;

        cardTransform = gameObject.GetComponent<Transform>();
        basicScale = cardTransform.localScale;
        basicPos = cardTransform.position;
    }

    void Update()
    {

        var x = Input.mousePosition.x;

        if (cardIsChanging)
        {
            Disappear();
            if (cardTransform.position.x < -5 || cardTransform.position.x > 5)
            {
                cardIsChanging = false;
                cardTransform.position = basicPos;
                if (cardTransform.position.x < 0)
                    Defines.GameManager.CardPos = CardPosition.OnLeft;
                else
                    Defines.GameManager.CardPos = CardPosition.OnRight;
            }
            return;
        }

        if (x >= Screen.width * 0.2f && x <= Screen.width * 0.78f) //возвращение карты на середину
            Center();
        else if (mouseDn) //Сдвиг карты влево или вправо        
            FollowFinger(x);
    }

    private void OnMouseDown()
    {
        if (cardIsChanging) return;

        //увеличение при нажатии
        cardTransform.localScale *= 1.05f;
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
        {
            if (!updated)
            {
                Defines.VisManager.UpdateDescription(Defines.GameManager.currentCard.RightLabel);
                Defines.VisManager.UpdateInfliuences(Defines.GameManager.currentCard.Right);
            }
                
            cardTransform.Translate(8 * Time.deltaTime, 0, 0);
        }
        else if (x < Screen.width * 0.25f && cardTransform.position.x > -_maxX)
        {
            if (!updated)
            {
                Defines.VisManager.UpdateDescription(Defines.GameManager.currentCard.LeftLabel);
                Defines.VisManager.UpdateInfliuences(Defines.GameManager.currentCard.Left);
            }
                cardTransform.Translate(-8 * Time.deltaTime, 0, 0);
        }
        updated = true;            
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
        updated = false;
        if (cardTransform.position.x < -0.1)
            cardTransform.Translate(8 * Time.deltaTime, 0, 0);
        else if (cardTransform.position.x > 0.1)
            cardTransform.Translate(-8 * Time.deltaTime, 0, 0);
        else
            cardTransform.position = basicPos;
    }
}