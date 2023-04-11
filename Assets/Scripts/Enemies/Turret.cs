using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public BulletShooter wp;
    public Area area;

    public bool circle = true;
    public bool follow_target = false;
    public bool kill = false;

    void Update()
    {
        if (area.is_colliding)
        {
            if (circle) wp.fireCircle();
            else wp.fire();

            if (follow_target)
            {
                GameObject target = area.getObject();
                Vector2 relative = transform.InverseTransformPoint(target.transform.position);
                float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
                wp.transform.Rotate(0, 0, -angle);
            }

            if (kill) Destroy(gameObject);
        }
    }
}
