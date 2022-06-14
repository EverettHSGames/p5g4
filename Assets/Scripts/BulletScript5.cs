using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript5 : MonoBehaviour
{
    public Collider mycollider;
    public Rigidbody myrigidbody;
    public float speed;
    // direction
    public int damage;
    public int offMap = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x) > offMap ||
            Mathf.Abs(transform.position.y) > offMap ||
            Mathf.Abs(transform.position.z) > offMap)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }




    public void InitBullet(Vector3 dir, float spd, int dam)
    {
        transform.forward = dir;
        speed = spd;
        damage = dam;
    }
}
