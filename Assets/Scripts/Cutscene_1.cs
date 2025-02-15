using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;



//======== this is the script to call EnterDialogueMode for cutscene 1 ====================
//NOTE: need sounds, maybe remove pausing.
public class Cutscene_1 : MonoBehaviour
{
    public static Cutscene_1 Instance;
    //public UnityEngine.UI.Image background;
  //  public Sprite[] sprites;
    [SerializeField] public TextAsset inkJSON;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        DialogueSystem.Instance().EnterDialogueMode(inkJSON, 0);
        //AudioManager.Instance.Play("BGM_CUTSCENE_CEREMONY");

        // Create a temporary reference to the current scene.
		
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(AudioManager.Instance.SwapBGM("BGM_CUTSCENE_INTO_THE_STORM", 1));
            Debug.Log("music changed");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.Stop("BGM_CUTSCENE_CEREMONY");
            Debug.Log("music stopped");
        }
    }

    public void SceneChanger(string SceneName)
    {
        //loads the next scene, its in a function so it can be called within ink.
        SceneManager.LoadScene(SceneName);
        Debug.Log("scene changed to " + SceneManager.GetActiveScene());
    }

}
