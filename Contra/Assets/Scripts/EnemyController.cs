using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] float dame = 3;
    [SerializeField] float maxHp = 70;
    [SerializeField] float speed = 4;
    [SerializeField] float delta = 2f;
    [SerializeField] float _distance1 = 1f;
    [SerializeField] int type = 0;
    [SerializeField] bool facingRight; // nhân vật quay mặt về bên phải
    [SerializeField] float timeSpawm = 1f;
    [SerializeField] Slider sldHp;
    [SerializeField] TextMeshProUGUI txtHp;


    GameObject player;
    Animator myAnimation;
    Rigidbody2D myRGB2D;
    bool isMovingRight;
    Vector3 index;
    float m_timeSpawm = 0, mHp;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        if(!facingRight)
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
        isMovingRight = facingRight;
        index = transform.position;
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimation = GetComponent<Animator>();
        myRGB2D = GetComponent<Rigidbody2D>();
        mHp = maxHp;
        sldHp.maxValue = 1;
        sldHp.value = 1;
        sldHp.interactable = false;
        txtHp.text = mHp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetPause())
            return;
        if(m_timeSpawm > 0)
            m_timeSpawm -= Time.deltaTime;

        //Vector3 distance = player.transform.position - index;
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= _distance1)
        {
            this.Attack();
        }
        else
        {
            this.Run();
        }
    }
    void Run()
    {
        if (transform.position.x >= index.x - delta && transform.position.x <= index.y + delta)
        {
            if (player.transform.position.x < transform.position.x && player.transform.position.x >= index.x - delta)
            {
                MoveLeft();
            }
            else if (player.transform.position.x > transform.position.x && player.transform.position.x <= index.x + delta)
            {
                MoveRight();
            }
            else if (isMovingRight)
            {
                myRGB2D.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                myRGB2D.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
            }
        }
        else
        {
            MoveRandomly();
        }
    }
    private void MoveLeft()
    {
        myRGB2D.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
        if (isMovingRight)
        {
            isMovingRight = false;
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
        }
    }

    private void MoveRight()
    {
        myRGB2D.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
        if (!isMovingRight)
        {
            isMovingRight = true;
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
        }
    }

    private void MoveRandomly()
    {
        if (isMovingRight)
        {
            myRGB2D.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= index.x + delta)
            {
                isMovingRight = false;
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
        }
        else
        {
            myRGB2D.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= index.x - delta)
            {
                isMovingRight = true;
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
        }
    }

    void Attack()
    {
        if(m_timeSpawm <= 0)
        {
            GameObject childObject = transform.GetChild(1).gameObject;
            childObject.SetActive(true);
            myAnimation.Play("DogAttack");
            m_timeSpawm = timeSpawm;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BulletPlayer" && collision.gameObject.activeSelf)
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            float damePlayer = collision.gameObject.GetComponent<BulletControler>().Dame();
            GameObject.FindWithTag("GameController").GetComponent<UIController>().OnDame(-damePlayer, transform.position);
            mHp -= damePlayer;
            sldHp.value = mHp / maxHp;
            txtHp.text = mHp.ToString();
            if (mHp <= 0)
            {
                gameController.NewItem(transform.position);
                //gameObject.SetActive(false);
                Destroy(gameObject);
               // gameObject.GetComponentInParent<FrustumCulling>().FixedUpdateS();

            }
        }
    }
    public float GetHp() { return mHp; }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }
    public float GetDame()
    {
        return dame;
    }

}
