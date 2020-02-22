using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUpdater : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _audioClip;

    void Awake()
    {
        GameController.CardUpdate = UpdateData; // при изменении карты
    }

    void UpdateData(Card card)
    {
        _text.text = card.Text;
        _image.sprite = Resources.Load<Sprite>(@"Sprites/" + card.Icon);

        if(_audioSource.enabled) _audioSource.PlayOneShot(_audioClip);
    } 

}