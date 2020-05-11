using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetPosition;
    Vector3 currentVelocity;
    PlayerMovement player;

    private void Start()
    {
        Application.targetFrameRate = 300;
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.playerKill)
            transform.position = new Vector3(targetPosition.position.x - 5.5f, transform.position.y, transform.position.z);
    }
}
