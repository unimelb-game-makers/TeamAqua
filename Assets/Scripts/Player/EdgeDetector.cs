using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    [Header("Edge Detection")]
    [SerializeField] private float _edgeCheckDistance = 0.5f;
    [SerializeField] private float _tiltAngle = 2.5f;
    [SerializeField] private float _edgeCheckRadius = 0.2f;
    [SerializeField] private int _edgeCheckRays = 3; // More rays = more precision
    [SerializeField] private float _edgeRaySpread = 30f; // Width of the ray fan
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Debug")]
    [SerializeField] private bool _showDebugRays = true;
    
    /// Checks if movement in a direction is safe (no edges)
    public bool CanMoveInDirection(Vector3 direction)
    {
        if (!IsGrounded()) return false; // Only check edges if grounded
        return !IsEdgeInDirection(direction.normalized);
    }

    /// Simple ground check
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 
            _edgeCheckDistance * 1.2f, _groundLayer);
    }

    /// Checks for edges in movement direction
    private bool IsEdgeInDirection(Vector3 direction)
    {
        float halfSpread = _edgeRaySpread * 0.5f;
        float angleStep = _edgeRaySpread / (_edgeCheckRays - 1);

        for (int i = 0; i < _edgeCheckRays; i++)
        {
            float angle = -halfSpread + (i * angleStep); // Fix this angle
            Vector3 rayDir = Quaternion.Euler(0, angle, 0) * direction;
            rayDir.y = -1 * _tiltAngle; // Angle slightly downward

            if (_showDebugRays)
                Debug.DrawRay(transform.position, rayDir.normalized * _edgeCheckDistance, Color.red, 0.1f);

            if (!Physics.Raycast(transform.position, rayDir, _edgeCheckDistance, _groundLayer))
            {
<<<<<<< HEAD
                Debug.LogWarning($"Edge detected on ray {i} (angle: {angle}°)");
                return true; // Edge detected
            }
            //Debug.Log("here");
            
        }
        //Debug.Log("here");
=======
                //Debug.LogWarning($"Edge detected on ray {i} (angle: {angle}°)");
                return true; // Edge detected
            }            
        }
>>>>>>> main
        return false; // No edge
    }
}
