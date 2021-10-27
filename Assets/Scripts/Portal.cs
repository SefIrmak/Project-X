using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public Transform portalExit;
    public bool isKeyPressed;

    private void Update()
    {        
        transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime, Space.World);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isKeyPressed = false;
            other.gameObject.transform.position = portalExit.position + Vector3.right;
        }
    }
}
