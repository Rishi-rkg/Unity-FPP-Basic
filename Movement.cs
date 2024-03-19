using UnityEngine;

public class Movement : MonoBehaviour
{
    
    /*
    NOTES:
    Serialize field helps it viewing in the editor but keeps it private we 
    can do the same with public but since no other script is using it 
    */


    //VARIABLES
    [SerializeField] float moveSpeed;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float RunSpeed;
    
    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float JumpHeight;

    //REFERENCES
    private CharacterController controller;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0,0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if(isGrounded)
        {
            if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //Walk
                Walk();
            }
            else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                 //Run
                Run();
            }
            else if(moveDirection == Vector3.zero)
            {
                //Idle
                Idle();
            }
            moveDirection*=moveSpeed;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity*Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
    }
    private void Idle()
    {
        anim.SetFloat("Speed", 0.0f,0.1f,Time.deltaTime);
    }

     private void Run()
    {
        moveSpeed = RunSpeed;
        anim.SetFloat("Speed", 1.0f,0.1f,Time.deltaTime);
    }

     private void Walk()
    {
        moveSpeed = WalkSpeed;
        anim.SetFloat("Speed", 0.5f,0.1f,Time.deltaTime);

    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravity);
    }
}
