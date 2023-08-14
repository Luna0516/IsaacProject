using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //이 클래스는 테스트용 플레이어 클래스인듯 합니다.(삭제해도 무관할듯?)

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
