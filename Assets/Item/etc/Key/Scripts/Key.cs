using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        GameManager.Inst.Key += 1;
        this.gameObject.SetActive(false);
    }
}
