using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCircle : Enemy
{
    [Header("��ת����")]
    [SerializeField] private float rotationSpeed = 30f;

    protected override void Start()
    {

        base.Start(); // ���û����ʼ��Ѫ��
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
        base.HandleMovement();
        base.HandleShooting();
    }
}


