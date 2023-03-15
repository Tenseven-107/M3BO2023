using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Portal : MonoBehaviour
{
    public string next_scene_name;

    public bool boss = false;
    bool boss_spawned = false;
    public GameObject boss_obj;
    GameObject final_boss = null;

    [HideInInspector] public int child_count;
    bool active = false;

    SpriteRenderer sprite;
    Animator anims;

    PlayerHud hud;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anims = GetComponent<Animator>();

        child_count = transform.childCount;

        sprite.enabled = false;
        active = false;

        Invoke("setHUD", 1);

        if (gameObject.tag != "Portal") gameObject.tag = "Portal";
    }


    void OnTransformChildrenChanged()
    {
        child_count = transform.childCount;

        setHudAltars();
        
        if (child_count <= 0)
        {
            if (boss_spawned || !boss)
            {
                active = true;
                sprite.enabled = true;

                anims.Play("Portal_appear");
            }
            else spawnBoss();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && active)
        {
            SceneLoader loader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
            loader.transition(next_scene_name);
        }
    }


    private void spawnBoss()
    {
        if (final_boss == null)
        {
            final_boss = Instantiate(boss_obj, transform);
            boss_spawned = true;

            GameObject[] managers = GameObject.FindGameObjectsWithTag("SpawnManager");
            foreach (GameObject manager in managers)
            {
                SpawnManager m = manager.GetComponent<SpawnManager>();
                m.active = false;
            }
        }  
    }



    void setHUD()
    {
        hud = GameObject.FindGameObjectWithTag("PlayerHud").GetComponent<PlayerHud>();

        setHudAltars();
    }

    void setHudAltars()
    {
        if (hud != null)
        {
            hud.Altarvalue = transform.childCount;
        }
    }
}
