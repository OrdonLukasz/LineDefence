using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        enemyController.transform.position += new Vector3(0, 0, -enemyController.enemySpeed);
    }
}
