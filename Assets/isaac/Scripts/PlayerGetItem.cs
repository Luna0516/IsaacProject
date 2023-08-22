using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerGetItem : MonoBehaviour
{
    Player player;
    Animator bodyAni;
    Animator headAni;
    private void Awake()
    {
        player = GetComponent<Player>();
        Transform body = player.transform.GetChild(2);
        bodyAni = body.GetComponent<Animator>();

        Transform head = player.transform.GetChild(3);
        headAni = head.GetComponent<Animator>();
    }
    public void GetCricket()
    {
        player.State = Player.ItemSpriteState.CricketHead;
        var resourceName = "AC/Cricket_AC";
        headAni.runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load(resourceName));
    }
    public void GetHalo()
    {
        
    }
    public void GetPolyphemus()
    {
        player.IsGetPolyphemus = true;
    }
    public void GetSacredHeart()
    {
        player.IsGetSacredHeart = true;
    }
}
