using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIinputProvider : MonoBehaviour
{
    public static UIinputProvider UIprovider;
    // script to organize different UI, recognize when one is open and not let others be opened while it still is.
    [SerializeField]    //really not sure how to do this cleanly without abusing bools :(((
                        //logic: if one is true, set rests to false
                        //exceptions: pause and dialogue, they override and forcibly closes other canvases
    public bool[] UI_canOpen = new bool[7]{true, true, true, true, true, true, true};   
    //GUIDE --> 0: all true; 1: inventory; 2: quest; 3: map; 4: journal; 5: dialogue panel
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        UIprovider = this;
    }

    public static UIinputProvider instance()
    {
        return UIprovider;
    }

    public void SendUIinput(int UIindex)
    {   // UIindex is the UI we want open, set its bool to true while rests are set to false
        for(int i = 0; i < UI_canOpen.Length; i++)
        {
            if (UIindex == 0)
            {
                for (int j = 0; j < UI_canOpen.Length; j++)
                {
                    UI_canOpen[j] = true;
                }
            }
            if (i == UIindex)
            {
                UI_canOpen[i] = true;
            }
            else
            {
                UI_canOpen[i] = false;
            }
        }

    }    
}
