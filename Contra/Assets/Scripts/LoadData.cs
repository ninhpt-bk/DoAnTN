using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class LoadData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/database.json";
        if (File.Exists(path))
        {
            //Debug.Log(2);
            StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            reader.Close();
            MyData myData = JsonUtility.FromJson<MyData>(json);
            PlayerPrefs.SetInt("CoinWorld", myData.coin);
            PlayerPrefs.SetString("Bag", myData.bag);
            PlayerPrefs.SetString("Skins", myData.skins);
        }
        else
        {
            PlayerPrefs.SetInt("CoinWorld", 2000);
            PlayerPrefs.SetString("Bag", "0,0,0");
            PlayerPrefs.SetString("Skins", "1,0,0");
        }
        Invoke("PlayGame", 1);
    }
    void PlayGame()
    {
        LoadMap a = GetComponent<LoadMap>();
        a.SetMap(1);
    }
}
[System.Serializable]
public class MyData
{
    public int coin;
    public string bag;
    public string skins;
}
