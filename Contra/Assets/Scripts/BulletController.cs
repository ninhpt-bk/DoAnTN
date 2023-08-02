using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public float speed = 10f;
    public float dame = 10;
    Rigidbody2D myBody;
    
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        Vector2 directionPlus90 = Quaternion.Euler(0, 0, 0) * transform.right;
        myBody.AddForce(directionPlus90 * speed*50);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && gameObject.tag != "BulletGunner")
        {
            Destroy(gameObject);
        }
    }
    public float Dame()
    {
        return dame;
    }
}
