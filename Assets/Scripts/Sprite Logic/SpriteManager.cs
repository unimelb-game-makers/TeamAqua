using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager spriteManager;

    // Following variables going to be checked by all the Sprites that have SpriteBillboard.cs script.
    // As a way to set ALL sprites to follow the same billboard rules.
    [SerializeField] public bool billboard = true; // Whether the sprites billboard at all.
    [SerializeField] public bool freezeXYAxis = true; // Which axis the sprites are being rotated on.

    // Start is called before the first frame update
    void Start()
    {
        spriteManager = this;
    }
}
