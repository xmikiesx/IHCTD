using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Bolt.GlobalEventListener
{
    public int life;
    public int impact;
    public Tower target;
    public float attackFrequency;

    private NavMeshAgent agent;
    private bool onAttack;
    private Animator anim;

  //  public GameObject impactParticleEffect;

    // Start is called before the first frame update
    private void OnEnable()
    {
   //     GameObject particle = Instantiate(impactParticleEffect, transform.position, Quaternion.identity) as GameObject;
     //   Destroy(particle, 2);
    }
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        target = GameObject.Find("Tower").GetComponent<Tower>();
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
            BoltNetwork.Destroy(gameObject);
            //Destroy(gameObject);
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
}