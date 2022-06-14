using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killwall : MonoBehaviour
{
    public int HPMax = 100;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript5>().damage);
        }
    }

public void TakeDamage (int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        if (HP > HPMax)
        {
            HP = HPMax;
        }
    }
}

