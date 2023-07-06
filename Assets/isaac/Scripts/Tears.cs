using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    PlayerAction playerAction;

    public float shotSpeed = 1.00f;

    Vector3 direction = Vector3.zero;

    private void Update()
    {
        //���� �̵�
        transform.position += Time.deltaTime * shotSpeed * direction;

    }

    public void SetTearDirection(Vector2 vec)
    {
        if (vec == Vector2.up)
            direction = Vector3.up;
        else if (vec == Vector2.down)
            direction = Vector3.down;
        else if (vec == Vector2.left)
            direction = Vector3.left;
        else if (vec == Vector2.right)
            direction = Vector3.right;
        else
        {
            //(0,0) �̳� �밢������ ������ �� �߻��� �� ����!
            Debug.LogWarning("���׹��׹���!");
        }
        
    }
}

