using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelIn : MonoBehaviour
{
    public static WheelIn wheelIn;
    public static int combo = 0;
    public GameObject particleSimple, particleBonus, particleCombo;

    private void Awake()
    {
        if (wheelIn == null)
            wheelIn = this;
    }
    void OnTriggerEnter(Collider col)
    {
        if (gameObject.name == "TriggerCilinder")
        {
            if (!Wheel.firstCollide)
            {
                if (combo >= 3)
                {
                    Instantiate(particleCombo, transform.position, Quaternion.identity);
                }
                else
                    Instantiate(particleBonus, transform.position, Quaternion.identity);
            }
        }
        else if (gameObject.name == "TriggerWheel")
            if (!Wheel.firstCollide)
                Instantiate(particleSimple, transform.position, Quaternion.identity);

        Wheel.wheel.recieveTriggerEnter(name, col);
    }

    private void OnTriggerExit(Collider col)
    {
        if (name == "TriggerWheel")
            Wheel.firstCollide = false;
    }
}
