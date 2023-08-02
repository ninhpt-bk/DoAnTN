using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEffect : MonoBehaviour
{
    [SerializeField] float distance = 0;
    [SerializeField] bool ngang = true;
    [SerializeField] float speed = 2f;
    [SerializeField] bool xuoi = true;
    GameController gameController;
    GameObject player;
    Vector3 left, right;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (ngang)
        {
            left = transform.position - new Vector3(distance, 0, 0);
            right = transform.position + new Vector3(distance, 0, 0);
        }
        else
        {
            left = transform.position - new Vector3(0, distance, 0);
            right = transform.position + new Vector3(0, distance, 0);
        }
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameController.GetPause())
            return;
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (distance > 5 * distance)
            return;
        this.ChuyenDong();
    }
    void ChuyenDong()
    {
        float move = Time.deltaTime;
        if (xuoi)
        {
            move *= speed;
        }
        else
        {
            move *= -speed;
        }
        //Debug.Log(move);
        if (ngang)
        {
            transform.position = transform.position + new Vector3 (move, 0, 0);
            if ((transform.position.x > right.x && xuoi) || (transform.position.x < left.x && !xuoi))
                xuoi = !xuoi;
        }
        else
        {
            transform.position = transform.position + new Vector3(0, move, 0);
            if ((transform.position.y > right.y && xuoi) || (transform.position.y < left.y && !xuoi))
                xuoi = !xuoi;
        }
    }
}
