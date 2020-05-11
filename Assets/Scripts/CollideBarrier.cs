using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideBarrier : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.PlayerKill();
        }
    }

}
