using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public Item item;

    [SerializeField] Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        //Render the components
        sprite = item.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
