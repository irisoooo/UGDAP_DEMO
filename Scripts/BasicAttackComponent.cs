using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackComponent : AbstractAttackComponent {

    [SerializeField] private GameObject projectileGameObject;

    public override void Attack(Vector2 startPoint, Vector2 forward) {
        GameObject projectile = Instantiate(projectileGameObject, startPoint + forward * (new Vector2(0.6f, 0.6f)), Quaternion.identity);
        projectile.transform.up = forward;
    }
}
