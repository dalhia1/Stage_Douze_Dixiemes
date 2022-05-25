using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController charControl;

    [SerializeField] private bool canJump = false;

    public float speed = 1f;

    private FMOD.Studio.EventInstance moveEvent;
    public bool airControl;
    public float gravityForce = 9.81f;
    public float currentGravity;
    public float maxGravity = 100.0f;
    public float jumpForce = 10f;
    private void Start()
    {
        



    charControl = GetComponent<CharacterController>();
        
        
    }

    private void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");


        /*if (!airControl && !charControl.isGrounded)
        {
            horizontalValue = 0.0f;
        }*/


        if (charControl.isGrounded)
        {
            currentGravity = 0.0f;
            canJump = true;

        }

        if (canJump && Input.GetButton("Jump"))
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

        Vector3 direction = new Vector3(horizontalValue,currentGravity,verticalValue);
        charControl.Move(Vector3.ClampMagnitude(direction,1) * speed * Time.deltaTime);
        
        if (horizontalValue != 0 || verticalValue != 0)
        {
            //moveEvent.setParameterByName("Movement", Mathf.Max(Mathf.Abs(horizontalValue), Mathf.Abs(verticalValue)));
            moveEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Player_Deplacement");
            moveEvent.start();
            moveEvent.release();

        }
    }
}

