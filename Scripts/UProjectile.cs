using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class UProjectile : MonoBehaviour {
    
    [SerializeField] protected float flySpeed = 20f;
    [SerializeField] protected Rigidbody2D rigidbody2D;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected int damage = 20;
    // Start is called before the first frame update
    protected void Start() {
        // Destroy(gameObject,);
        rigidbody2D.velocity = flySpeed * transform.up;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out var currentEnemy))
        {
            // ´«µÝÉËº¦Öµ
            currentEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
