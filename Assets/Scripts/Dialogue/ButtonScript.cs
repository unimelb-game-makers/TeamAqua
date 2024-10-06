using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    int index = 0;
    public Button[] button;
    void Start()
    {
        foreach (Button buts in button)
        {
            buts.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        foreach(Button buts in button)
        {
            buts.gameObject.SetActive(false);
        }
        
    }
}
