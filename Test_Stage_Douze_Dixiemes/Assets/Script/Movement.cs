using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    public float speed;
    public CharacterController controller;
    public bool airControl;
    public float gravityForce = 9.81f;
    public float currentGravity;
    public float maxGravity = 100.0f;
    public float jumpForce = 10f;
    [SerializeField]private bool canJump=false;

    // Update is called once per frame
    void Update()
    {
        float directionX = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionX = -speed ;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            directionX = speed;
        }


        if (!airControl && !controller.isGrounded)
        {
            directionX = 0.0f;
        }
        
        
        if (controller.isGrounded)
        {
            currentGravity = 0.0f;
            canJump = true;

        }
        
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        { 
            currentGravity = jumpForce;
            canJump = false;
        }
        currentGravity = currentGravity - gravityForce * Time.deltaTime;
        if (currentGravity < -1)
        {
            canJump = false;
        }
        if (currentGravity < -maxGravity)
        {
            currentGravity = -maxGravity;
        }
        
        
        Vector3 direction = new Vector3(directionX,currentGravity,0);
        controller.Move(direction*Time.deltaTime);
    }

    public Transform respawnPosition;
    public void Death()
    {
        controller.enabled = false;
        transform.position = respawnPosition.position;
        controller.enabled = true;
    }

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }
}
