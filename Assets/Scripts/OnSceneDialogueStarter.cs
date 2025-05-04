using UnityEngine;
using UnityEngine.SceneManagement;

//======== this is the script to call EnterDialogueMode for cutscene 1 ====================
//NOTE: need sounds, maybe remove pausing.
public class OnSceneDialogueStarter : MonoBehaviour
{
    public static OnSceneDialogueStarter Instance;

    //public UnityEngine.UI.Image background;
    //  public Sprite[] sprites;
    [SerializeField]
    public TextAsset inkJSON;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        DialogueManager.Instance().EnterDialogueMode(inkJSON, 0);
        //AudioManager.Instance.Play("BGM_CUTSCENE_CEREMONY");

        // Create a temporary reference to the current scene.
    }

    // Update is called once per frame
    void Update() { }

    public void SceneChanger(string SceneName)
    {
        //loads the next scene, its in a function so it can be called within ink.
        SceneManager.LoadScene(SceneName);
        Debug.Log("scene changed to " + SceneManager.GetActiveScene());
    }
}
