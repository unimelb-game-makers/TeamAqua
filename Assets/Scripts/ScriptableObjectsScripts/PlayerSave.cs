using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSave", menuName = "ScriptableObjects/PlayerSave")]
public class PlayerSave : ScriptableObject
{
    public Vector2 playerPosition = Vector2.zero;
    public float energy = 0f;
    public int currentDay = 0;
    public bool overrideSaveFile = false;

    void OnEnable()
    {
        float playerPositionX = PlayerPrefs.GetFloat("playerPositionX");
        float playerPositionY = PlayerPrefs.GetFloat("playerPositionY");
        int savedCurrentDay = PlayerPrefs.GetInt("currentDay");
        float savedEnergyAmount = PlayerPrefs.GetFloat("energyAmount");

        if (!overrideSaveFile)
        {
            playerPosition = new Vector2(playerPositionX, playerPositionY);
            energy = savedEnergyAmount;
            currentDay = savedCurrentDay;
            return;
        }
    }
}
