using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BottomBarHider : MonoBehaviour
{
    private GraphicRaycaster raycaster;
    private Vector3 temporaryPos;
    private RectTransform rectTransform;
    public BottomBarMachine barMachine;
    void Awake()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        rectTransform = GetComponentInParent<RectTransform>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        temporaryPos = rectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckMouse(raycaster) && Input.GetKey(KeyCode.Mouse0) && rectTransform.position.y < Screen.height * 0.8f && barMachine.IsOpened)
        {
            barMachine.CloseMenu();
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
