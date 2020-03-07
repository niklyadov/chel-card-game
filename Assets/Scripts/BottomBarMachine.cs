using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum BarPosition
{
    Opened,
    Closed
}

public class BottomBarMachine : MonoBehaviour
{
    public CardMachine cardMachine;
    private GraphicRaycaster raycaster;
    private Vector3 temporaryPos;
    private RectTransform rectTransform;
    private Action state;
    public bool IsOpened = false;

    [FormerlySerializedAs("ToggleBackgroundSound")] [SerializeField]
    private Toggle toggleBackgroundSound;
    [FormerlySerializedAs("ToggleOnlistSound")] [SerializeField]
    private Toggle toggleOnlistSound;
    [FormerlySerializedAs("ToggleOnlistVibrate")] [SerializeField]
    private Toggle toggleOnlistVibrate;

    [SerializeField]
    private Text achievementsLabel;

    private int tapCountsM = 10;
    private int tapCounts = 0;
    
    private void Awake()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        rectTransform = GetComponentInParent<RectTransform>();

        toggleBackgroundSound.onValueChanged.AddListener(delegate
        {
            {
                GameController.GameStorage.Options.MuteBackgroundSound = !toggleBackgroundSound.isOn;
                GameController.GameStorage.WriteOptions();
            };
        });
        toggleOnlistSound.onValueChanged.AddListener(delegate
        {
            {
                GameController.GameStorage.Options.MuteOnListSound = !toggleOnlistSound.isOn;
                GameController.GameStorage.WriteOptions();
            };
        });
        toggleOnlistVibrate.onValueChanged.AddListener(delegate
        {
            {
                GameController.GameStorage.Options.EnableVibrateOnList = toggleOnlistVibrate.isOn;
                GameController.GameStorage.WriteOptions();
            };
        });
    }

    public void ResetStats()
    {
        GameController.GameStorage.ResetAchievements();
        state = Closing;
    }
    public void ResetOptions()
    {
        GameController.GameStorage.ResetAchievements();
        state = Closing;
    }
    
    private void Start()
    {
        tapCounts = tapCountsM;
        temporaryPos = transform.position;
        state = Idle;

        toggleBackgroundSound.isOn  = !GameController.GameStorage.Options.MuteBackgroundSound;
        toggleOnlistSound.isOn      = !GameController.GameStorage.Options.MuteOnListSound;
        toggleOnlistVibrate.isOn =     GameController.GameStorage.Options.EnableVibrateOnList;
        
    }

    private void Update()
    {
        state.Invoke();
    }
    private void Idle()
    {
        if (CheckMouse(raycaster))
            state = HandleMove; // обработать все перемещения
    }

    private void HandleMove()
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
            if (transform.position.y + rectTransform.sizeDelta.y / 2 >= Screen.height * 0.6f)
                state = Opening;
            else
                state = Closing;
        }
    }

    public void CloseMenu() 
    {
        state = Closing;
    }

    private void Closing()
    {
        transform.position = Vector3.Lerp(transform.position, temporaryPos, 0.5f);

        if (Vector3.Distance(transform.position, temporaryPos) < 1)
            state = Closed;
    }

    private void Opening()
    {
        transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x, (Screen.height / 2) - 25, transform.position.z), 0.5f);

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, (Screen.height / 2) - 25, transform.position.z)) < 1)
            state = Opened;
    }
    private void Opened()
    {
        if (!IsOpened)
        {
            //GameController.BarStateChange[(int)GameController.GameMode](BarPosition.Opened);
            achievementsLabel.text = GameController.GameStorage.Achievements.ToString();
        }
        IsOpened = true;
        // --- ЗАГЛУШКА ---
        // открыто, ничего не делаем
    }

    private void Closed() 
    {
        if (IsOpened)
        {
            //GameController.BarStateChange[(int)GameController.GameMode](BarPosition.Closed);
        }
        IsOpened = false;
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