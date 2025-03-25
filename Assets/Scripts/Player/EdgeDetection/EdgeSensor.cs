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

    public Collider currentTileCollider;
    public Collider nextTileCollider;
    private Vector3 followPosition;
    private Vector3 offset;
    private bool followPlayer = false;
    bool started = false;
    // Enable the sensor to start detecting edges
    public void StartSensor()
    {
        // Raycast down and get a collider
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up * 20, out hit, ~8))
        {
            Debug.Log("Found an object: " + hit.collider);
            currentTileCollider = hit.collider;
        }
        offset = transform.localPosition;

        followPlayer = true;
    }

    public void FollowPlayer(Vector3 playerPosition, Vector3 saveDirection){
        if(followPlayer == false)
            return;
        
        followPosition = playerPosition + offset;
        if((currentTileCollider != null) || (currentTileCollider == null && saveDirection != blockDirection)){
            transform.position = followPosition;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("FloorTile") && currentTileCollider != other){
            nextTileCollider = other;
            if(currentTileCollider == null){
                currentTileCollider = nextTileCollider;
                nextTileCollider = null;
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if(other == currentTileCollider && nextTileCollider != null){
            currentTileCollider = nextTileCollider;
            nextTileCollider = null;
        }
        else if(other == currentTileCollider && nextTileCollider == null){
            currentTileCollider = null;
        }
    }
}
