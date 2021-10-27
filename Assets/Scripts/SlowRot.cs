using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRot : MonoBehaviour
{
    public Transform target;
    [Tooltip("0.01 being the minumum and the 1 is the highest value can be given")]
    public float turnSpeed = .01f;
    Quaternion rotGoal;
    Vector3 direction;

    void Update()
    {
        direction = (target.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
    }
}