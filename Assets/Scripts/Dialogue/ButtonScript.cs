using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public GameObject ButtonPanel;
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        button.onClick.RemoveListener(OnButtonClick);
       
        ButtonPanel.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }
}
