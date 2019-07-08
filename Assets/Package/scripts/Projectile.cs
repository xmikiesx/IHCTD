using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float Animation;
    public Vector3 PosInit;
    public Vector3 PosEnd;
    public GameObject spellPrefab;

    //   void OnCollisionEnter(Collision col)
    //   {
    //       if (col.gameObject.tag == "Floor")
    //       {
    //       Debug.Log("Impacto contra el suelo");
    //      }
    //    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("La bala impacto contra" + other.gameObject.tag);

        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Enemy")
        {
            Debug.Log("Impacto contra el suelo" + other.gameObject.tag);
            GameObject spellGO = Instantiate(spellPrefab, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(spellGO, 2f);
            Destroy(this.gameObject);
        }

    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //PosInit = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Animation += Time.deltaTime;
        Animation = Animation % 5f;
        transform.position = MathParabola.Parabola(PosInit, PosEnd, 1f, Animation / 4f);
        if (transform.position.y < PosInit.y)
        {
            Destroy(this.gameObject);
        }
    }
}