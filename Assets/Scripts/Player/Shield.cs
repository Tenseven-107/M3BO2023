using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Shield : MonoBehaviour
{
    public int hp = 3;
    public int max_hp = 3;
    public int team = 0;

    bool recharging = false;
    public float max_charge = 100;
    float charge;

    Collider2D collider;
    Animator anims;


    private void Start()
    {
        collider = GetComponent<Collider2D>();
        anims = GetComponent<Animator>();

        recharging = false;
        charge = 100;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }


    public void handleHit(int damage)
    {
        if (hp > 0 && !recharging)
        {
            hp -= damage;

            if (hp <= 0)
            {
                recharging = true;
                charge = 0;

                collider.enabled = false;

                anims.SetTrigger("Dissapear");
                anims.ResetTrigger("Appear");
            }
        }
    }

    void Update()
    {
        if (recharging && charge < max_charge)
        {
            charge += 0.1f;
            charge = Mathf.Clamp(charge, 0, max_charge);

            if (charge >= max_charge)
            {
                hp = max_hp;
                recharging = false;

                collider.enabled = true;

                anims.ResetTrigger("Dissapear");
                anims.SetTrigger("Appear");
            }
        }
    }
}
