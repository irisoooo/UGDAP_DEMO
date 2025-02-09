using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCircle : Enemy
{
    [Header("旋转设置")]
    [SerializeField] private float rotationSpeed = 30f;

    protected override void Start()
    {

        base.Start(); // 调用基类初始化血量
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
        base.HandleMovement();
        base.HandleShooting();
    }
}


