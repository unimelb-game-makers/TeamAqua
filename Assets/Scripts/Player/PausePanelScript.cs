using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PausedPanel;
    private bool isPaused = true;
    void Start()
    {
        PausedPanel.SetActive(false);
        //isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DialogueSystem.GetIsPlaying())
        {
            if (!PausedPanel.activeSelf)
            {
                PausedPanel.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }

            else
            {
                PausedPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
        }
    }
}
