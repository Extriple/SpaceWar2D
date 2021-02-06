using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Enemy targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;

    public static Arrow Create(Vector3 position, Enemy enemy)
    {
        Transform prefabArrow = Resources.Load<Transform>("pfArrowProjectile");
        Transform arrowTransform = Instantiate(prefabArrow, position, Quaternion.identity);

        Arrow arrow = arrowTransform.GetComponent<Arrow>();
        arrow.SetTarget(enemy);
        
        return arrow;
    }
    
    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        
        
        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            
            Destroy(gameObject);
        }
    }
}
