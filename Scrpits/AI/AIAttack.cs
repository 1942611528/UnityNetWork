using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {
    // attack range
    public float range = 5;

    // attack interval
    public float interval = 1.5f;

    // tag of the unit that should be attacked
    public string enemyTag = "";

    // arrow prefab (to shoot at enemies)
    public GameObject arrow;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Fire", interval, interval);
    }

    void Fire()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            if (g != null)
            {		
                if (Vector3.Distance(g.transform.position, transform.position) <= range)
                {
                    // shoot arrow at it
                    GameObject a = (GameObject)Instantiate(arrow,
                                                           transform.position, // default position
                                                           Quaternion.identity); // default rotation
                    a.GetComponent<Arrow>().target = g.transform;
                    break;
                }
            }
        }
    }
}
