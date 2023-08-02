using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadMap : MonoBehaviour
{
    public void SetMap(int mapId)
    {
        if (mapId == 7)
            SceneManager.LoadScene(8);
        else
        {
            PlayerPrefs.SetInt("LoadMap", mapId + 1);
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("Coin", 0);
        }
    }
    // Start is called before the first frame update
   
    public void Quit()
    {
        MyData a = new MyData();
        a.coin = PlayerPrefs.GetInt("CoinWorld", 0);
        a.bag = (PlayerPrefs.GetString("Bag", "0, 0, 0"));
        a.skins = (PlayerPrefs.GetString("Skins", "1, 0, 0"));

        string json = JsonUtility.ToJson(a);
        string path = Application.dataPath + "/database.json";
        File.WriteAllText(path, json);
        Debug.Log("Save");
        Application.Quit();
    }
}
