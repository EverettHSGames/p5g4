using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int hpMax = 100;
    public int hp;

    public float rotangle;
    public float speed;
    public float stopDistance;

    public float firingDelay = 0.33f;
    public float firingDelayLeft = 0;
    
    public GameObject player;
    public NavMeshAgent nav;
    public GameObject bulletSpawner;
    public GameObject bullet;

    public GameObject hpBar;
    
    // Start is called before the first frame update
    void Start()
    {
        hp = hpMax;
        nav.speed = speed;
        nav.angularSpeed = rotangle;
        nav.stoppingDistance = stopDistance;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        firingDelayLeft -= Time.deltaTime;
        if (player != null)
        {
            //navigation
            nav.SetDestination(player.transform.position);

            // if delay is at 0
            if (firingDelayLeft < 0)
            {
                //can i see something
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
                {
                    // if "something" is the player
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //fire bullet
                        GameObject b = Instantiate(bullet, bulletSpawner.transform.position, Quaternion.identity);
                        b.GetComponent<BulletScript>().InitBullet(transform.forward, 6, 20);
                        firingDelayLeft = firingDelay;
                    }
                }

            }



            if ((player.transform.position - transform.position).sqrMagnitude < stopDistance * stopDistance)
            {
                Debug.Log("player close");
                Vector3 newRot = Vector3.RotateTowards(transform.forward, player.transform.position - transform.position, rotangle * Mathf.PI / 180, 0);
                transform.rotation = Quaternion.LookRotation(newRot);
            }

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

        Vector3 newScale = hpBar.transform.localScale;
        newScale.x = hp / (float)hpMax;
        hpBar.transform.localScale = newScale;
        
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        if (hp > hpMax)
        {
            hp = hpMax;
        }
    }

}