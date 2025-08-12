using System.Collections.Generic;
using UnityEngine;

public class ItemHarvester : MonoBehaviour
{
    private readonly List<ItemHarvestSource> _sources = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (DialogueManager.Instance().State == DialogueState.None) && _sources.Count > 0)
        {
            // Get the most recent source that entered
            ItemHarvestSource mostRecentSource = _sources[^1];
            if (mostRecentSource.HarvestResource(out Item item))
            {
                InventoryManager.instance.AddItem(item);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItemResource") && other.gameObject.TryGetComponent(out ItemHarvestSource source))
        {
            // Hide all sources
            for (int i = 0; i < _sources.Count; ++i)
                _sources[i].OnPlayerExit();

            // Add the new source and show it 
            _sources.Add(source);
            source.OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ItemResource") && other.gameObject.TryGetComponent(out ItemHarvestSource harvestSource))
        {
            // Remove the source from the list
            harvestSource.OnPlayerExit();
            _sources.Remove(harvestSource);
            
            // Show the most recent source and hide the others
            for (int i = 0; i < _sources.Count; ++i)
            {
                if (i == _sources.Count - 1)
                    _sources[i].OnPlayerEnter();
                else
                    _sources[i].OnPlayerExit();
            }
        }
    }
}