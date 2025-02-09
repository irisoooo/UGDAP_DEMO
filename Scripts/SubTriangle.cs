using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTriangle : Enemy
{
    [Header("ս������")]
    [SerializeField] private int breakawayDamageThreshold = 50; // ����������˺�ֵ
    [SerializeField] private float followSmoothness = 5f;  // ����ƽ����
    [SerializeField] private float positionUpdateInterval = 0.2f; // λ�ø��¼��

    [Header("�ƶ�����")]
    [SerializeField] private float followOffset = 2f; // ����ƫ�ƾ���
    [SerializeField] private float independentSpeed = 1f; // �����ƶ��ٶ�

    public Transform parent;
    private Vector3 baseLocalPosition; // ��ʼ���λ��
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
            base.HandleShooting(); // �̳и�������߼�
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

     

        


    override public void TakeDamage(int damage)//���˵�һ���̶Ƚ�����ӹ�ϵ

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
        transform.SetParent(null); // ������ӹ�ϵ

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.angularDrag = 0.5f;

        Debug.Log($"{gameObject.name} ��������ƣ�");
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
                // ���㸸������ת�����λ��
                Vector3 parentForward = transform.parent.up;
                targetFollowPos = transform.parent.position + parentForward * followOffset;
            }
        }
    }

    // ƽ�������ƶ�
    private void SmoothFollowMovement()
    {
        if (transform.parent == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            targetFollowPos,
            followSmoothness * Time.fixedDeltaTime
        );

        // ������������
        Vector3 lookDirection = transform.parent.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
