﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BottomBarController : MonoBehaviour
{
    public CardController cardController;
    private GraphicRaycaster raycaster;
    private Vector3 temporaryPos;
    private RectTransform rectTransform;
    [SerializeField]
    private bool hidden = true;
    private bool opened;
    private bool closing;

    private void Awake()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        rectTransform = GetComponentInParent<RectTransform>();
    }

    private void Start()
    {
        temporaryPos = transform.position;
    }

    public void Close()
    {
        closing = true;
        opened = false;

        transform.position =
                    new Vector3(transform.position.x, Screen.height * 0.5f - rectTransform.sizeDelta.y / 2);
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && CheckMouse(raycaster) && !opened)
        {

            if (cardController.enabled)
                cardController.enabled = false;

            hidden = false;

            // двигаем за позицей курсора
            transform.position = Vector3.Lerp(transform.position,
                        Input.mousePosition, Mathf.PingPong(Time.time * 0.5f, 0.5f));
            transform.position = new Vector3(temporaryPos.x, transform.position.y, temporaryPos.z);
        }
        else
        {
            if (transform.position.y + rectTransform.sizeDelta.y / 2 > Screen.height * 0.85f)
            {
             
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x, Screen.width - 110, transform.position.z), 0.5f);

                if (Vector3.Distance(transform.position, new Vector3(transform.position.x, Screen.width - 110, transform.position.z)) < 1)
                    opened = true;

                /// TODO: ограничить по Y
            }
            else
            {
                if (hidden)
                {
                    if (!cardController.enabled)
                        cardController.enabled = true;
                    return;
                }
                    
                transform.position = Vector3.Lerp(transform.position, temporaryPos, 0.5f);

                if (Vector3.Distance(transform.position, temporaryPos) < 1)
                {
                    hidden = true;
                    if (closing)
                        closing = false;
                }
            }
        }
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