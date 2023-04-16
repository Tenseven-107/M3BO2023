using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAltarsInvis : MonoBehaviour
{
    public bool set_invis = false;

    void Start()
    {
        if (set_invis) Invoke("Init", 1);
    }

    void Init()
    {
        PlayerHud hud = GameObject.FindGameObjectWithTag("PlayerHud").GetComponent<PlayerHud>(); ;
        if (hud != null) hud.setBoss();
    }
}
