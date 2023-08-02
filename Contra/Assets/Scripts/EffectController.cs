using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject effectBullet;
   // [SerializeField] GameObject effectNPC;
    public void Instantiate(int type, Vector3 position, Quaternion rotation)
    {
        switch (type)
        {
            case 0:
                {
                    Instantiate(effectBullet, position, rotation);
                    break;
                }
          /*  case 1:
                {
                    Instantiate(effectNPC, position, rotation);
                    break;
                }*/
        }
    }
}
