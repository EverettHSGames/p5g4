using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_5 : MonoBehaviour
{
    public int hpMax = 100;
    public int hp;

    public float speed = .05f;
    public float rotAngle = 5f;

    public float firingDelay = 0.33f;
    public float firingDelayLeft = 0;

    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject myCam;

    public float mouseSensitivitiy = 1.0f;
    public float joySensitivity = 1.0f;
    public float joySpeed = .5f;
    public float joyTurnMod = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        firingDelayLeft -= Time.deltaTime;
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    //transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        //    transform.position += transform.forward * speed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    //transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        //    transform.position -= transform.forward * speed * Time.deltaTime;
        //}

        float joyY = Input.GetAxis("Vertical") * joySensitivity * Time.deltaTime;
        transform.position += transform.forward * joyY * joySpeed;

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    //transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        //    transform.position -= transform.right * speed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        //    transform.position += transform.right * speed * Time.deltaTime;

        //}

        float joyX = Input.GetAxis("Horizontal") * joySensitivity * Time.deltaTime;
        transform.position += transform.right * joyX * joySpeed;
        /* Turn code
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(new Vector3(0, -rotAngle * Time.deltaTime, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(new Vector3(0, rotAngle * Time.deltaTime, 0), Space.Self);

        }
       */

        float turnX = Input.GetAxis("TurnCamera") * joySensitivity * Time.deltaTime;
        transform.Rotate(new Vector3(0, joyTurnMod * turnX * rotAngle, 0), Space.Self);


        if (firingDelayLeft <= 0)
        {
            if (Input.GetMouseButton(0) || Input.GetAxis("FirePrimary") != 0)
            {
                Debug.Log("YOU SHOOTING SOMEONE!)");
                GameObject b = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
                b.GetComponent<BulletScript5>().InitBullet(transform.forward, 10, 5);
                firingDelayLeft = firingDelay;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                TakeDamage(collision.gameObject.GetComponent<BulletScript5>().damage);
            }
        }
        else if (collision.gameObject.tag == "Pickups")
        {
            PickupScript ps = collision.gameObject.GetComponent<PickupScript>();
            if (ps.pickup == PickupTypes.HEALTH && hp < hpMax)
            {
                TakeDamage(-(int)ps.myValue);
                Destroy(collision.gameObject);
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