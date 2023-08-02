using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
public class LoadingController : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI mess;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        string[] Mes = new string[] { "Chiến thắng trở về!", "Không ngừng tiến lên!", "Hãy tận dụng mọi thứ có thể!" };
        mess.text = "Node: " + Mes[(int)Random.Range(0, Mes.Length)];
        slider.value = 0;
        slider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 0)
        {
            time = Random.Range(0, 1f);
            int x = (int)Random.Range(20, 40);
            slider.value += x;
            if (slider.value >= slider.maxValue)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("LoadMap"));
            }
            dog.transform.position = dog.transform.position + new Vector3(x *0.14f, 0, 0);
        }else
            time -= Time.deltaTime;
    }
}
