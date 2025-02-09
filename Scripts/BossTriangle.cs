using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriangle : Enemy
{
    [Header("Settings")]
   
    [SerializeField] private GameObject[] splitPrefabs; // 分裂后的Boss预制体数组
    [SerializeField] private float splitRadius = 2f; // 分裂生成半径
    [SerializeField] private int currentSplitLevel = 0; // 当前分裂层级（0=初始）
    public float[] splitThresholds = { 0.7f, 0.6f };//当生命值为百分之多少时分裂

    private bool isSplitTriggered = false;

    protected override void Start()
    {
      
        base.Start(); // 调用基类初始化血量

    }

    override public void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        CheckSplitCondition();
       
        
    }

    private void FixedUpdate()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
        base.HandleMovement();
        base.HandleShooting();
    }



    // 检测是否达到分裂条件
    private void CheckSplitCondition()
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        // 根据层级判断分裂阈值（示例：大型→70%，中型→60%）
        

        if (currentSplitLevel < splitThresholds.Length &&
            healthPercentage <= splitThresholds[currentSplitLevel] &&
            !isSplitTriggered)
        {
            isSplitTriggered = true;
            StartCoroutine(SplitBoss());
           
        }
    }

    // 分裂逻辑
    private IEnumerator SplitBoss()
    {
        // 播放分裂动画（如缩放、粒子效果）
        
        yield return new WaitForSeconds(0.1f); 

        // 生成子Boss
        foreach (var prefab in splitPrefabs)
        {
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * splitRadius;
            GameObject childBoss = Instantiate(prefab, spawnPos, Quaternion.identity);

            // 传递分裂层级信息
            if (childBoss.TryGetComponent<BossTriangle>(out var childController))
            {
                childController.currentSplitLevel = currentSplitLevel + 1;
            }
        }

      
    }

   
}
