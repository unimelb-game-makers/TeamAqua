using UnityEngine;
using UnityEngine.SceneManagement;

//======== this is the script to call EnterDialogueMode for cutscene 1 ====================
//NOTE: need sounds, maybe remove pausing.
public class OnSceneDialogueStarter : MonoBehaviour
{
    public DialogueScript script;

    private void Start()
    {
        DialogueManager.Instance().EnterDialogue(script);
        //AudioManager.Instance.Play("BGM_CUTSCENE_CEREMONY");
        // Create a temporary reference to the current scene.
    }
}
