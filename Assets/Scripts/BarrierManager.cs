using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BarrierManager Instance;
    public List<Collider> barriers;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    void Start()
    {
        
        foreach(Collider barrier in GetComponentsInChildren<Collider>()){
            barriers.Add(barrier);
        }
    }

    public void TurnOffBarrier(int id)
    {
        if (id < barriers.Count)
            barriers[id].gameObject.SetActive(false);
    }

    public void TurnOnBarrier(int id)
    {
        if (id < barriers.Count)
            barriers[id].gameObject.SetActive(true);
    }
}
