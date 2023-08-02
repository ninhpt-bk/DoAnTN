using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UIText : MonoBehaviour
{
    [SerializeField] string text;
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            UIController uiController = GameObject.FindWithTag("GameController").GetComponent<UIController>();
            uiController.OnHuongDan(text);
            gameObject.SetActive(false);
        }
    }
}
