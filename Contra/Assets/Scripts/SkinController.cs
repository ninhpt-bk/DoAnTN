using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] public float maxSpeed = 10;
    [SerializeField] public float forceJump = 200f; // lực nhảy tối đa
    [SerializeField] public float dame = 100f; // sát thương
    [SerializeField] public float maxHp = 100f; // hp tối đa
    [SerializeField] public float speedBullet = 10;
    [SerializeField] public GameObject gun;
    [SerializeField] public GameObject bullet;
    Animator myAnimation;

    private void Start()
    {
        myAnimation = GetComponent<Animator>();
    }
    public void Run(float value)
    {
        myAnimation.SetFloat("Speed", value);
    }
    public float Run()
    {
        return myAnimation.GetFloat("Speed");
    }
    public void Jump(bool value)
    {
      myAnimation.SetBool("Jump", value);
    }
    public void Attack(bool value)
    {
        myAnimation.SetBool("Attack", value);
    }
    public void Sit(bool value)
    {
        myAnimation.SetBool("Sit", value);
    }
    public void Die()
    {
        myAnimation.SetBool("Die", true);
    }
}
