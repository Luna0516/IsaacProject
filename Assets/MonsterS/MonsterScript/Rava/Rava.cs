using UnityEngine;
public class Rava : EnemyBase
{
    Animator animator;
    Vector2 targetPosition;
    GameObject Ravanian;
    SpriteRenderer sprite;
    public float jumpingTerm = 1.25f;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;

    protected override void Movement()
    {
        transform.Translate(Time.fixedDeltaTime * speed * targetPosition);
    }

    protected override void Awake()
    {
        UpdateCooltimer += wewantnoNull;
        rig = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Ravanian = transform.GetChild(0).gameObject;
        sprite = Ravanian.GetComponent<SpriteRenderer>();
        transform.position = transform.position;
        jumpingTerm = Random.Range(1f, 2f);
    }
     protected override void Start()
    {
        base.Start();
        animator.SetFloat("speed", jumpingTerm);
        targetPosition = Vector2.zero;
        cooltimeStart(2, jumpingTerm);
    }

    void moveingRava()
    {
        if (!coolActive2)
        {
            SetNextTargetPosition();
        }
    }
    protected override void Update()
    {
        UpdateCooltimer();
        orderInGame(sprite);
        damageoff(sprite);
        moveingRava();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void SetNextTargetPosition()
    {
        allcoolStop();
        float x;
        float y;
        x = Random.Range(MinX, MaxX);
        y = Random.Range(MinY, MaxY);
        targetPosition.x = x;
        targetPosition.y = y;
        if (x > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
        targetPosition.Normalize();
        cooltimeStart(2, jumpingTerm);
    }
    public  override void Hitten()
    {
        base.Hitten();
        damaged(sprite);
    }
}
