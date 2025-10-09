using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private PuzzleData data;
    public CinemachineVirtualCamera puzzleCam;
    public GameObject door;
    public Switch[] switches;
    public List<PushBlock> pushBlocks = new List<PushBlock>();
    public ResetBlock resetBlock;

    private bool playerEntered = false;
    private bool _completed = false;

    public string ID => data.name;

    private void Awake()
    {
        if (!data)
        {
            Debug.LogError($"PUZZLE | {name} is missing a puzzle data object.");
            enabled = false;
        }
        PuzzleManager.instance.Register(this);
    }

    private void Start()
    {
        // Load from PuzzleManager
        Load();

        //Turn the Puzzle Camera Off
        if (puzzleCam.gameObject.activeSelf)
        {
            puzzleCam.gameObject.SetActive(false);
        }
        //Set the parent puzzle for each switch
        foreach (var _switch in switches)
        {
            _switch.puzzle = this;
        }
        if (resetBlock)
            resetBlock.puzzle = this;
    }

    private void Load()
    {
        // If no save data for the puzzle, then return
        if (!PuzzleManager.instance.TryGetSaveData(data.name, out PuzzleSaveData saveData))
            return;

        // If there is a mismatch between the saved blocks and the current, don't try saving, let it override next time
        if (saveData.blocks.Length != pushBlocks.Count)
            return;

        // Set completed and the push block locations
        _completed = saveData.completed;
        Vector3 doorPos = door.transform.position;
        doorPos.y = GetClosedDoorPosition();
        door.transform.position = doorPos;
        for (int i = 0; i < pushBlocks.Count; ++i)
        {
            pushBlocks[i].transform.position = saveData.blocks[i].position;
        }
    }

    public PuzzleSaveData GetSaveData()
    {
        PuzzleSaveData saveData = new PuzzleSaveData();
        saveData.id = data.name;
        saveData.completed = _completed;
        saveData.blocks = new PuzzleBlockSaveData[pushBlocks.Count];
        for (int i = 0; i < pushBlocks.Count; ++i)
        {
            saveData.blocks[i].position = pushBlocks[i].transform.position;
        }
        return saveData;
    }

    public void ResetPuzzle()
    {
        foreach (var pb in pushBlocks)
        {
            pb.transform.position = pb.startPos;
        }
    }

    private bool IsFinished()
    {
        if (_completed)
            return true;
        //Check all Switches are on
        foreach (var _switch in switches)
        {
            if (!_switch.On)
                return false;
        }

        _completed = true;
        return true;
    }

    public void TryOpenDoor()
    {
        //If all switches are on, then open the door if it is not completed already
        if (_completed && IsFinished() && door)
            LeanTween.moveY(door, GetClosedDoorPosition(), 0.5f);
    }

    public float GetClosedDoorPosition()
    {
        return door.transform.position.y - 0.3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEntered = true;
            puzzleCam.gameObject.SetActive(true);

            if (AudioManager.Instance != null)
                AudioManager.Instance.Play("PUZZLE_ENTER");
            //AudioManager.Instance.SwapBGM("id", 5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEntered = false;
            puzzleCam.gameObject.SetActive(false);

            if (AudioManager.Instance != null)
                AudioManager.Instance.Stop("PUZZLE_ENTER");

            //Reset the puzzle if it is not completed
            if (!IsFinished())
                ResetPuzzle();
        }
    }
}
