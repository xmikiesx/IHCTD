using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public int playerSpeed;
    // Start is called before the first frame update
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
}
