using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainVR : Bolt.GlobalEventListener
{

    // Start is called before the first frame update
    void Start()
    {
      
            Terrain[] terrainComponents = GetComponentsInChildren<Terrain>();

            foreach (Terrain component in terrainComponents)
            {
                component.enabled = true;
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
