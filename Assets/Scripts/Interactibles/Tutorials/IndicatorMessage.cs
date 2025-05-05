using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Once exited, show next indicator.

*/

public class IndicatorMessage : MonoBehaviour
{
    public IndicatorMessage nextIndicator;
    public Animator anim;

    void Start(){
        if(gameObject.activeSelf){
            StartIndicate();
        }
    }

    void StartIndicate(){
        anim.SetBool("indicate", true);
    }

    void StopIndicate(){
        Vector3 savePos = transform.position;
        anim.SetBool("indicate", false);
        transform.position = savePos;
    }

    IEnumerator TransitionNextIndicator(){
        yield return new WaitForSeconds(2.25f);
        nextIndicator.gameObject.SetActive(true);
        nextIndicator.StartIndicate();
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            StopIndicate();
            if(nextIndicator != null){
                StartCoroutine(TransitionNextIndicator());
            }
        }
    }
}
