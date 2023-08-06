using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int levelMap = 3;
    [SerializeField] GameObject gOCoin;
    [SerializeField] GameObject goItem;
    LoadMap loadMap;
    UIController uiController;
    PlayerInformation playerInformation;
    PlayerController playerController;

    int coin;
    bool pause;
    int[] items = { 0, 0, 0 };
    bool[] useItems = { false, false, false };
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetString("Bag", "0,0,0");
        loadMap = GetComponent<LoadMap>();
        uiController = GetComponent<UIController>();
       
        playerInformation = GameObject.FindWithTag("Player").GetComponent<PlayerInformation>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (playerInformation == null)
            pause = true;
        else
            pause = false;
        playerInformation.AddDame(0);

        coin = PlayerPrefs.GetInt("Coin", 0);
        uiController.OnCoin(coin.ToString());
        string[] s = PlayerPrefs.GetString("Bag", "0,0,0").Split(",");
        //Debug.Log(PlayerPrefs.GetString("Bag", ""));
        for (int i = 0; i < s.Length; i++)
        {
            items[i] = int.Parse(s[i]);
            uiController.ShowBtnItem(i, s[i]);
        }
    }
    public void useItem(int type)
    {
        if (items[type] > 0)
        {
           /* if (playerInformation == null)
                //Debug.Log("NULL");
                playerInformation = GameObject.FindWithTag("Player").GetComponent<PlayerInformation>();*/
            if (!useItems[type]) {
                switch(type)
                {
                    case 0:
                        {
                            if (playerInformation != null)
                                playerInformation.SetDame(playerInformation.GetDame() * 1.5f);
                            break;
                        }
                    case 2:
                        {
                            if (playerInformation != null)
                                playerInformation.AddDame(-playerInformation.GetHp(1)/2);
                            break;
                        }
                }
            }
            if(type != 2)
                useItems[type] = true;
            items[type]--;
            uiController.UseItem(type,items[type].ToString());
        }
    }
    public void SetUseItem(int type, bool value = false)
    {
        useItems[type] = value;
        if (value)
            return;
        switch(type)
        {
            case 0:
                {
                    playerInformation.SetDame(playerInformation.GetDame() / 1.5f);
                    break;
                }
             /*case 2:
                {
                    playerInformation.SetHP(playerInformation.GetHp() / 2);
                    break;
                }*/
        }
    }
    public void MenuGame()
    {
        pause = true;
        uiController.MenuPause(pause);
    }
    public void MenuPauseFunction(int type)
    {
        switch(type)
        {
            case 0:
                {
                    pause = false;
                    uiController.MenuPause(pause);
                    break;
                }
            case 1:
                {
                    PlayerPrefs.SetInt("Coin", coin);
                    loadMap.SetMap(levelMap);
                    break;
                }
            case 2:
                {
                    loadMap.SetMap(1); // menu
                    PlayerPrefs.SetInt("CoinWorld", PlayerPrefs.GetInt("CoinWorld", 0) + coin);
                    string s = "";
                    for (int i = 0; i < items.Length - 1; i++)
                    {
                        s += items[i].ToString() + ",";
                    }
                    PlayerPrefs.SetString("Bag", s + items[items.Length - 1].ToString());
                    break;
                }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") == null && !pause)
        {
            //Destroy(btn1);
            MenuGame();
        }
    }
    void Instance(int type, Vector3 position, Quaternion rotation)
    {
        switch (type)
        {
            case 0:
                {
                    Instantiate(gOCoin, position, rotation);
                    break;
                }
            case 1:
                {
                    Instantiate(goItem, position, rotation);
                    break;
                }
        }
    }
    public void NewItem(Vector3 pos)
    {
        int x = (int)Random.Range(0, 5);
        if(x == 2 || x == 4)
        {
            this.Instance(0, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        }else if(x == 3)
            this.Instance(1, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    public void AddCoin(int value)
    {
        if (useItems[1])
            value *= 2;
        coin += value;
        uiController.OnCoin(coin.ToString());
    }
    public void AddItem(int type)
    {
        items[type]++;
        uiController.ShowBtnItem(type, items[type].ToString());
    }
    public void NextMap()
    {
        PlayerPrefs.SetInt("CoinWorld", PlayerPrefs.GetInt("CoinWorld", 0) + coin);
        string s = "";
        for (int i = 0; i < items.Length - 1; i++)
        {
            s += items[i].ToString() + ",";
        }
        PlayerPrefs.SetString("Bag", s + items[items.Length - 1].ToString());
        if (levelMap == 7)
            loadMap.SetMap(1); // menu
        else
            loadMap.SetMap(levelMap + 1); // menu
    }
    public void SetPause(bool _pause) { pause = _pause; }
    public bool GetPause() { return pause; }
    public void PlayerWithIOS(int type)
    { // 0 nhay, 1 trai, 2 phai, 3 tan cong
        //Debug.Log("OK");
        switch (Mathf.Abs(type))
        {
            case 0:
                {
                    playerController.Jump2();
                    break;
                }
            case 1:
                {
                    playerController.runIOS = type > 0 ? -1 : 0;
                    break;
                }
            case 2:
                {
                    playerController.runIOS = type > 0 ? 1 : 0;
                    break;
                }
            case 3:
                {
                    playerController.attackIOS = type > 0 ? true : false;
                    break;
                }
        }
    }
}

