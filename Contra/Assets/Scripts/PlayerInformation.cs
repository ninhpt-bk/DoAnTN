using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInformation : MonoBehaviour
{
    float maxSpeed = 10;
    float maxInputTime = 2;
    float forceJump = 20f; // lực nhảy tối đa
    float dame = 100f; // sát thương
    float maxHp = 100f; // hp tối đa
    [SerializeField] Slider hp;
    [SerializeField] TextMeshProUGUI textHP;
    [SerializeField] GameObject[] goSkins;
    SkinController skinController;
    //UIController uiController;
    float mHp; // lượng máu hiện tại
    int skin;
    // Start is called before the first frame update
    void Start()
    {
        LoadDataSkin();
        //uiController = GameObject.FindWithTag("GameController").GetComponent<UIController>();
        hp.maxValue = 1;
        hp.value = 1;
        hp.interactable = false;
        textHP.text = mHp.ToString() + "/" + maxHp.ToString(); 
    }
    void LoadDataSkin()
    {
        skin = PlayerPrefs.GetInt("Skin", 0);
        if (skin < 0 || skin >= goSkins.Length)
            skin = 0;
        for (int i = 0; i < goSkins.Length; i++)
            goSkins[i].SetActive(false);
        goSkins[skin].SetActive(true);
        skinController = goSkins[skin].GetComponent<SkinController>();
        maxSpeed = skinController.maxSpeed;
        maxHp = skinController.maxHp;
        dame = skinController.dame;
        mHp = maxHp;
        forceJump = skinController.forceJump;
    }
    // Update is called once per frame

    public Vector2 GetSpeed(){return new Vector2(maxInputTime, maxSpeed);  }
    public float GetHp(int type = 0) {
        if (type == 0)
            return mHp;
        else
            return maxHp;
    }
    public void SetHP(float value) {maxHp = value; mHp = value; textHP.text = mHp.ToString() + "/" + maxHp.ToString(); }
    public float GetDame() { return dame; }
    public float SpeedBullet() { return skinController.speedBullet; }
    public void SetDame(float value) {dame = value; }
    public float GetJump() { return forceJump; }
    public void AddDame(float dame)
    {
       GameObject.FindWithTag("GameController").GetComponent<UIController>().OnDame(-dame, transform.position);
        mHp -= dame;
        if (mHp < 0)
            Destroy(gameObject);
        if (mHp > maxHp)
            mHp = maxHp;
        hp.value = mHp/maxHp;
        textHP.text = mHp.ToString() + "/" + maxHp.ToString();
    }
    public void Run(float value)
    {
        skinController.Run(value);
    }
    public float Run()
    {
        return skinController.Run();
    }
    public void Jump(bool value)
    {
        skinController.Jump(value);
    }
    public void Attack(bool value)
    {
        skinController.Attack(value);
    }
    public void Die()
    {
        skinController.Die();
    }
    public GameObject Bullet()
    {
        return skinController.bullet;
    }
    public GameObject Gun()
    {
        return skinController.gun;
    }
    public int TypeSkin()
    {
        return skin;
    }
}
