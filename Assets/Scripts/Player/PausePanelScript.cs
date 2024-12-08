using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PausedPanel;
    public static PausePanelScript PauseManager;
    public bool isPaused = false;

    private void Awake()
    {
        PauseManager = this;
    }

    public static PausePanelScript instance()
    {
        return PauseManager;
    }
    /*
    void Start()
    {
        PausedPanel.SetActive(false);
        isPaused = false;
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
                Debug.Log("E to pauseeee");
            }

            else
            {
                PausedPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                Debug.Log("E to unpaused");
            }
        }
    }
    */

    public static bool GetIsPlaused()
    {
        return PauseManager.isPaused;
    }
    
}
