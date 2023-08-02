using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] Sprite[] hub;
    [SerializeField] int type = 0;
    [SerializeField] float speed = 0;
    [SerializeField] float deltaY = 0.5f;
    GameObject player;
    float deltaYMax, deltaYMin;
    bool xuoi;
    void Start()
    {
        int x = (int)Random.Range(0, 10);
        if (x < 2)
            type = 0;
        else if(x < 5) type = 1;
        else type = 2;
        gameObject.GetComponent<SpriteRenderer>().sprite = hub[type];
        player = GameObject.FindWithTag("Player");
        xuoi = true;
        deltaYMax = transform.position.y + deltaY;
        deltaYMin = transform.position.y - deltaY/2;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (distance > 20)
            return;
        if (xuoi)
        {
            transform.position = transform.position + new Vector3(0, speed*Time.deltaTime, 0);
            if(transform.position.y > deltaYMax)
            {
                xuoi = !xuoi;
            }
        }
        else
        {
            transform.position = transform.position - new Vector3(0, speed * Time.deltaTime, 0);
            if (transform.position.y < deltaYMin)
            {
                xuoi = !xuoi;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            GameController controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            controller.AddItem(type);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        xuoi = !xuoi; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        xuoi = !xuoi;
    }
}
