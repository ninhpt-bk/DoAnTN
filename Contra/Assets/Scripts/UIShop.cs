using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShop : MonoBehaviour
{
    [SerializeField] GameObject ShopItems;
    [SerializeField] GameObject ShopSkin;
    [SerializeField] GameObject InformationItem;
    [SerializeField] TextMeshProUGUI textInformationItem;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] ShowSkin[] btnSkin;
    int coin = 0;
    int[] items = { 0, 0, 0 };
    int[] skins = { 0, 0, 0 };
    void Start()
    {
        ShopItems.SetActive(true);
        ShopSkin.SetActive(false);
        InformationItem.SetActive(false);
        coin = PlayerPrefs.GetInt("CoinWorld", 5000);
        textCoin.text = coin.ToString();
        if (PlayerPrefs.GetString("Bag", "") != "")
        {
            string[] s = PlayerPrefs.GetString("Bag", "").Split(",");
            for (int i = 0; i < s.Length; i++)
            {
                items[i] = int.Parse(s[i]);
            }
        }
        //Debug.Log(PlayerPrefs.GetString("Skins"));
        string[] s2 = PlayerPrefs.GetString("Skins", "").Split(",");
        for (int i = 0; i < s2.Length; i++)
        {
            skins[i] = int.Parse(s2[i]);
            if (skins[i] == 0)
            {
                btnSkin[i].btn1.gameObject.SetActive(false);
                btnSkin[i].btn2.gameObject.SetActive(true);
                btnSkin[i].numText.text = btnSkin[i].price.ToString();
            }
            else
            {
                btnSkin[i].btn2.gameObject.SetActive(false);
                btnSkin[i].btn1.gameObject.SetActive(true);
                btnSkin[i].numText.gameObject.SetActive(false);
            }
        }
    }
    public void OnShop(int id)
    {
        if(id == 0)
        {
            ShopItems.SetActive(true);
            ShopSkin.SetActive(false);
            InformationItem.SetActive(false) ;
        }
        else
        {
            ShopItems.SetActive(false);
            ShopSkin.SetActive(true);
            InformationItem.SetActive(false);
        }
    }
    public void OfShop()
    {
        ShopItems.SetActive(false);
        ShopSkin.SetActive(false);
        InformationItem.SetActive(false);
    }
    public void ShowInformationItem(string s)
    {
        textInformationItem.text = "Thông tin vật phẩm\n";
        string[] arr = s.Split(';');
        for(int i = 0; i < arr.Length; i++)
        {
            textInformationItem.text += arr[i] + "\n";
        }
        InformationItem.SetActive(true);
    }
    public void ShowInformationSkin(string s)
    {
        textInformationItem.text = "Thông tin nhân vật\n";
        string[] arr = s.Split(';');
        for (int i = 0; i < arr.Length; i++)
        {
            textInformationItem.text += arr[i] + "\n";
        }
        InformationItem.SetActive(true);
    }
    void Mess(string s)
    {
        textInformationItem.text = s;
        InformationItem.SetActive(true);
    }
    public void OfInformationItem()
    {
        InformationItem.SetActive(false);
    }
    public void BuyItem(int type)
    {
        if (ShopItems.activeSelf)
        {
            int[] price = { 1200, 500, 700 };
            if (coin >= price[type])
            {
                items[type]++;
                coin -= price[type];
                textCoin.text = coin.ToString();
                this.Mess("Mua vật phẩm thành công!");
            }
            else
            {
                this.Mess("Số dư không đủ!");
            }
        }
        else
        {
            if (coin >= btnSkin[type].price)
            {
                skins[type] = 1;
                coin -= btnSkin[type].price;
                textCoin.text = coin.ToString();
                this.Mess("Mua nhân vật thành công!");
                btnSkin[type].btn2.gameObject.SetActive(false);
                btnSkin[type].btn1.gameObject.SetActive(true);
                btnSkin[type].numText.gameObject.SetActive(false);
            }
            else
            {
                this.Mess("Số dư không đủ!");
            }
        }
    }
    public void UsePlayer(int type)
    {
        PlayerPrefs.SetInt("Skin", type);
        this.Mess("Thay đổi nhân vật thành công!");
    }
    public void Save()
    {
        PlayerPrefs.SetInt("CoinWorld", coin);
        string s = "";
        for(int i=0; i<items.Length - 1; i++)
        {
            s += items[i].ToString() + ",";
        }
        PlayerPrefs.SetString("Bag", s + items[items.Length - 1].ToString());

        s = "";
        for (int i = 0; i < skins.Length - 1; i++)
        {
            s += skins[i].ToString() + ",";
        }
        PlayerPrefs.SetString("Skins", s + skins[skins.Length - 1].ToString());
        //Debug.Log("Skin: " + PlayerPrefs.GetString("Skins"));
    }
}
[System.Serializable]
class ShowSkin
{
    public Button btn1;
    public Button btn2;
    public TextMeshProUGUI numText;
    public int price;
}