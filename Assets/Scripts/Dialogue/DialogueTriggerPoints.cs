using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTriggerPoints : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> TrigPoints;
    public bool collided;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Collider myCollider = other.GetContact(0).thisCollider;
        if (other.tag ==("Player"))
        {
            collided = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player") /*&& quest incomplete or non-existent*/)
        {
            collided = false;
        }
    }
}
