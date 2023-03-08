using Unity.VisualScripting;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public float cooldown = 0.1f;
    float last;

    public int bullets = 1;

    public bool has_spread = true;
    public float spread = 0;

    Transform fire_trans;
    GameObject bullet_holder;

    public float y_angle = 0;

    public GameObject bullet;
    ParticleSystem particle;


    private void Start()
    {
        fire_trans = transform;

        bullet_holder = GameObject.FindGameObjectWithTag("BulletHolder");
        if (bullet_holder == null) Debug.LogWarning("No Bullet Holder found. Add one into your scene!");
        if (GetComponent<ParticleSystem>() != null) particle = GetComponent<ParticleSystem>();
    }


    public void fire()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        for (int i = 0; i < bullets; i++)
        {
            if (has_spread)
            {
                float number = UnityEngine.Random.RandomRange(-spread, spread);
                float angle = number;
                if (!(fire_trans.eulerAngles.z < -spread || fire_trans.eulerAngles.z > spread)) fire_trans.transform.eulerAngles = new Vector3(0, y_angle, angle);
                else fire_trans.transform.eulerAngles = new Vector3(0, y_angle, 0);
            }

            Instantiate(bullet, fire_trans.position, fire_trans.rotation, bullet_holder.transform);

            if (particle != null) particle.Emit(1);
        }
    }

    public void fireCircle()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        float rot = 360 / bullets;
        float current_rot;

        for (int i = 0; i < bullets; i++)
        {
            current_rot = fire_trans.eulerAngles.z;
            float new_rot = current_rot + rot;
            fire_trans.transform.eulerAngles = new Vector3(0, 0, new_rot);

            Instantiate(bullet, fire_trans.position, fire_trans.rotation, bullet_holder.transform);
        }
        
        if (particle != null) particle.Emit(1);
    }
}
