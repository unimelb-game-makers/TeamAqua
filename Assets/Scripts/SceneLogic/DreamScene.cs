using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamScene : MonoBehaviour
{
    public string BG_MUSIC_1 = "BGM_A_DREAM";
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play(BG_MUSIC_1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
