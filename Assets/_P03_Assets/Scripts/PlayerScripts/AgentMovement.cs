
using UnityEngine;
using UnityEngine.EventSystems;

namespace SVS
{
    public class AgentMovement : MonoBehaviour
    {
        [SerializeField] ParticleSystem _bubbleBurst = null;
        [SerializeField] AudioClip _jump1 = null;
        [SerializeField] AudioClip _jump2 = null;


        CharacterController controller;
        public float rotationSpeed, movementSpeed, gravity = 20;
        Vector3 movementVector = Vector3.zero;
        private float desiredRotationAngle = 0;

        public float jumpSpeed = 8f;
        private int jumps = 0;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        public void HandleMovement(Vector2 input)
        {
            if (controller.isGrounded)
            {
                if (input.y > 0)
                {
                    movementVector = transform.forward * movementSpeed;
                }
                else
                {
                    movementVector = Vector3.zero;
                }
            }
        }

        public void HandleMovementDirection(Vector3 direction)
        {
            desiredRotationAngle = Vector3.Angle(transform.forward, direction);
            var crossProduct = Vector3.Cross(transform.forward, direction).y;
            if(crossProduct < 0)
            {
                desiredRotationAngle *= -1;
            }
        }

        private void RotateAgent()
        {
            if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
            {
                transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
            }
        }

        private void Update()
        {
         

            if (controller.isGrounded)
            {
                //code for side to side movement
                movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                movementVector = transform.TransformDirection(movementVector);
                movementVector *= movementSpeed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    movementVector.y = jumpSpeed * Time.deltaTime;
                    OneShotSoundManager.PlayClip2D(_jump1, 1);
                    _bubbleBurst.Play();
                }
                jumps = 0;


                if (movementVector.magnitude > 0)
                {
                    RotateAgent();
                }
            }
            else
            {
                //code for side to side movement
                movementVector = new Vector3(Input.GetAxis("Horizontal"), movementVector.y, Input.GetAxis("Vertical"));
                movementVector = transform.TransformDirection(movementVector);
                movementVector.x *= movementSpeed;
                movementVector.z *= movementSpeed;

                if (Input.GetKeyDown(KeyCode.Space) && jumps < 1)
                {
                    movementVector.y = jumpSpeed * Time.deltaTime;
                    OneShotSoundManager.PlayClip2D(_jump2, 1);
                    _bubbleBurst.Play();
                    jumps++;
                }

                if (movementVector.magnitude > 0)
                {
                    RotateAgent();
                }

            }
            movementVector.y -= gravity * Time.deltaTime;
            controller.Move(movementVector * Time.deltaTime);
        }
    }
}
