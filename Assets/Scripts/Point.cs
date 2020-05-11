using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public static int pointCount = 0;
    public GameObject tangaParticle;
    public static Point point;
    public bool isTutorial = false;

    private void Awake()
    {
        if (point == null)
            point = this;
    }

    private void Update()
    {
        if(PlayerMovement.player != null)
        if (PlayerMovement.player.gameObject.transform.position.x > transform.position.x + 20.0f) 
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
            Instantiate(tangaParticle, transform.position, Quaternion.identity);
        if (col.gameObject.tag == "Player")
        {
            AudioManager.audioManager.PlaySound("coin");
            Destroy(gameObject);
            if(!isTutorial)
            SetPoint(1);
        }
    }

    public void SetPoint(int point)
    {
        pointCount += point;
        PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") + point);
        LevelGenerator.levelGenerator.SetPointText(pointCount);
    }
}
