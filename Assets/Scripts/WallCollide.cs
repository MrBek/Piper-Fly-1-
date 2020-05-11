using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement.player.PlayerKill();
            WheelIn.combo = 0;
        }
    }
}
