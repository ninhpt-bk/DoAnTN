using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int type;
    PlayerInformation playerInformation;
    Rigidbody2D myRigidbody;
    float m_delayJump = 0.3f;
    float delayJump = 0;
    bool touchTheGround; // chạm đất
    bool facingRight; // nhân vật quay mặt về bên phải
    bool doubleJump; // nhảy 2 lần
    float timeSpwam = 0.35f, m_timeSpwam = 0;
    //float lastInputTime;

    GameController gameController;
    EffectController effectController;
    UIController uiController;
    AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        touchTheGround = false;
        doubleJump = false;
        playerInformation = GetComponent<PlayerInformation>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        effectController  = GameObject.FindWithTag("GameController").GetComponent<EffectController>();
        uiController = GameObject.FindWithTag("GameController").GetComponent <UIController>();
        audioController = GameObject.FindWithTag("GameController").GetComponent<AudioController>();
        myRigidbody = GetComponent<Rigidbody2D>();
        type = playerInformation.TypeSkin();
        switch (type)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    timeSpwam /= 1.5f;
                    break;
                }
            case 2:
                {
                    timeSpwam *= 2;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetPause())
            return;

        this.Jump();
        this.Run();
        //this.Sit();
        this.Attack();
    }
    void Run()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Run(1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Run(-1);
        }
        else
            Run(0);
    }
    public void Run(int value)
    {
        if (value > 0)
        {
            if (facingRight)
            {
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            facingRight = false;
        }
        else if (value < 0)
        {
            if (!facingRight)
            {
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            facingRight = true;
        }
        if (value != 0)
        {
            //playerInformation.Sit(false);
            if (touchTheGround)
            {
                if (playerInformation.Run() < 1)
                    playerInformation.Run(playerInformation.Run() + 0.1f);
            }
            myRigidbody.velocity = new Vector2(-value * playerInformation.GetSpeed().y, myRigidbody.velocity.y);
        }
        else if (playerInformation.Run() > 0)
        {
            playerInformation.Run(playerInformation.Run() - 0.025f);
        }
    }
    public void Attack()
    {   
        m_timeSpwam -= Time.deltaTime;
        if(Input.GetKey(KeyCode.Q) && m_timeSpwam <= 0){ // người dùng bấm phím Q và chiêu đã hồi xong
            m_timeSpwam = timeSpwam; // làm mới thời gian hồi
            //playerInformation.Sit(false);
            playerInformation.Attack(true);
            audioController.PlaySound(2);
            
            if(facingRight){// nếu nhân vật đang quay sang phải:
                GameObject goBullet = Instantiate(playerInformation.Bullet(), playerInformation.Gun().transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo viên đạn trùng với hướng hiện tại
                goBullet.GetComponent<BulletControler>().speed = playerInformation.SpeedBullet();
                goBullet.GetComponent<BulletControler>().dame = playerInformation.GetDame();
                effectController.Instantiate(0, playerInformation.Gun().transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else{
                GameObject goBullet = Instantiate(playerInformation.Bullet(), playerInformation.Gun().transform.position, Quaternion.Euler(new Vector3(0, 0, 180))); // tạo viên đạn trùng với hướng hiện tại
                effectController.Instantiate(0, playerInformation.Gun().transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                goBullet.GetComponent<BulletControler>().speed = playerInformation.SpeedBullet();
                goBullet.GetComponent<BulletControler>().dame = playerInformation.GetDame();
            }
        }
        if(Input.GetKeyUp(KeyCode.Q))
            playerInformation.Attack(false);
    }
    void Jump()
    {
        if(delayJump > 0)
            delayJump -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (touchTheGround)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, playerInformation.GetJump());
                doubleJump = true;
            }
            else if (doubleJump)
            {
                doubleJump = false;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, playerInformation.GetJump()*0.75f);
            }
            else
                return;
            delayJump = m_delayJump;
           // playerInformation.Sit(false);
            playerInformation.Jump(true);
            touchTheGround = false;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && delayJump <= 0){
            touchTheGround = true;
            playerInformation.Jump(false);
            doubleJump = true;
        }
        else if (collision.gameObject.CompareTag("LineMap"))
        {
            playerInformation.Die();
            Destroy(gameObject, 1f);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && delayJump <= 0)
        {
            touchTheGround = false;
            //playerInformation.Sit(false);
            playerInformation.Jump(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyHit" && collision.gameObject.activeSelf)
        {
            playerInformation.AddDame(collision.transform.parent.gameObject.GetComponent<EnemyController>().GetDame());
            collision.gameObject.SetActive(false);
            if (playerInformation.GetHp() <= 0)
            {
                this.Die();
            }
        }
        else if (collision.tag == "BulletGunner" && collision.gameObject.activeSelf)
        {
            float addDame = collision.gameObject.GetComponent<BulletControler>().Dame();
            playerInformation.AddDame(addDame);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            if (playerInformation.GetHp() <= 0)
            {
                this.Die();
            }
        }
        else if (collision.tag == "Finish" && collision.gameObject.activeSelf)
        {
            gameController.NextMap();
        }else if(collision.tag == "LineMap")
        {
            Destroy(gameObject);
        }
    }
    void Die()
    {
        playerInformation.Die();
        Destroy(gameObject, 0.5f);
    }
}
