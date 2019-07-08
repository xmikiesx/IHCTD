using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
//public class Spell : MonoBehaviour
{
    public int impact;
    public GameObject impactParticleEffect;
    public float radiusEffect;

    public int manaCost;
    public delegate void OnScoreChange(int value);
    public static event OnScoreChange onScoreChange;

    // Start is called before the first frame update
    public new void OnEnable()
    {
        GameObject particle = BoltNetwork.Instantiate(BoltPrefabs.ExplosionMobile_1, transform.position, Quaternion.identity);
        //GameObject particle = Instantiate(impactParticleEffect, transform.position, Quaternion.identity) as GameObject;
        spellCoroutine(particle);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(" Colision detectada con " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.gameObject.GetComponent<Enemy>().damage(-impact);
        }
    }

    private void spellCoroutine(GameObject particle)
    {
        StartCoroutine(waitSpellDestroy(particle));
    }
    
    IEnumerator waitSpellDestroy(GameObject particle)
    {
        Debug.Log("Entre a la corutina");
        yield return new WaitForSeconds(0.5f);
        BoltNetwork.Destroy(particle); 
    }

    // Update is called once per frame
    void Update()
    {

    }
}