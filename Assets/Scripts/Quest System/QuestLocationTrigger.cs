using UnityEngine;

public class QuestLocationTrigger : MonoBehaviour
{
    public QuestStep step;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider hit, checking quest step status... ");
        if (other.CompareTag("Player"))
        {
            QuestManager.instance.CompleteStep(step);
        }
    }
}
