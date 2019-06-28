using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class ModelInstance : Bolt.EntityBehaviour<ICustomCubeState>
{
    GameObject Modelo;
    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, transform);
    }

    void Start()
    {
        var spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Modelo = BoltNetwork.Instantiate(BoltPrefabs.Cube, spawnPosition, Quaternion.identity);
        Modelo.transform.SetParent(this.transform);
        
    }
    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("transform.position");
        Debug.Log(Modelo.transform.position);
        Debug.Log("this.transform.position");
        Debug.Log(this.transform.position);
        Vector3 A = new Vector3(Modelo.transform.position.x, Modelo.transform.position.y, Modelo.transform.position.z);
        this.transform.position = A;*/         
    }
}
