using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    [Range(1, 100)] public int heal_hp = 1;

    private void OnDestroy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            HealingEntity pheal = player.GetComponent<HealingEntity>();
            pheal.heal(heal_hp);
        }
    }
}
