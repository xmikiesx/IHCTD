using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerEventListener : Bolt.GlobalEventListener
{
    public delegate void OnScoreChange(int value);
    public static event OnScoreChange onScoreChange;
    public delegate void OnManaChange(int value);
    public static event OnManaChange onManaChange;
    public override void OnEvent(VRCollider evnt)
    {
        Debug.Log(evnt.Message);
        if (evnt.Message == "Coin")
        {
            if (onScoreChange != null)
            {
                onScoreChange(5);
            }
            
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
