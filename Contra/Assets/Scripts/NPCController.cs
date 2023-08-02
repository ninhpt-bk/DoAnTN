using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] string value;
    UIController controller;
    EffectController effectController;
    float time = 0;
    void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<UIController>();
        effectController = GameObject.FindWithTag("GameController").GetComponent<EffectController>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(time > 0)
                controller.OnNPC("Hãy quay trở lại sau: " + time.ToString() + "s");
            else
            {
                controller.OnNPC(value);
                Invoke("Go", 2);
            }
        }
    }
    private void Update()
    {
        if(time > 0)
            time -= Time.deltaTime;
    }
    public void ResetTime()
    {
        time = 10;
    }
    public void Go()
    {
        //effectController.Instantiate(1, key.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        NPCController npcKey = key.GetComponent<NPCController>();
        npcKey.ResetTime();
        controller.OfNPC();
        if(time > 0)
        {
            return;
        }
        time = 10;
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = key.transform.position;
    }
}
