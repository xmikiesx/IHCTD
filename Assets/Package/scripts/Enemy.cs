using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Bolt.GlobalEventListener
//public class Enemy : MonoBehaviour
{
    public int life;
    public int impact;
    public TowerBolt target;
    public float attackFrequency;

    private NavMeshAgent agent;
    private bool onAttack;
    private Animator anim;
    public GameObject parent;

    //  public GameObject impactParticleEffect;
    public GameObject coinsObject;
    public delegate void OnScoreChange(int value);
    public static event OnScoreChange onScoreChange;
    public int score;
    // Start is called before the first frame update
    void OnEnable()
    {
        //transform.SetParent(parent.transform);
        //     GameObject particle = Instantiate(impactParticleEffect, transform.position, Quaternion.identity) as GameObject;
        //   Destroy(particle, 2);
    }
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        target = GameObject.Find("TowerBolt(Clone)").GetComponent<TowerBolt>();
        Debug.Log("CERCAAAAAAA!!!!");
        Debug.Log(GameObject.Find("Tower"));
        agent = GetComponent<NavMeshAgent>();
        goTO();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathComplete() && !onAttack)
        {
            onAttack = true;
            //  Debug.Log("agent arrived");
            manageAttack();
            transform.LookAt(target.transform.position);
        }
    }
    public void damage(int value)
    {
        life += value;
        if (life <= 0)
        {
            //Destroy(gameObject);
            //GameObject particle = Instantiate(coinsObject, transform.position, Quaternion.identity) as GameObject;
            int random = Random.Range(0, 3);
            if (onScoreChange != null)
            {
                onScoreChange(5);
            }
            if (random == 1)
            {
                GameObject coinPrefab = BoltNetwork.Instantiate(BoltPrefabs.Coin, transform.position, Quaternion.identity);
            }
            else if (random == 2)
            {
                GameObject coinPrefab = BoltNetwork.Instantiate(BoltPrefabs.Coin, transform.position, Quaternion.identity);
            }
            else if (random == 3)
            {
                GameObject energyPrefab = BoltNetwork.Instantiate(BoltPrefabs.Energy, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject energyPrefab = BoltNetwork.Instantiate(BoltPrefabs.Energy, transform.position, Quaternion.identity);
            }
            BoltNetwork.Destroy(gameObject);
            Destroy(gameObject);

        }
    }

    public void goTO()
    {
        agent.SetDestination(target.gameObject.transform.position);
        anim.SetBool("walk", true);
        anim.SetBool("attack", false);
    }

    public bool pathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            anim.SetBool("walk", false);
            anim.SetBool("attack", true);
            return true;
        }
        return false;
    }

    public void manageAttack()
    {
        StartCoroutine(lauchAttack());
    }

    public void stopAttack()
    {
        onAttack = false;
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
    }

    IEnumerator lauchAttack()
    {
        while (onAttack)
        {
            target.damage(-impact);
            yield return new WaitForSeconds(attackFrequency);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        //first disable the zombie's collider so multiple collisions cannot occur
        //destroy the bullet
        if (col.name == "Bullet_45mm_Bullet(Clone)")
        {
            //GetComponent<BoxCollider>().enabled = false;


            //stop the walking animation and play the falling back animation
            //GetComponent<Animator>().Play("fallingback");
            //destroy this zombie in six seconds.
            //Destroy(gameObject, 6);
            /*if (onScoreChange != null)
            {
                onScoreChange(5);
            }*/
            //BoltNetwork.Destroy(gameObject);
            damage(-10);
            //instantiate a new zombie
            //if the zombie gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
            //so keep repositioning the zombie until it is greater than 3 scene units away. 
        }
        else if(col.gameObject.CompareTag("Projectile")) 
        {
            BoltNetwork.Destroy(gameObject);
        }
    }
}