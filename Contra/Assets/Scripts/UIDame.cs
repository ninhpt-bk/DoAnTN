using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDame : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    void Update()
    {
        transform.position = transform.position + new Vector3(0, speed*Time.deltaTime, 0);
    }
}
