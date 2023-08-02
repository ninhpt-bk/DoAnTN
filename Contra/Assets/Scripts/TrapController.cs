using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] float dame = 1f;
    [SerializeField] float spawn = 0.3f;
    float m_spawn = 0;

    // Update is called once per frame
    void Update()
    {
        if(m_spawn > 0)
            m_spawn -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
      if(m_spawn <= 0 && collision.tag == "Player") {
            GameObject.FindWithTag("Player").GetComponent<PlayerInformation>().AddDame(dame);
            m_spawn = spawn;
       }  
    }
}
