using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    /*
        Fade in when activated
        Play spin animation
    */
    public Orb nextOrb;
    public float normalRotationSpeed = 10;
    public float fastRotationSpeed = 50;

    [SerializeField] SpriteRenderer spriteRenderer;

    private float rotateSpeed;

    private void Start() {
        // Color tmp = spriteRenderer.color;
        // tmp.a = 0f;
        // spriteRenderer.color = tmp;

        rotateSpeed = normalRotationSpeed;
    }

    private void Update() {
        transform.Rotate(0,0,rotateSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        // Tween scale big
        if(other.CompareTag("Player")){
            LeanTween.scale(gameObject, new Vector3(.1f, .1f, 1), .75f);
            rotateSpeed = fastRotationSpeed;
            //Debug.Log("Player Entered");
        }
    }

    private void OnTriggerExit(Collider other) {
        // Tween scale small
        if(other.CompareTag("Player")){
            LeanTween.scale(gameObject, new Vector3(.05f, .05f, 1), .75f);
            rotateSpeed = normalRotationSpeed;
            //Debug.Log("Player Exited");
        }
    }
}
