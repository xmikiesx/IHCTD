using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : Bolt.EntityBehaviour<ICustomCubeState>
{
    public int playerSpeed;
    // Start is called before the first frame update

    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, transform);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("MoveFwd"))
        {
            transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
        }
        if (Input.GetButton("MoveBck"))
        {
            transform.position = transform.position - Camera.main.transform.forward * playerSpeed * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(" Colision detectada con " + other.gameObject.name);
        if (other.gameObject.CompareTag("Coin"))
        {
            //other.gameObject.GetComponent<Enemy>().damage(-impact);
        }
    }
}
