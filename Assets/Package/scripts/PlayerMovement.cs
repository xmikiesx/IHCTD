﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Bolt.EntityBehaviour<ICustomCubeState>
{

    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, transform);
    }

    public override void SimulateOwner()
    {
        var speed = 1f;
        var movement = Vector3.zero;

        /*        if (Input.GetKey(KeyCode.A)) {
                    movement.x -= 1f;
                }
                if (Input.GetKey(KeyCode.D)) {
                    movement.x += 1f;
                }
                if (Input.GetKey(KeyCode.W)) {
                    movement.z += 1f;
                }
                if (Input.GetKey(KeyCode.S)) {
                    movement.z -= 1f;
                }
        */
        if (Input.GetButton("Fire1"))
        {
            movement.z += 1f;
        }
        if(movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.transform.position;
    }

}
