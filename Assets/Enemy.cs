using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Transform targetTransform;
    private Rigidbody2D rigibody2D;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax =.2f;
    

    private void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;

       // lookForTargetTimer = Random.Range(0f, lookForTargetTimer);
    }

    private void Update()
    {
        Vector3 moveDir = (targetTransform.position - transform.position).normalized;

        float moveSpeed = 6f;
        rigibody2D.velocity = moveDir * moveSpeed;

        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                targetTransform = building.transform;
            }
            else
            {
                if ((Vector3.Distance(transform.position, building.transform.position) <
                     Vector3.Distance(transform.position, targetTransform.position)))
                {
                    //Close ! 
                    targetTransform = building.transform;
                }
            }
        }
    }
}
