using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCheckBox : MonoBehaviour {
    CircleCollider2D parentColl;

    private void Awake() {
        parentColl = GetComponentInParent<CircleCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("Player 나감");
            parentColl.enabled = true;
            Destroy(gameObject);
        }
    }

}
