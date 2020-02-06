using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUpdater : MonoBehaviour
{
    [SerializeField]
    private Text Text;

    [SerializeField]
    private Image Image;

    void Awake()
    {
        GameController.CardUpdate = UpdateData; // при изменении карты
    }

    void UpdateData(Card card)
    {
        Text.text = card.Text;
        Image.sprite = Resources.Load<Sprite>(@"Sprites/" + card.Icon);
    } 

}