using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public float InputX;
    public float InputZ;

    public float desiredRotationSpeed;
    public float Speed;
    public float Sprint;
    public float moveSpeed = 2.0f;
    public float sprintSpeed = 2.0f;


    const float acceleration = 0.2f;




    public float allowPlayerRotation;
    public float verticalVel;
    public float gravity_g = 5f;
    public float ground_detect_radius = 0.4f;


    public float jumpHeigh = 20f;
    public float jumpSpeed = 20f;


    public Transform ground_detector;


    public LayerMask platform_mask;
    public LayerMask ground_mask;



    public Vector3 desiredMovementDirection;
    private Vector3 moveVector;
    [HideInInspector]
    public Vector3 velocity;

    public bool isInAir;
    public bool blockRotationPlayer;
    public bool is_grounded;


    public Animator anim;

    public Camera cam;

    public CharacterController controller;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();

        velocity.y = 0;
    }
    private void Update()
    {
        InputMagnitude();

        is_grounded = Physics.CheckSphere(ground_detector.position, ground_detect_radius, ground_mask);

        if (is_grounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 0;
        }
        moveVector = new Vector3(0, verticalVel, 0);

        bool is_platforming = Physics.CheckSphere(ground_detector.position, ground_detect_radius, platform_mask);

        velocity.y -= gravity_g * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
            Acceleration();


    }
    private void FixedUpdate()
    {
        Jump();
        /*Acceleration();*/
    }
    void Jump()
    {



        bool is_jumping = Input.GetKeyDown(KeyCode.Space);

        if (controller.isGrounded)
        {

            var camera = Camera.main;
            var forward = cam.transform.forward;
            var right = cam.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            desiredMovementDirection = forward * InputZ + right * InputX;
            velocity *= moveSpeed;
            if (is_jumping)
            {
                is_grounded = false;


                velocity.y = jumpHeigh;
                /*velocity.z = jumpSpeed;*/
                anim.SetBool("isInAir", true);

            }
            else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
            {
                is_grounded = false;


                velocity.y = jumpHeigh;
               /* velocity.z = jumpSpeed;*/
                anim.SetBool("isInAir", true);

            }

            else
            {
                is_grounded = true;
                anim.SetBool("isInAir", false);
                velocity.y = 0f;
               /* velocity.z = 0f;*/


            }
        }
        /* else if (is_platforming)
         {
             if (is_jumping)
             {
                 isGrounded = false;
                 velocity.y = jumpSpeed;
             }
             else
             {
                 isGrounded = true;
                 velocity.y = -2f;
             }
         }*/
        /*        controller.Move(velocity * Time.deltaTime);
        */
    }
    void PlayerMoveAndRotation()
    {

        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");




        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMovementDirection = forward * InputZ + right * InputX;

        if (blockRotationPlayer == false)
        {
            is_grounded = true;
            controller.Move(desiredMovementDirection * Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMovementDirection), desiredRotationSpeed);
        }

    }

    void InputMagnitude()
    {

        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);




        //calculate the input Magnitude

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //physically move

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
        }
    }

    void Acceleration()
    {


        if (Input.GetKey(KeyCode.W))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isInAir = true;
                is_grounded = true;
                Sprint = new Vector2(2, 0).sqrMagnitude;
                Sprint = Mathf.Clamp(Sprint, 1, 2);
                anim.SetFloat("InputMagnitude", Sprint, 0.0f, Time.deltaTime);
                /*Sprint += acceleration;*/

                controller.Move(desiredMovementDirection * Time.deltaTime * moveSpeed * sprintSpeed);


            }
            else

            {
                isInAir = false;
                is_grounded = true;
                /* Speed = new Vector2(InputX, InputZ).sqrMagnitude;*/
                Speed = Mathf.Clamp(Speed, 0, 1);
                anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
                /*Speed -= acceleration;*/
            }
        }



    }
}