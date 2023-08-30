using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationTrigger : MonoBehaviour
{
    PenetrationTear ptear;
    CircleCollider2D circleCollider;

    private void Awake()
    {
        ptear = GetComponentInParent<PenetrationTear>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ptear.Penetration--;

            if (ptear.isPenetrate)
            {
                ptear.circleCollider.enabled = false;

                if (ptear.Penetration <= 0)
                {

                    ptear.isPenetrate = false;
                    ptear.circleCollider.enabled = true;
                    
                }
            }
        }
    }
}
