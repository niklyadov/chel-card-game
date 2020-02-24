using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottomBarMachine : MonoBehaviour
{
    public CardMachine cardMachine;
    private GraphicRaycaster raycaster;
    private Vector3 temporaryPos;
    private RectTransform rectTransform;
    private Action state;


    [SerializeField]
    private Toggle ToggleBackgroundSound;
    [SerializeField]
    private Toggle ToggleOnlistSound;

    void Awake()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        rectTransform = GetComponentInParent<RectTransform>();

        ToggleBackgroundSound.onValueChanged.AddListener(delegate {
            {
                GameController.GameOptions.Options.MuteBackgroundSound = !ToggleBackgroundSound.isOn;
                GameController.GameOptions.WriteOptions();
            };
        });
        ToggleOnlistSound.onValueChanged.AddListener(delegate {
            {
                GameController.GameOptions.Options.MuteOnListSound = !ToggleOnlistSound.isOn;
                GameController.GameOptions.WriteOptions();
            };
        });
    }

    void Start()
    {
        temporaryPos = transform.position;
        state = Idle;

        ToggleBackgroundSound.isOn  = !GameController.GameOptions.Options.MuteBackgroundSound;
        ToggleOnlistSound.isOn      = !GameController.GameOptions.Options.MuteOnListSound;
    }

    void Update()
    {
        state.Invoke();
    }
    void Idle()
    {
        if (CheckMouse(raycaster))
            state = HandleMove; // обработать все перемещения
    }

    void HandleMove()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // отключаем контроллер карты
            if (cardMachine.enabled) cardMachine.enabled = false;

            // двигаем за позицей курсора
            transform.position = Vector3.Lerp(transform.position, Input.mousePosition, Time.deltaTime * 10);
            transform.position = new Vector3(temporaryPos.x, transform.position.y, temporaryPos.z);

            // ограничение по Y
            if (transform.position.y + rectTransform.sizeDelta.y / 2 >= Screen.height)
                state = Opening;
        }
        else 
        {
            if (transform.position.y + rectTransform.sizeDelta.y / 2 >= Screen.height * 0.8f)
                state = Opening;
            else
                state = Closing;
        }
        
    }

    public void CloseMenu() 
    {
        state = Closing;
    }

    void Closing() 
    {
        transform.position = Vector3.Lerp(transform.position, temporaryPos, 0.5f);

        if (Vector3.Distance(transform.position, temporaryPos) < 1)
            state = Closed;
    }

    void Opening()
    {
        transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x, (Screen.height / 2) - 25, transform.position.z), 0.5f);

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, (Screen.height / 2) - 25, transform.position.z)) < 1)
            state = Opened;
    }
    void Opened() 
    {
        // --- ЗАГЛУШКА ---
        // открыто, ничего не делаем
    }

    void Closed() 
    {
        // включить обратно управление картой (если отключено)
        if (!cardMachine.enabled) cardMachine.enabled = true;

        state = Idle;
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