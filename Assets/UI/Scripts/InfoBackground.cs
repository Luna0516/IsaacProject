using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoBackground : MonoBehaviour
{
    Animator anim;

    TextMeshProUGUI nameText;
    TextMeshProUGUI infoText;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        Transform child = transform.GetChild(0);
        nameText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        infoText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Player player = GameManager.Inst.Player;

        player.getPassiveItem += SetPassiveItem;
        player.getActiveItem += SetActiveItem;
    }

    void SetPassiveItem(PassiveItemData itemData)
    {
        StopAllCoroutines();
        StartCoroutine(StartAnim(itemData.name, itemData.explain));
    }

    void SetActiveItem(ActiveItemData itemData)
    {
        StopAllCoroutines();
        StartCoroutine(StartAnim(itemData.name, itemData.explain));
    }

    IEnumerator StartAnim(string _name, string _info)
    {
        anim.SetBool("GetItem", true);
        nameText.text = $"{_name}";
        infoText.text = $"{_info}";
        yield return new WaitForSeconds(2.5f);
        anim.SetBool("GetItem", false);
        yield return new WaitForSeconds(0.5f);
        nameText.text = " ";
        infoText.text = " ";
    }
}
