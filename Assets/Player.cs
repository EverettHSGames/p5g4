using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hpMax = 100;
    public int hp;

    public float mouseSensitivity = 1.0f;
    public float joySensitivity = 1.0f;
    public float joySpeed = 0.5f;
    public float joyTurnMod = 1f;

    public int bulletDmg;
    public float bulletSpd;

    public float speed = 0.5f;
    public float rotAngle = 2.0f;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject myCam;
    public TextMeshPro textMesh;
    Rigidbody myRigidbody;

     // Start is called before the first frame update
    void Start()
    {
        hp = hpMax;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = Vector3.zero;
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        //    transform.position += transform.forward * speed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    //transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        //    transform.position -= transform.forward * speed * Time.deltaTime;
        //}

        float joyY = Input.GetAxis("Vertical") * joySensitivity * Time.deltaTime;
        transform.position += transform.forward * joyY * joySpeed;
        
        //if (Input.GetKey(KeyCode.D))
        //{
        //    //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        //    transform.position += transform.right * speed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        //    transform.position -= transform.right * speed * Time.deltaTime;
        //}

        float joyX = Input.GetAxis("Horizontal") * joySensitivity * Time.deltaTime;
        transform.position += transform.right * joyX * joySpeed;

        
        
        
        
        //turn code
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, rotAngle * Time.deltaTime, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -rotAngle * Time.deltaTime, 0), Space.Self);
        }
        float turnX = Input.GetAxis("TurnCamera") * joySensitivity * Time.deltaTime;
        transform.Rotate(new Vector3(0, joyTurnMod * turnX * rotAngle, 0), Space.Self);



        //Shooting code
        if (Input.GetMouseButtonDown(0) || Input.GetAxis("FirePrimary") != 0)
        {
            Debug.Log("Im Friendly");
            GameObject b = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
            b.GetComponent<BulletScript>().InitBullet(transform.forward, bulletSpd, bulletDmg);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                TakeDamage(collision.gameObject.GetComponent<BulletScript>().damage);
            }
        }
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            myCam.transform.parent = transform.parent;
            Destroy(gameObject);
        }
        if (hp > hpMax)
        {
            hp = hpMax;
        }
    }
}