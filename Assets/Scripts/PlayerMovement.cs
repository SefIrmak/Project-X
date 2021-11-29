using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Horizontal Movement
    public float moveSpeed = 5f;
    private Vector3 direction;
    private Rigidbody _rb;

    private bool isFacingRight = true;

    [Header("Jumping")]
    [Range(1, 10)] public float jumpVelocity;
    [SerializeField] private float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;

    [Header("Collision")]
    public Collider _charCollider;
    public bool isGrounded = false;
    public float touchGround = 1.1f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        extraJumps = extraJumpValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpValue;
        }
        // does extra jump in mid air
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            _rb.velocity = Vector3.up * jumpVelocity;
            extraJumps--;
        }
        // does jumping when on the ground
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            //_rb.velocity = Vector3.up * jumpVelocity;
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        //Input recognition
        direction = new Vector3(0f, 0f, Input.GetAxisRaw("Horizontal"));

        if (direction.z < 0 && isFacingRight || direction.z > 0 && !isFacingRight)
        {
            Flip();
        }
    }


    private void FixedUpdate()
    {
        //moves the player horizontally
        _rb.MovePosition(_rb.position + (direction * moveSpeed * Time.fixedDeltaTime));

        // Ground Check logic
        RaycastHit hit;
        isGrounded = Physics.Raycast(_charCollider.bounds.center, transform.TransformDirection(Vector3.down), out hit, touchGround, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_charCollider.bounds.center, _charCollider.bounds.center + Vector3.down * touchGround);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);
    }

    // level restarts when player falls down //

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelBoundary")
        {
            RestartLevel();
        }
    }

    private static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Adding a delegate to the Scene.LoadScene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //delegation to LoadScene
    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log(arg0.name);
        Debug.Log(arg1);
    }
}
