using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    [SerializeField] float delta;
    //[SerializeField] Transform[] Rect;
    GameObject player;
    GameObject[] childObjects;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        FixedUpdateS();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log(Rect[1].localPosition);*/
        foreach (GameObject childObject in childObjects)
        {
            if (childObject != null)
            {
                if (childObject.transform.localPosition.x > player.transform.position.x - delta && childObject.transform.localPosition.x < player.transform.position.x + delta
                   && childObject.transform.localPosition.y < player.transform.position.y + delta && childObject.transform.localPosition.y > player.transform.position.y - delta)
                    childObject.SetActive(true);
                else
                    childObject.SetActive(false);
            }
            //Debug.Log(player.transform.position);
            /* Debug.Log(childObject.transform.localPosition);
             Debug.Log("\n");*/
        }
    }
    public void FixedUpdateS()
    {
        childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }
    }
}
