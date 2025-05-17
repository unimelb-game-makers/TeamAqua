using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject uiPanel;
    private bool _nearPlayer = false;

    private void Start()
    {
        SphereCollider trigger = gameObject.AddComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = 1f;

        uiPanel.SetActive(false);
    }

    private void Update()
    {
        if (_nearPlayer && DayManager.instance.CanChangeDay() && Input.GetKeyDown(KeyCode.E))
            DayManager.instance.StartNewDay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && DayManager.instance.CanChangeDay())
        {
            _nearPlayer = true;
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _nearPlayer = false;
            uiPanel.SetActive(false);
        }
    }
}
