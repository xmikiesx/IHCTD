using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class RobotBehaviour : Bolt.EntityBehaviour<IRobotState>
{
    // Start is called before the first frame update
    public delegate void OnScoreChange(int value);
    public static event OnScoreChange onScoreChange;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
        state.SetAnimator(GetComponent<Animator>());
        state.Animator.applyRootMotion = entity.IsOwner;
    }

    void OnEnable()
    {
        ControllListener.onPlayerChange += Move;
    }
    void OnDisable()
    {
        ControllListener.onPlayerChange -= Move;
    }


    public void Move(string value)
    {
        var speed = state.Speed;
        var angularSpeed = state.AngularSpeed;

        if (value=="F")
        {
            speed += 0.2f;
        }
        else
        {
            speed -= 0.2f;
        }

        if (value == "R")
        {
            angularSpeed -= 0.1f;
        }
        else if (value == "L")
        {
            angularSpeed += 0.1f;
        }
        else
        {
            if (angularSpeed < 0)
            {
                angularSpeed += 0.1f;
                angularSpeed = Mathf.Clamp(angularSpeed, -1f, 0);
            }
            else if (angularSpeed > 0)
            {
                angularSpeed -= 0.1f;
                angularSpeed = Mathf.Clamp(angularSpeed, 0, +1f);
            }
        }


        state.Speed = Mathf.Clamp(speed, 0f, 2.5f);
        state.AngularSpeed = Mathf.Clamp(angularSpeed, -2f, +2f);
    }


    // Update is called once per frame
    public override void SimulateOwner()
    {/*
        var speed = state.Speed;
        var angularSpeed = state.AngularSpeed;

        if (Input.GetButton("MoveFwd"))
        {
            speed += 0.1f;
        }
        else
        {
            speed -= 0.1f;
        }

        if (Input.GetButton("MoveBck"))
        {
            angularSpeed -= 0.08f;
        }
        else if (Input.GetButton("left"))
        {
            angularSpeed += 0.08f;
        }
        else
        {
            if (angularSpeed < 0)
            {
                angularSpeed += 0.08f;
                angularSpeed = Mathf.Clamp(angularSpeed, -1f, 0);
            }
            else if (angularSpeed > 0)
            {
                angularSpeed -= 0.08f;
                angularSpeed = Mathf.Clamp(angularSpeed, 0, +1f);
            }
        }

        state.Speed = Mathf.Clamp(speed, 0f, 1.5f);
        state.AngularSpeed = Mathf.Clamp(angularSpeed, -1f, +1f);*/
    }

}
