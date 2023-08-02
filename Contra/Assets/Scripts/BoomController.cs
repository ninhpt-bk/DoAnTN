using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    [SerializeField] float time = 3;
    public float speed = 10;
    public float dame = 30;
    float delay = 0.3f;
    Animator animator;
    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        Vector2 directionPlus90 = Quaternion.Euler(0, 0, 0) * transform.right;
        myBody.AddForce(directionPlus90*new Vector2(Random.Range(0, speed), Random.Range(-5, 20)), ForceMode2D.Impulse);
        Invoke("Attack", time);
    }
    void Attack()
    {
        animator.SetBool("Attack", true);
        transform.localEulerAngles = Vector3.zero;
        Destroy(gameObject, 0.5f);
    }
    private void Update()
    {
        if(delay > 0)
            delay -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        myBody.velocity = new Vector3(1, myBody.velocity.y, 0);
        if(collision.tag == "Player" && animator.GetBool("Attack") && delay <= 0)
        {
            delay = 0.3f;
            PlayerInformation playerInformation = GameObject.FindWithTag("Player").GetComponent<PlayerInformation>();   
            if(playerInformation != null)
            {
                playerInformation.AddDame(dame);
            }
        }
    }
}
