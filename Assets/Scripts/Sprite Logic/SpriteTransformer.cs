/*
This script is to be used for directing sprite orientation.
Such as flipping sprites. 
Thus, preventing repeating code each time a sprite needs to be flipped.

Simply add this script and reference it in the sprite GameObject.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTransformer : MonoBehaviour
{
    [SerializeField] bool billboard = false;

    Vector3 spriteScale;

    // Start is called before the first frame update
    void Start()
    {
        spriteScale = transform.localScale;
    }

    void Update(){
        if(billboard == false || SpriteManager.instance == null)
            return;
        //print($"Billboard = {billboard}, SpriteManager = {SpriteManager.instance}");
        // Choose which axis to rotate on.
        if(SpriteManager.instance.freezeXYAxis == true){
            transform.localRotation = Quaternion.Euler(0f, Camera.main.transform.localRotation.eulerAngles.y, 0f);
            print("here");
        } else{
            transform.localRotation = Camera.main.transform.rotation;
        }
    }

    public void flipX(bool flip){
        if(flip){
            transform.localScale = new Vector3(-spriteScale.x, spriteScale.y, spriteScale.z);
        } else{
            transform.localScale = spriteScale;
        }
    }
}
