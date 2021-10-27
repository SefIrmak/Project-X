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
    private bool isGrounded;
    public Transform groundCheck;
    [SerializeField] private float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;


    //public Quaternion targetRotation = new Quaternion(0f, 180, 0, 0); // DOES NOT WORK!!

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
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            _rb.velocity = Vector3.up * jumpVelocity;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true) {
            _rb.velocity = Vector3.up * jumpVelocity;
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
        Collider[] something = Physics.OverlapSphere(groundCheck.position, checkRadius, whatIsGround);
        if (something != null)
        {
            isGrounded = true;
        }
        else
            isGrounded = false;

        //moves the player horizontally
        _rb.MovePosition(_rb.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        //level restarts when player falls down
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

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);
        //float speed = 100f;        // DOES NOT WORK!!!
        //transform.rotation = Quaternion.Lerp(transform.rotation, target.normalized, Time.deltaTime * speed);
    }


}
