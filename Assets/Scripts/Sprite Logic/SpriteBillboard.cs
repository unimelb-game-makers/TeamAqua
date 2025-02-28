using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Choose which axis to rotate on.
        if(SpriteManager.spriteManager.freezeXYAxis == true){
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        } else{
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
