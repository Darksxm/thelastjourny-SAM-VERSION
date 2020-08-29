using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity_g = 5f;

    public float ground_detect_radius = 0.4f;
    public Transform ground_detector;
    public LayerMask ground_mask;
    public LayerMask platform_mask;

    [HideInInspector]
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        bool is_grounded = Physics.CheckSphere(ground_detector.position, ground_detect_radius, ground_mask);
        bool is_platforming = Physics.CheckSphere(ground_detector.position, ground_detect_radius, platform_mask);
        bool is_jumping = Input.GetKey(KeyCode.Space);


        if (is_grounded)
        {
            var platform_script = controller.GetComponent<PlatformColiision>();
            platform_script.last_platform = null;

            if (is_jumping)
            {
                velocity.y = 20f;
            }
            else
            {
                velocity.y = 0f;
            }
        }
        else if (is_platforming)
        {
            if (is_jumping)
            {
                velocity.y = 20f;
            }
            else
            {
                velocity.y = -2f;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y -= gravity_g * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        Debug.Log($"Grounded: {is_grounded}, Platforming: {is_platforming}, Jumping: {is_jumping}");
    }
}
