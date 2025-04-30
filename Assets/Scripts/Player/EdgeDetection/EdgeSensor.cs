// Get the collider of the current tile
// Set position to new tile
// If current tile, follow the player

// If exited tile collider and no new tile collider, then stop moving

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeSensor : MonoBehaviour
{
    public Vector3 blockDirection;
    [NonSerialized] public PlayerController playerController;

    public Collider currentTileCollider;
    private Vector3 followPosition;
    private Vector3 offset;
    private bool followPlayer = false;

    // Enable the sensor to start detecting edges
    public void StartSensor(PlayerController _playerController)
    {
        // Raycast down and get a collider
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            Debug.Log("Found an object - distance: " + hit.distance);
            currentTileCollider = hit.collider;
        }
        offset = transform.localPosition;

        playerController = _playerController;
        followPlayer = true;
    }

    public void FollowPlayer(){
        if(followPlayer == false)
            return;
        
        followPosition = playerController.transform.position + offset;
        if((playerController != null && currentTileCollider != null) || 
            (playerController != null && currentTileCollider == null && playerController.saveDirection != blockDirection)){
            transform.position = followPosition;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("FloorTile") && currentTileCollider != other){
            currentTileCollider = other;
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if(other == currentTileCollider){
            currentTileCollider = null;
        }
    }
}
