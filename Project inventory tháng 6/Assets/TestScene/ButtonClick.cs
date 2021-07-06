using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    
    private void Start()
    {
        
        
    }


    public void ClaimClickedEffect()
    {
        GameObject darkPanel = this.transform.parent.Find("Panel").gameObject;
        darkPanel.SetActive(true);
    }
}