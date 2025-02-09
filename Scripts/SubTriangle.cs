using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTriangle : Enemy
{
    [Header("战斗设置")]
    [SerializeField] private int breakawayDamageThreshold = 50; // 脱离所需的伤害值
    [SerializeField] private float followSmoothness = 5f;  // 跟随平滑度
    [SerializeField] private float positionUpdateInterval = 0.2f; // 位置更新间隔

    [Header("移动设置")]
    [SerializeField] private float followOffset = 2f; // 跟随偏移距离
    [SerializeField] private float independentSpeed = 1f; // 独立移动速度

    public Transform parent;
    private Vector3 baseLocalPosition; // 初始相对位置
    private Vector3 targetFollowPos;
    private int currentDamageTaken;
    public bool isIndependent = false;
    private float positionUpdateTimer;

    public Rigidbody2D rb;
    protected override void Start()
    {

        base.Start();
        transform.localPosition = baseLocalPosition;
        currentDamageTaken = 0;
    }
    private void Update()
    {
        if (!isIndependent)
        {
            UpdateFollowPosition();
            base.HandleShooting(); // 继承父类射击逻辑
        }
    }
    private void FixedUpdate()
    {

        if (isIndependent)
        {

            HandleMovement();

        }
        else
        {
            SmoothFollowMovement();
        }

    }

     

        


    override public void TakeDamage(int damage)//受伤到一定程度解除父子关系

    {
        currentDamageTaken += damage;
       
            base.TakeDamage(damage);

        if (currentDamageTaken >= breakawayDamageThreshold)
        {
            BreakawayFromParent();
        }
    }


    private void BreakawayFromParent()
    {
        isIndependent = true;
        transform.SetParent(null); // 解除父子关系

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.angularDrag = 0.5f;

        Debug.Log($"{gameObject.name} 已脱离控制！");
    }


    override public void HandleMovement()
    {
        if (playerTarget == null)
        {
            Debug.LogWarning("Player target is not assigned!");
            return;
        }
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        if (rb != null)
        {
            rb.velocity = direction * independentSpeed;
        }
    }
    private void UpdateFollowPosition()
    {
        positionUpdateTimer += Time.deltaTime;
        if (positionUpdateTimer >= positionUpdateInterval)
        {
            positionUpdateTimer = 0;
            if (transform.parent != null)
            {
                // 计算父物体旋转后的新位置
                Vector3 parentForward = transform.parent.up;
                targetFollowPos = transform.parent.position + parentForward * followOffset;
            }
        }
    }

    // 平滑跟随移动
    private void SmoothFollowMovement()
    {
        if (transform.parent == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            targetFollowPos,
            followSmoothness * Time.fixedDeltaTime
        );

        // 保持面向父物体
        Vector3 lookDirection = transform.parent.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
