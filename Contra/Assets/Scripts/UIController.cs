using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*using Unity.VisualScripting.Dependencies.NCalc;*/

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject HuongDan;
    [SerializeField] TextMeshProUGUI textHuongDan;
    [SerializeField] GameObject NPC;
    [SerializeField] TextMeshProUGUI textNPC;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] GameObject Dame;
    [SerializeField] TextMeshProUGUI textCoin;
    GameController gameController;
    [SerializeField] GameObject menuPause;
    [SerializeField] ItemShow[] arrItem;
    [SerializeField] ButtonItem[] arrBtnItem;
    // Start is called before the first frame update
    void Start()
    {
        HuongDan.SetActive(false);
        menuPause.SetActive(false);
        if(NPC != null)
           NPC.SetActive(false);
        gameController = GetComponent<GameController>();
        for (int i = 0; i < arrItem.Length; i++)
        {
            arrItem[i].slider.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetPause())
            return;
        for(int i = 0; i<arrItem.Length; i++)
        {
            if (arrItem[i].m_time > 0)
            {
                arrItem[i].m_time -= Time.deltaTime;
                arrItem[i].slider.value = arrItem[i].m_time;
                if (arrItem[i].m_time <= 0)
                {
                    arrItem[i].slider.gameObject.SetActive(false);
                    gameController.SetUseItem(i);
                }
            }
        }
    }
    public void UseItem(int type, string s)
    {
        ShowBtnItem(type, s);
        if (type == 2)
            return;
        arrItem[type].m_time = arrItem[type].time;
        arrItem[type].slider.value = arrItem[type].time;
        arrItem[type].slider.maxValue = arrItem[type].time;
        arrItem[type].slider.gameObject.SetActive(true); 
    }
    public void ShowBtnItem(int type, string s)
    {
        arrBtnItem[type].num.text = "x" + s;
    }
    public void MenuPause(bool value)
    {
        menuPause.SetActive(value);
    }
    public void OnCoin(string s)
    {
        textCoin.text = s;
    }
    public void OnDame(float dame, Vector3 index)
    {
        if (dame == 0)
            return;
        TMP_Text textDame = Instantiate(Dame, index, Quaternion.Euler(new Vector3(0, 0, 0)), gameCanvas.transform).GetComponent<TMP_Text>();
        textDame.text = dame.ToString();
    }
    public void OnHuongDan(string s){
        textHuongDan.text = "Chú ý:\n";
        string[] value = s.Split(";");
        for(int i = 0; i < value.Length; i++)
        {
            textHuongDan.text += value[i] + "\n";
        }
        HuongDan.SetActive(true);
        gameController.SetPause(true);
    }
    public void OfHuongDan(){
        textHuongDan.text = "";
        HuongDan.SetActive(false);
        gameController.SetPause(false);
    }
    public void OnNPC(string s)
    {
        textNPC.text = "NPC:\n";
        textNPC.text += s;
        NPC.SetActive(true);
        gameController.SetPause(true);
    }
    public void OfNPC()
    {
        textNPC.text = "";
        NPC.SetActive(false);
        gameController.SetPause(false);
    }
}
[System.Serializable]
class ItemShow
{
    //public GameObject goSlider;
    public Slider slider;
    public float m_time;
    public float time;
}
[System.Serializable]
class ButtonItem
{
    public GameObject btn;
    public TextMeshProUGUI num;
}
