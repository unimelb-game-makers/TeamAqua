using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    private EnergyManager EnergyMana;
    void Start()
    {
        EnergyMana = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnergyMana.EnergyAmount <= 0)
        {
            //placeholder for now, will change later when restart mechanic is made clear
            Destroy(gameObject);     
        }
    }
}
