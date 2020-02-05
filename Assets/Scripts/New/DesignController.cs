using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DesignController : MonoBehaviour
{

    // карта
    [SerializeField]
    private Text mainDescription;
    [SerializeField]
    private Image icon;

    //описание выбора
    [SerializeField]
    private Text choiceDescription;


    public void UpdateMainCard(string sprite, string description)
    {
        mainDescription.text = description;
        icon.sprite = Resources.Load<Sprite>(@"Sprites/" + sprite);
    }

    public void UpdateDescription(string description)
    {
        choiceDescription.text = description;
    }
}
