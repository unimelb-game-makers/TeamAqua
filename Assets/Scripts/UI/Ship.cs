using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject uiPanel;

    private void Start()
    {
        SphereCollider trigger = gameObject.AddComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = 1f;

        uiPanel.SetActive(false);
    }

    private void Update()
    {
        if (DayManager.instance.CanChangeDay() && Input.GetKeyDown(KeyCode.E))
            DayManager.instance.StartNewDay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && DayManager.instance.CanChangeDay())
        {
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
        }
    }
}
