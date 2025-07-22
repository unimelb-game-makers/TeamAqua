using UnityEngine;

// TODO: This is not really used.
public class NPC : MonoBehaviour
{
    public ID id;

    private void Awake()
    {
        DayManager.instance.RegisterNpc(this);
    }
}
