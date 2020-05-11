using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidePlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerMovement.player.PlayerKill();       
        }
    }
}
