using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPos;
    GameObject goob;
    public GameObject parent;

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Enemy" && goob == null)
        {
            goob = obj.gameObject;
            InvokeRepeating("Shoot", 0, 4.0f);
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(bullet, spawnPos.position, spawnPos.rotation) as GameObject;
        //projectile.transform.SetParent(parent.transform);

        projectile.GetComponent<Projectile>().PosInit = spawnPos.position;
        projectile.GetComponent<Projectile>().PosEnd = goob.transform.position;

        //     this.GetComponent<AudioSource>().Play();
        //    if (goob.GetComponent<Move>().dead)
        //    {
        //        goob = null;
        //        CancelInvoke("Shoot");
        //    }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject == goob)
        {
            goob = null;
            CancelInvoke("Shoot");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (goob != null)
            spawnPos.transform.LookAt(goob.transform.position);
    }
}