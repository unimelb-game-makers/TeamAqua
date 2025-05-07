using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Once exited, show next indicator.

*/

public class TutorialIndicatorMessage : MonoBehaviour
{
    public TutorialIndicatorMessage nextIndicator;
    public Vector3 playerOffset; // position to spawn indicator from the player
    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
        if(gameObject.activeSelf){
            StartIndicate();
        }
    }

    void StartIndicate(Transform playerTransform = null){
        if(playerTransform != null)
            transform.position = playerTransform.position + playerOffset;
        anim = GetComponent<Animator>();
        anim.SetBool("indicate", true);
    }

    void StopIndicate(){
        Vector3 savePos = transform.position;
        anim.SetBool("indicate", false);
        transform.position = savePos;
    }

    IEnumerator TransitionNextIndicator(Transform playerTransform){
        yield return new WaitForSeconds(2.25f);
        nextIndicator.gameObject.SetActive(true);
        nextIndicator.StartIndicate(playerTransform);
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            StopIndicate();
            if(nextIndicator != null){
                StartCoroutine(TransitionNextIndicator(other.transform));
            }
        }
    }
}
