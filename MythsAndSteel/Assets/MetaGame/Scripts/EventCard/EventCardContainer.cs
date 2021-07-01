using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventCardContainer : MonoBehaviour
{

    [Header("Event Info")]
    [SerializeField] private EventCard _eventCardInfo = null;
    public EventCard EventCardInfo => _eventCardInfo;

 

    [Header("Object Reference")]
    [SerializeField] private Image _spriteObject = null;
    [SerializeField] private TextMeshProUGUI _titleText = null;
    [SerializeField] private TextMeshProUGUI _descriptionText = null;

    /// <summary>
    /// Ajoute une event à la carte
    /// </summary>
    /// <param name="eventC"></param>
    public void AddEvent(EventCard eventC){
        _eventCardInfo = eventC;
        UpdateInfo();
    }

    [EasyButtons.Button]
    void UpdateInfo(){
        _titleText.text = _eventCardInfo._eventName;
        _descriptionText.text = _eventCardInfo._description;
        _spriteObject.sprite = _eventCardInfo._eventSprite;
      
    }

}
