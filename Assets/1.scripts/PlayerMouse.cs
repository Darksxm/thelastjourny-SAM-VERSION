using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    public float mouse_sensitiviy = 100f;

    public Transform player_body;

    float x_rotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouse_sensitiviy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse_sensitiviy * Time.deltaTime;

        x_rotation -= mouseY;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);
        player_body.Rotate(Vector3.up * mouseX);
    }
}
