using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalManager : MonoBehaviour
{
    
    public static JournalManager journalMana;
    [SerializeField] public GameObject journalCanvas; // the canvas that displays the quests
    private List<QuestData> quests = new List<QuestData>(); // list of all the quests the player has
    [SerializeField] private TextMeshProUGUI journalText; // the text that displays the quests
    [SerializeField] private RectTransform Scroll_View_rect_transform; // the rect transform of the scroll view
    private bool isScaled = false;
    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        OnDisableJ();
    }

    private void Awake()
    {
        journalMana = this;
    }
    public static JournalManager instance()
    {
        return journalMana;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && UIinputProvider.instance().UI_canOpen[4] && !DialogueSystem.GetIsPlaying())  //added bool check to see if dialogue is on
        {
            if (!journalCanvas.activeSelf)
            {
                OnEnableJ();
            }
            
            else
            {
                OnDisableJ();
            }
        }

        if (DialogueSystem.GetIsPlaying() || PausePanelScript.instance().isPaused) // forcibly closes journal if player enters dialogue
        {
            OnDisableJ();
        }

        if (journalCanvas.activeSelf && !isScaled)
        {       // horizontal fade animation
            Scroll_View_rect_transform.localScale = Scroll_View_rect_transform.localScale + new Vector3(0.05f, 0, 0);
            if (Scroll_View_rect_transform.localScale.x >= 1)
            {
                isScaled = true;
            }
        }
        
    }

    public void OnEnableJ()
    {
        journalCanvas.SetActive(true);
        UIinputProvider.instance().SendUIinput(4);
        isOpen = true;
        Scroll_View_rect_transform.localScale = new Vector3(0, 1, 1); // reset scale for animation
        isScaled = false;
        Time.timeScale = 0; // pause the game when the journal canvas is active
    }

    public void OnDisableJ()
    {
        isOpen = false;
        Scroll_View_rect_transform.localScale = new Vector3(0, 1, 1); // reset scale for animation
        journalCanvas.SetActive(false);
        UIinputProvider.instance().SendUIinput(0);
        Time.timeScale = 1; // resume game when journal canvas is deactivated
    }
}

