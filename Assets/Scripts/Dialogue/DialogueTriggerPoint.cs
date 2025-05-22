using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerPoint : MonoBehaviour
{
    public List<DialogueNode> dialogues;
    bool collided = false;   // to record which trig point has alr been hit, avoiding the same trig point activating multiple times

    private bool TryGetNode(string dialogueId, out DialogueNode node)
    {
        for (int i = 0; i < dialogues.Count; ++i)
        {
            if (dialogues[i].name == dialogueId)
            {
                node = dialogues[i];
                return true;
            }
        }

        node = null;
        return false;
    }

    public bool PlayDialogue()
    {
        string dialogue = DialogueManager.instance.DialogueId;
        if (TryGetNode(dialogue, out DialogueNode node))
        {
            DialogueManager.instance.EnterDialogue(node, DialogueMode.Moving);
            return true;
        }

        return false;
    }
    /*
    This will be automatically called on Trigger by NPCDialogueHandler
    */
    public void TriggerDialogue(){
        if (collided) return;
        Debug.Log("Entering dialogue trigger point child mode");
        DialogueManager.instance.EnterDialogue(dialogues[0], DialogueMode.Moving);
        collided = true;    // remembers that this trig point has already been collided
        this.gameObject.SetActive(false);
    }
}
