using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    public bool can_push = true;
    public float move_distance = 0.2500f;
    public float tween_time = 0.25f;
    [NonSerialized] public Vector3 startPos;

    public void Start(){
        startPos = transform.position;
    }


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            PushDirection(other.transform.position);
        }
    }

    private void PushDirection(Vector3 playerPos){
        //Calculate the direction to move first
        double x_value = playerPos.x - transform.position.x;
        double z_value = playerPos.z - transform.position.z;
        bool is_x = Math.Abs(x_value) > Math.Abs(z_value);

        double playerValue;
        double blockValue;
        Vector3 direction;
        int dir;

        if(is_x){
            playerValue = playerPos.x;
            blockValue = transform.position.x;
            direction = new Vector3(1, 0, 0);
        }
        else{
            playerValue = playerPos.z;
            blockValue = transform.position.z;
            direction = new Vector3(0, 0, 1);
        }
        
        if(playerValue > blockValue)
            dir = -1;
        else
            dir = 1;
        
        direction *= dir;
        
        //Move the block if it is not facing an obstacle and currently tweening.
        if(!Physics.Raycast(transform.position, direction, move_distance, -1, QueryTriggerInteraction.Ignore)
            && can_push && !LeanTween.isTweening(gameObject)){
            LeanTween.move(gameObject, transform.position + direction * move_distance, tween_time);
        }
    }
}
