using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllListener : Bolt.GlobalEventListener
{
    public delegate void OnPlayerChange(string value);
    public static event OnPlayerChange onPlayerChange;


    public override void OnEvent(ControllEvent evnt)
    {
        Debug.Log(evnt.Message);
        if (onPlayerChange != null)
        {
            onPlayerChange(evnt.Message);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
