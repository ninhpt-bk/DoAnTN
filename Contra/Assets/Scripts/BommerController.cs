using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BommerController : MonoBehaviour
{
    [SerializeField] float dame = 10;
    [SerializeField] float maxHp = 100;
    [SerializeField] float speed = 3;
    [SerializeField] float speedBullet = 10;
    [SerializeField] float delta = 2f;
    [SerializeField] float _distance1 = 1f;
    [SerializeField] float timeSpawm = 1;
    [SerializeField] bool facingRight = true; // nhân vật quay mặt về bên phải
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullet;
    [SerializeField] Slider sldHp;
    [SerializeField] TextMeshProUGUI txtHp;
    GameObject player;
    Animator myAnimation;
    Rigidbody2D myRGB2D;
    bool isMovingRight;
    Vector3 index;
    float m_timeSpawm = 0, mHp;

    GameController gameController;
    EffectController effectController;

    // Start is called before the first frame update
    void Start()
    {
        if (!facingRight)
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
        isMovingRight = facingRight;
        index = transform.position;
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        effectController = GameObject.FindWithTag("GameController").GetComponent<EffectController>();
        myAnimation = GetComponent<Animator>();
        myRGB2D = GetComponent<Rigidbody2D>();
        mHp = maxHp;
        sldHp.maxValue = 1;
        sldHp.value = 1;
        txtHp.text = mHp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetPause())
            return;
        if (m_timeSpawm > 0)
            m_timeSpawm -= Time.deltaTime;
        Vector3 distance = this.player.transform.position - transform.position;
        if (distance.magnitude <= _distance1)
        {
            if((player.transform.position.x < transform.position.x && isMovingRight) || (player.transform.position.x > transform.position.x && !isMovingRight))
            {
                isMovingRight = !isMovingRight;
                this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, 0);
            }
            this.Attack();
        }
        else
        {
            this.Run();
        }
    }
    void Idle()
    {
        myAnimation.SetFloat("Speed", 0);
        myAnimation.SetBool("Attack", false);
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
        myAnimation.SetFloat("Speed", 0.5f);
    }
    private void MoveLeft()
    {
        myRGB2D.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
        isMovingRight = false;
        this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, 0);
    }

    private void MoveRight()
    {
        myRGB2D.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
        isMovingRight = true;
        this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, 0);
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
        if (m_timeSpawm <= 0)
        {
            //Debug.Log(m_timeSpawm);
            myAnimation.SetBool("Attack", true);
            Invoke("Idle", 0.3f);
            float angle = (Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg;
            if (facingRight)
            {// nếu nhân vật đang quay sang phải:
                GameObject goBullet = Instantiate(bullet, gun.transform.position, Quaternion.Euler(new Vector3(0, 0, angle))); // tạo viên đạn trùng với hướng hiện tại
                goBullet.GetComponent<BoomController>().speed = speedBullet;
                goBullet.GetComponent<BoomController>().dame = dame;
                effectController.Instantiate(0, gun.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else
            {
                GameObject goBullet = Instantiate(bullet, gun.transform.position, Quaternion.Euler(new Vector3(0, 0, angle))); // tạo viên đạn trùng với hướng hiện tại
                effectController.Instantiate(0, gun.transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                goBullet.GetComponent<BoomController>().speed = speedBullet;
                goBullet.GetComponent<BoomController>().dame = dame;
            }
            m_timeSpawm = timeSpawm;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletPlayer" && collision.gameObject.activeSelf)
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
                Destroy(gameObject);
                //gameObject.GetComponentInParent<FrustumCulling>().FixedUpdateS();
                //gameObject.SetActive(false);
            }
        }
    }
    public float GetHp() { return mHp; }
}