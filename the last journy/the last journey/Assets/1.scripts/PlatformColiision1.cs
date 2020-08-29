using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColiision1 : MonoBehaviour
{
    public CharacterController player;
    public Transform platform;
    
    [HideInInspector]
    public string last_platform;

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent == null) return;
      
        if (collision.transform.parent.name == "Platform" && collision.collider.name != last_platform)
        {
            Debug.Log("Doing collision");
            platform.transform.position = player.transform.position - new Vector3(0, 2f, 0);
            
            var script = player.GetComponent<PlayerMovement>();
            script.velocity = new Vector3(0, 0, 0);

            last_platform = collision.collider.name;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //platform.transform.position = new Vector3(0, 0, 0);
    }
}
