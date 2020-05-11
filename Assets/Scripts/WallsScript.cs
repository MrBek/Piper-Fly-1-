using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsScript : MonoBehaviour
{
    public static WallsScript wallsScript;
    public static bool permitGenerate = false;

    private void Awake()
    {
        if (wallsScript == null)
            wallsScript = this;
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(LevelGenerator.levelGenerator)
            LevelGenerator.levelGenerator.GenerateBarrier(this.gameObject);
            else
                Tutorial.tutorial.GenerateBarrier(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag != "devor_start")
        {
            Destroy(gameObject);
        }
        else if (gameObject.tag == "devor_start")
        {
            if(LevelGenerator.levelGenerator)
            LevelGenerator.levelGenerator.barrierStart.SetActive(false);
            else
                Tutorial.tutorial.barrierStart.SetActive(false);
        }
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }

}
