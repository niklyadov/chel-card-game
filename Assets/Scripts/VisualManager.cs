using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour
{
    // карта
    [SerializeField]
    private Text mainDescription;
    [SerializeField]
    private GameObject icon;

    //описание выбора
    [SerializeField]
    private Text choiceDescrioption;

    //показания параметров
    [SerializeField]
    private Text[] parametres = new Text[4];

    //влияние параметров
    [SerializeField]
    private GameObject[] influences = new GameObject[4];

    void Start()
    {
        Defines.VisManager = this;
    }

    void Update()
    {
        
    }

    public void UpdateMainCard(string sprite, string description)
    {
        mainDescription.text = description;
        icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(@"Sprites/" + sprite);
        Debug.Log("Card Updated " + description + ' ' + sprite);
    }

    public void UpdateDescription(string description)
    {
        choiceDescrioption.text = description;
        Debug.Log("description Updated " + description);
    }

    public void UpdateParametres()
    {
        var newParams = Defines.GameManager.progressManager.progresses;
        for (int i = 0; i < 4; i++)
            parametres[i].text = System.Math.Round(newParams[i]).ToString() + '%';
    }

    public void UpdateInfliuences(float[] influence)
    {
        for (int i = 0; i < influence.Length; i++)
            influences[i].GetComponent<Image>().color = new Color(-influence[i] * 0.1f, influence[i] * 0.1f, 0.0f, System.Math.Abs(influence[i]) * 0.1f);
    }

    //добавить обновление процентиков
}
