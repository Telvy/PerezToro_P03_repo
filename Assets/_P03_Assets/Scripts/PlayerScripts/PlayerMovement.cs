using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
//using System.Numerics;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rbPlayer;
    public GameObject playerVisuals;
    public Transform GroundCheckPosition;
    public Transform camera;
    //public CharacterController controller;

    public float rotSpeed = 1F;
    public Vector3 targetRot;

    public LayerMask layerMask;
    public bool Grounded;

    public float moveSpeed = 6f;
    public float jumpForce = 4f;
    
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        MovementRigidbody();
        //LookTowardsInput();
    }

    void MovementRigidbody()
    {
        //input
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        //Grounding
        Grounded = Physics.CheckSphere(GroundCheckPosition.position, 0.4f, layerMask);

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, jumpForce, rbPlayer.velocity.z);
        }

        //movement
        //Vector3 playerMovement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        //transform.Translate(playerMovement, Space.Self);

    

    }

    private void LookTowardsInput()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        float divCount = 0;

        float hDegree = 0;

        if (hInput != 0)
        {
            hDegree = (hInput > 0 ? 90 : -90);
            divCount++;
        }

        float vDegree = 0;

        if (vInput != 0)
        {
            vDegree = (vInput > 0 ? 0 : 180);

            if (hInput < 0)
            {
                if (vInput < 0)
                {
                    vDegree *= -1;
                }
            }

            divCount++;
        }

        if (hInput != 0 || vInput != 0)
        {
            float finalTargetRot = (hDegree + vDegree) / divCount;
            targetRot = new Vector3(0, finalTargetRot, 0);
        }


        playerVisuals.transform.rotation = Quaternion.Lerp(playerVisuals.transform.rotation, Quaternion.Euler(targetRot), Time.deltaTime * rotSpeed);
    }
}

    

