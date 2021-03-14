using System;
using System.Collections;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform respawnPoint;

    public CharacterController controller; //Referencias
    public Transform cam; //Referencias

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groudMask;
    private bool isDied;

    public float speed = 10f;
    public float walk = 10f;
    public float sprintspeed = 20f;

    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3f;
    float turnSmoothVelocity;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private IEnumerator Revive()
    {
        yield return new WaitForSeconds(0.1f);
        isDied = false;
    } 
    private void Move()
    {
        if (isDied)
        {
            Restart();
            StartCoroutine(Revive());
        }
        else
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groudMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }


            float horizontal = Input.GetAxisRaw("Horizontal"); // Raw tem resposta imediata
            float vertical = Input.GetAxisRaw("Vertical"); // Raw tem resposta imediata

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity + speed);
            }

            if (Input.GetKey((KeyCode.LeftShift)) && isGrounded)
            {
                speed = sprintspeed;

                if (Input.GetButtonDown("Jump"))
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity + speed);
                }
            }

            else
            {
                speed = walk;
            }

            if (direction.magnitude >= 0.1f)

            {
                float targetAngle =
                    Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                    cam.eulerAngles.y; //Pega Angulação entre os eixos
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime); //Dados para Rotação suave

                transform.rotation = Quaternion.Euler(0f, angle, 0f); //Faz a rotação (diagonais)

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            if (Input.GetKey((KeyCode.R)))
                Restart();
        }

    }
    

    public void Restart()
    {
        transform.position = respawnPoint.transform.position;
    }

    public void Death(Transform respawnTransform)
    {
        isDied = true;
        respawnPoint = respawnTransform;
    }
}