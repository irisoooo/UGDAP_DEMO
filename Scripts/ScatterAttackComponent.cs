using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterAttackComponent : AbstractAttackComponent
{
    [SerializeField] private GameObject projectileGameObject;
    [SerializeField] private float scatterAngle = 40f;

    Vector2 GenerateRandomVector(Vector2 baseDirection)
    {
        // 计算随机角度
        float randomAngle = Random.Range(-scatterAngle / 2f, scatterAngle / 2f);

        // 将基准方向转换为弧度
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

        // 计算新的角度
        float newAngle = baseAngle + randomAngle;

        // 将角度转换为弧度
        float radians = newAngle * Mathf.Deg2Rad;

        // 计算对应的Vector2
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        // 返回指定长度的向量
        return direction.normalized;
    }
    
    public override void Attack(Vector2 startPoint, Vector2 forward) {
        Vector2 newForward = GenerateRandomVector(forward);
        GameObject projectile = Instantiate(projectileGameObject, startPoint + newForward * (new Vector2(0.8f, 0.8f)), Quaternion.identity);
        projectile.transform.up = newForward;
    }
}
