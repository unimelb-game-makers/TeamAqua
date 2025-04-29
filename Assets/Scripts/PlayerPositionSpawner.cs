using UnityEngine;

public class PlayerPositionSpawner : MonoBehaviour
{
    void Start()
    {
        float playerPositionX = PlayerPrefs.GetFloat("playerPositionX");
        float playerPositionY = PlayerPrefs.GetFloat("playerPositionY");
        if (playerPositionX == 0f && playerPositionY == 0f)
        {
            return;
        }
        transform.position = new Vector3(playerPositionX, playerPositionY, transform.position.z);
    }
}
