using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class UpdateHud : MonoBehaviour
{
    // - En dit is de gerefactorde code voor het updaten van de hud

    Entity entity;
    PlayerHud hud;
    PlayerBoost boost;

    private void Start()
    {
        hud = GetComponent<PlayerHud>();
        entity = GetComponent<Entity>();
        boost = GetComponent<PlayerBoost>();
    }

    // Update hud elements
    public void setHUD()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("PlayerHud");

        if (obj != null)
        {
            hud = obj.GetComponent<PlayerHud>();

            updateHudFuel(boost.fuel);
            updateHudHP(entity.hp);
        }
    }

    public void updateHudFuel(float fuel_value)
    {
        if (hud != null)
        {
            hud.Fuelvalue = fuel_value;
        }
    }

    public void updateHudHP(int hp_value)
    {
        if (hud != null)
        {
            hud.HPvalue = hp_value;
        }
    }
}
