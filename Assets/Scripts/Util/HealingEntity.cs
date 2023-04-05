using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingEntity : MonoBehaviour
{
    Entity entity;
    int hp;
    int max_hp;

    public GameObject heal_effect;
    public RandomAudio audio;


    private void Start()
    {
        entity = GetComponent<Entity>();
        hp = entity.hp;
        max_hp = entity.max_hp;
    }


    public void heal(int added_hp)
    {
        hp = entity.hp;
        max_hp = entity.max_hp;

        if (hp < max_hp) entity.hp += added_hp;
        if (heal_effect != null)
        {
            Transform parent = transform.parent;
            Instantiate(heal_effect, transform.position, Quaternion.Euler(0, 0, 0), parent);
        }

        if (audio != null) audio.playSound();

        if (GetComponent<Player>() != null)
        {
            Player player = GetComponent<Player>();
            player.setHUD();
        }
    }
}
