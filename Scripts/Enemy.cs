using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    [Header("生命值设置")]
    public int maxHealth = 1000;
    public int currentHealth;
    [Header("移动设置")]
    public float moveSpeed = 3f;
    public float minDistance = 0f;//检测距离
    [Header("射击设置")]
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    public Transform[] firePoints;

    public Transform playerTarget;
    public float nextFireTime;
    protected virtual void Start()
    {
        currentHealth = maxHealth;

    }
    virtual public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // 播放死亡动画/音效
        Destroy(gameObject);
    }
    virtual public void HandleMovement()
    {
        if (playerTarget == null)

        {
            Debug.LogError("未找到玩家对象！请确保玩家有'Player'标签");
            
        }
            {
            // 计算朝向玩家的方向
            Vector2 direction = (playerTarget.position - transform.position).normalized;

            //Debug.DrawLine(Vector2.zero,direction,Color.white,2.5f);

            // 移动
            transform.Translate(
                moveSpeed * Time.deltaTime * direction
            );
        }
    }
    virtual public void HandleShooting()
    {
        if (Time.time >= nextFireTime)
        {
            foreach (Transform point in firePoints)
            {
                Instantiate(
                    projectilePrefab,
                    point.position,
                    point.rotation
                );
            }
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
