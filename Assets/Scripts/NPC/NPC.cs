using UnityEngine;

// TODO: This is not really used.
public class NPC : MonoBehaviour
{
    public ID id;

    private void Awake()
    {
        SetupTriggerLayerOverrides();
        DayManager.instance.RegisterNpc(this);
    }

    private void SetupTriggerLayerOverrides()
    {
        Debug.LogWarning($"intializing Player layer inclusion {gameObject.name}");

        // Get all colliders attached to this game object
        Collider[] colliders = GetComponents<Collider>();

        if (colliders.Length == 0)
        {
            Debug.LogWarning($"No colliders found on {gameObject.name}");
            return;
        }

        // Get the Player layer index
        int playerLayer = LayerMask.NameToLayer("Player");

        if (playerLayer == -1)
        {
            Debug.LogWarning("Player layer not found in project layers");
            return;
        }

        int triggerCollidersProcessed = 0;

        // Loop through all colliders and check if any are triggers
        foreach (Collider collider in colliders)
        {
            if (collider.isTrigger)
            {
                //collider.includeLayers = LayerMask.NameToLayer("Player");
                // Get current layer overrides or create new one if none exists
                int currentOverrides = collider.layerOverridePriority;

                // Add Player layer to the override mask
                int playerLayerMask = 1 << playerLayer;
                collider.layerOverridePriority = currentOverrides | playerLayerMask;

                triggerCollidersProcessed++;
                Debug.Log(
                    $"Added Player layer override to trigger collider #{triggerCollidersProcessed} on {gameObject.name}"
                );
            }
        }

        if (triggerCollidersProcessed == 0)
        {
            Debug.Log($"No trigger colliders found on {gameObject.name}");
        }
    }
}
