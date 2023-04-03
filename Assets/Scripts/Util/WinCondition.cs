using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player p = player.GetComponent<Player>();

        p.win();
    }
}
