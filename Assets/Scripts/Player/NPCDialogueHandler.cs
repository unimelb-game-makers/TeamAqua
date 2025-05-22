using System;
using Popups;
using UnityEngine;

public class NPCDialogueHandler : MonoBehaviour
{
    private NPCDialogue _dialogueSource = null;

    private void Update()
    {
        if (_dialogueSource == null)
            return;
        if (DialogueManager.GetIsPlaying())
            return;
        if (UIController.Paused)
            return;
        if (Input.GetKeyDown(KeyCode.E))
            if (_dialogueSource.PlayDialogue())
            {
                _dialogueSource.HideIndicators();
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Creature")){
            if (other.gameObject.TryGetComponent(out NPCDialogue dialogue))
            {
                _dialogueSource = dialogue;
                if (!_dialogueSource)
                    return;
                _dialogueSource.ShowIndicator();
            }
        } else if (other.gameObject.CompareTag("DialogueTrigger")){
            if (other.gameObject.TryGetComponent(out DialogueTriggerPoint dialogueTrig))
            {
                dialogueTrig.TriggerDialogue();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Creature") && _dialogueSource != null)
        {
            _dialogueSource.HideIndicators();
            _dialogueSource = null;
            DialogueManager.Instance().currentStory = null;
        }
    }
}
