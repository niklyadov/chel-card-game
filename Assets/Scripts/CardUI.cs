using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField]
    private Text mainDescription;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private Text choiceDescrioption;

    // Start is called before the first frame update
    void Start()
    {
    }

    void UpdateCard()
    {
        mainDescription.text = "main descr";
        icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(@"Sprites/zaborchik.png");


        //choiceDescrioption.text = "choice descr";

        //currentcart.GetSpritePath
    }
}
