using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float time = 0.5f;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
