using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public Entity entity;

    private void OnDestroy()
    {
        if (entity != null && entity.hp <= 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Player p = player.GetComponent<Player>();
                p.win();
            }
        }
    }
}
