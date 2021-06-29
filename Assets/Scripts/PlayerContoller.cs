using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float Fire1Cooldown_def = 1f;
    public float Fire2Cooldown_def = 1f;
    public float Fire3Cooldown_def = 1f;


    public GameObject Laser;
    public GameObject Grenade;
    public GameObject Bullet;
    public int maxGrenadeNum;

    //Private fields
    int GrenadeNum;
    float Fire1Cooldown;
    float Fire2Cooldown;
    float Fire3Cooldown;

    CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Fire1Cooldown = Fire1Cooldown_def;
        Fire1Cooldown = Fire2Cooldown_def;
        Fire2Cooldown = Fire2Cooldown_def;

        GrenadeNum = maxGrenadeNum;
    }

    void Update()
    {
        Jump();
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        PointAtMouse();
        Fire1();
        Fire2();
        Fire3();
    }


    private void Jump()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
    }
    private void ReforceXrotation()
    {
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void PointAtMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            transform.LookAt(hit.point);
        }

        ReforceXrotation();
    }

    private void Fire1()
    {
        if (Fire1Cooldown > 0)
        {
            Fire1Cooldown -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            if (Fire1Cooldown <= 0)
            {
                Fire1Cooldown = Fire1Cooldown_def;
                float fire1Offset = 3f;
                ReforceXrotation();
                Instantiate(Laser, transform.position + transform.forward*fire1Offset, transform.rotation);
            }
        }
    }

    private void Fire2()
    {
        if (Fire2Cooldown > 0)
        {
            Fire2Cooldown -= Time.deltaTime;
        }

        if (Input.GetButton("Fire2"))
        {
            if (Fire2Cooldown <= 0)
            {
                Fire2Cooldown = Fire2Cooldown_def;
                float fire2Offset = 3f;
                ReforceXrotation();
                Instantiate(Bullet, transform.position + transform.forward * fire2Offset, transform.rotation);
            }
        }
    }



    private void Fire3()
    {
        if (Fire3Cooldown > 0)
        {
            Fire3Cooldown -= Time.deltaTime;
        }

        if (GrenadeNum >= 1 && Fire3Cooldown <= 0)
        {
            if (Input.GetButton("Fire3"))
            {
                Fire3Cooldown = Fire3Cooldown_def;
                GrenadeNum -= 1;
                float fire3Offset = 3f;
                ReforceXrotation();
                Instantiate(Grenade, transform.position + new Vector3(0, fire3Offset, 0), transform.rotation);
            }
        }
    }
}
