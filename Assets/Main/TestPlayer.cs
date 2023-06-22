using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour {
    PlayerAction playerAction;

    Vector2 dir1 = Vector2.zero;

    Vector2 dir2 = Vector2.zero
;
    public GameObject Tears;
    public float speed = 1.0f;
    public float tearsSpeed = 0.5f;
    public float range = 6.5f;
    public float maxHealth = 6.0f;

    public float health;

    public float Health {
        get => health;
        set {
            if (health > maxHealth) {
                Debug.Log("최대 체력");
                return;
            }

            health = value;
            Debug.Log("health : " + health);
        }
    }

    Transform body;

    Transform head;

    Animator headAni;

    Animator bodyAni;

    SpriteRenderer bodySR;

    SpriteRenderer headSR;

    IEnumerator fireCoroutin;

    Transform tearspawn;

    private void Awake() {
        health = maxHealth;
        fireCoroutin = TearShootCoroutine();
        playerAction = new PlayerAction();

        tearspawn = transform.GetChild(0);
        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();

        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();
        headSR = head.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        Vector3 dir = new Vector3(dir1.x * speed * Time.deltaTime, dir1.y * speed * Time.deltaTime, 0f);
        transform.position += dir;
    }

    private void OnEnable() {
        playerAction.Move.Enable();
        playerAction.Move.WASD.performed += OnMove;
        playerAction.Move.WASD.canceled += OnMove;
        playerAction.Shot.Enable();
        playerAction.Shot.Cross.performed += OnFire;
        playerAction.Shot.Cross.canceled += OnFire;
    }

    private void OnDisable() {
        playerAction.Shot.Cross.performed -= OnFire;
        playerAction.Shot.Cross.canceled -= OnFire;
        playerAction.Shot.Disable();
        playerAction.Move.WASD.performed -= OnMove;
        playerAction.Move.WASD.canceled -= OnMove;
        playerAction.Move.Disable();
    }

    private void OnMove(InputAction.CallbackContext context) {
        Vector2 value = context.ReadValue<Vector2>();
        dir1 = value;

        if (dir1.x == 0 && dir1.y == 0) {
            headAni.SetBool("isMove", false);
            bodyAni.SetBool("isMove", false);
        } else {
            headAni.SetBool("isMove", true);
            bodyAni.SetBool("isMove", true);

            //headAni.SetFloat("Dir_Y1", dir1.y);
            bodyAni.SetFloat("MoveDir_Y", dir1.y);

            //headSR.flipX = dir1.x < 0;
            bodySR.flipX = dir1.x < 0;

            
            //headAni.SetFloat("Dir_X1", dir1.x);
            bodyAni.SetFloat("MoveDir_X", dir1.x);
        }
    }

    private void OnFire(InputAction.CallbackContext context) {
        Vector2 value = context.ReadValue<Vector2>();
        dir2 = value.normalized;

        if(dir2.x < 1 && dir2.x > -1) {
            dir2.x = 0;
        }
        if (dir2.y < 1 && dir2.y > -1) {
            dir2.y = 0;
        }

        if (dir2.x == 0 && dir2.y == 0) {
            headAni.SetBool("isShoot", false);
        } else {
            headAni.SetBool("isShoot", true);

            headAni.SetFloat("Dir_X2", dir2.x);
            headSR.flipX = dir2.x < 0;

            headAni.SetFloat("Dir_Y2", dir2.y);
        }

        if (context.canceled) {
            StopCoroutine(fireCoroutin);
            dir2.y = 0;
        } else {
            StartCoroutine(fireCoroutin);
        }

        if(dir2.magnitude == 0) {
            StopCoroutine(fireCoroutin);
        }
    }

    IEnumerator TearShootCoroutine() {
        while (true) {
            if (dir2 != Vector2.zero) {
                GameObject tears = Instantiate(Tears);
                tears.transform.position = tearspawn.position;
                tears.gameObject.GetComponent<Bullet>().dir = dir2;
            }
            
            yield return new WaitForSeconds(tearsSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Health--;
            Debug.Log("적과 충돌/ 남은 체력 : " + Health);
        }
    }
}