using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMainHolder : MonoBehaviour
{
    public GameObject fadeImage;
    public GameObject dayText;

    private void Start()
    {
        // Deactivate the main holder at the start
        fadeImage.SetActive(false);
        dayText.SetActive(false);
    
    }

    private void Update()
    {
        if (Ship.isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShowForSeconds(6f)); // Show and then hide after 6 seconds
        }
    }

    IEnumerator ShowForSeconds(float delay)
    {
        // Activate objects
        fadeImage.SetActive(true);
        dayText.SetActive(true);
        
        // Wait for 6 seconds
        yield return new WaitForSeconds(delay);

        // Deactivate objects
        fadeImage.SetActive(false);
        dayText.SetActive(false);
    }
}
