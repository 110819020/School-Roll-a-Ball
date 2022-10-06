using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class TeddyFollow : MonoBehaviour
{
    public Transform TargetObj;
    
    private Animator avatar;
    private CharacterController controller;
    
    private float speedDampTime = .25f;
    private float directionDampTime = .25f;
    private readonly int Speed = Animator.StringToHash("Speed");
    private readonly int Direction = Animator.StringToHash("Direction");


    // Start is called before the first frame update
    void Awake()
    {
        avatar = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        avatar.speed = 1 + Random.Range(-0.4f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (avatar && TargetObj)
        {
            if (Vector3.Distance(TargetObj.position, avatar.rootPosition) > 4)
            {
                avatar.SetFloat(Speed, 1, speedDampTime, Time.deltaTime);

                var currentDir = (avatar.rootRotation * Vector3.forward).normalized;
                var wantDir = (TargetObj.position - avatar.rootPosition).normalized;

                if (Vector3.Dot(currentDir, wantDir) > 0)
                {
                    avatar.SetFloat(Direction, Vector3.Cross(currentDir, wantDir).y, directionDampTime, Time.deltaTime);
                }
                else
                {
                    avatar.SetFloat(Direction, Vector3.Cross(currentDir, wantDir).y > 0 ? 1 : -1, directionDampTime, Time.deltaTime);
                }
            }
            else
            {
                avatar.SetFloat(Speed, 0, speedDampTime, Time.deltaTime);
            }
        }

        
    }
    
    
    void OnAnimatorMove()
    {
        controller.Move(avatar.deltaPosition);
        transform.rotation = avatar.rootRotation;
    }
}

