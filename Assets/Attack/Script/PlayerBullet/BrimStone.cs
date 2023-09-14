using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimStone : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite sprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = GetComponent<Sprite>();

    }
    private void Start()
    {
                
        
    }
}
