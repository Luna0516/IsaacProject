using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigid.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
    }
}
