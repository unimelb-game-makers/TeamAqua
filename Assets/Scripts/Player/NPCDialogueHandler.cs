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
        _dialogueSource.PlayDialogue();
        CheckTag();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Creature"))
            return;

        if (other.gameObject.TryGetComponent(out NPCDialogue dialogue))
        {
            _dialogueSource = dialogue;
            if (!_dialogueSource)
                return;
            _dialogueSource.ShowIndicator();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Creature") && _dialogueSource != null)
        {
            _dialogueSource.HideIndicators();
            _dialogueSource = null;
            DialogueManager.Instance().npcData = null;
            DialogueManager.Instance().currentStory = null;
        }
    }

    private void CheckTag()
    {
        if (DialogueManager.Instance().currentStory == null)
        {
            Debug.Log("no storry found");
            return;
        }

        // Defensive programming to avoid null exceptions
        if (!_dialogueSource)
            return;
        if (!_dialogueSource.npcData)
            return;
    }
}
