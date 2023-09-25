using UnityEngine;

public class ItemCheckBox : MonoBehaviour
{
    bool hasItem = true;

    public int itemPrice;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (hasItem)
            {
                hasItem = false;
                player.Coin -= itemPrice;
            }
        }
    }
}