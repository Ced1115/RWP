using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_script : MonoBehaviour
{
    public Gun_Projectile _gun;
    Rigidbody rb;
    bool _canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(gameObject.transform.forward * _gun._ProjectileSpeed * 10000 * Time.deltaTime);
    }

    // Update is called once per frame
    /*
    void Update()
    {   Vector3 dir = new Vector3(gameObject.transform.forward * _gun._ProjectileSpeed) + new Vector3(-gameObject.transform.up * _gun._gravityPull);
        if (_canMove)
        {
            
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        _canMove = false;
    }
    */
}
