using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Behaviour : MonoBehaviour
{
    //Variables
    public float movementSpeed;
    public GameObject cam;
    public GameObject bulletSpawnPoint;
    public float waitToShoot;
    public GameObject playerObject;
    public GameObject bullet;
    private Transform bulletSpawn;
    public float points;
    public float fireRate;
    private float nextFire = 0f;
    public float maxHealth = 100f;
    public float health;

    public Vector3 moveInput;
    public Vector3 moveVelocity;
    private Rigidbody myRigidbody;

    //Methods

    public void Start()
    {
        points = 0f;
        health = maxHealth;
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * movementSpeed;

        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }

        //Shooting
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        //Player Death
        if (health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        //Player Movement
        myRigidbody.velocity = moveVelocity;
    }

    void Shoot()
    {
        bulletSpawn = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawn.rotation = bulletSpawnPoint.transform.rotation;
    }

    public void Die()
    {
        Debug.Log("You died");
        Destroy(this.gameObject);
    }
}
