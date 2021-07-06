using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public string name;
    public bool isCarry;
    public string type;
    [SerializeField] public Button button;
    [SerializeField] public Image image;
    private Popup _popup;


    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            _popup.ShowPopup(this);
        });
    }

    // public void SetData(string nam, bool iscarry, string ty, Sprite img, GameObject po)
    public void SetData(string nam, bool iscarry, string ty, Sprite img,GameObject popupObj)
    {
        name = nam;
        isCarry = iscarry;
        type = ty;
        image.sprite = img;
        _popup = popupObj.GetComponent<Popup>();
    }

  
}