using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    public static int k = 0;
    public static Wheel wheel;
    public static bool firstCollide = false;

    private void Awake()
    {
        if (wheel == null)
            wheel = this;
    }
    
    public void recieveTriggerEnter(string fromObject, Collider col)
    {

        if (!firstCollide)
            if (fromObject == "TriggerWheel")
            {
                firstCollide = true;
                if (col.tag == "Player")
                {
                    Score(1);
                    WheelIn.combo = 0;
                    AudioManager.audioManager.PlaySound("portal" + WheelIn.combo.ToString());
                }
            }

        if (!firstCollide)
            if (fromObject == "TriggerCilinder")
            {
                firstCollide = true;
                if (col.tag == "Player")
                {
                    if (WheelIn.combo < 3)
                    {
                        Score(2);
                        AudioManager.audioManager.PlaySound("portal" + WheelIn.combo.ToString());
                        WheelIn.combo++;
                    }
                    else if (WheelIn.combo >= 3)
                    {
                        Score(4);
                        AudioManager.audioManager.PlaySound("portal" + 3);
                    }
                }
            }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!firstCollide)
            {
                WheelIn.combo = 0;
            }
        }
    }

    public void Score(int add)
    {
        k += add;
        if(LevelGenerator.levelGenerator)
        LevelGenerator.levelGenerator.SetScoreText(k);
    }
}
