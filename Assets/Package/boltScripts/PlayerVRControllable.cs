using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRControllable : Bolt.EntityBehaviour
{
    public delegate void OnScoreChange(int value);
    public static event OnScoreChange onScoreChange;
    public delegate void OnManaChange(int value);
    public static event OnManaChange onManaChange;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var evnt = ControllEvent.Create();

        if (Input.GetButton("MoveFwd"))
        {
            evnt.Message = "F";
            evnt.Send();
        }
        else
        {
            evnt.Message = "B";
            evnt.Send();
        }

        if (Input.GetButton("MoveBck"))
        {
            evnt.Message = "R";
            evnt.Send();
        }
        else if (Input.GetButton("left"))
        {
            evnt.Message = "L";
            evnt.Send();

        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(" Colision detectada con " + other.gameObject.name);
        if (other.gameObject.CompareTag("Coin"))
        {/*
            if (onScoreChange != null)
            {
                onScoreChange(5);
            }*/
            var evnt = VRCollider.Create();
            evnt.Message = "Coin";
            BoltNetwork.Destroy(other.gameObject);
            evnt.Send();
            //other.gameObject.GetComponent<Enemy>().damage(-impact);
        }
        else if (other.gameObject.CompareTag("Energy"))
        {/*
            if (onManaChange != null)
            {
                onManaChange(5);
            }*/
            var evnt = VRCollider.Create();
            evnt.Message = "Coin";
            BoltNetwork.Destroy(other.gameObject);
            evnt.Send();

            //other.gameObject.GetComponent<Enemy>().damage(-impact);
        }
    }
}
