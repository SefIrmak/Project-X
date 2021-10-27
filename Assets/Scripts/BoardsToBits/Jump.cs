using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Range(1, 10)]
    public float jumpVelocity;

    private Rigidbody _rb;
    private bool jumpInput;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
    }
    private void FixedUpdate()
    {
        if (jumpInput)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpInput = false;
        }
    }
}
