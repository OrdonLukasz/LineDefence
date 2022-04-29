using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BorderLineController>())
        {
            OnEndOfRoad();
        }
        if (other.GetComponent<BulletController>())
        {
            OnHitedByBullet();
        }
    }

    private void OnEndOfRoad()
    {
        CrossedLine();
    }

    private void CrossedLine()
    {
        if(enemyController.enemiesManager.playerController.playerLives > 0)
        {
            if(enemyController.enemyName == enemyController.enemiesManager.enemyTypes[0].enemyName)
            {
                enemyController.enemiesManager.enemiesSkills.SkillCube();
            }

            enemyController.enemiesManager.playerController.playerLives--;
            enemyController.enemiesManager.enemyControllers.Remove(enemyController);
            enemyController._pool.AcceptReturning(enemyController);
            enemyController.PrepareForPooling();
        }
        else
        {
            enemyController.enemiesManager.playerController.OnEndGame();
        }
    }

    private void OnHitedByBullet()
    {
        if (enemyController.healthPoints > 0)
        {
            enemyController.healthPoints -= enemyController.enemiesManager.playerController.playerDamage;
           
            if (enemyController.enemyName == enemyController.enemiesManager.enemyTypes[2].enemyName)
            {
                enemyController.enemiesManager.enemiesSkills.SkillSphere();
                return;
            }

            if (enemyController.enemyName == enemyController.enemiesManager.enemyTypes[3].enemyName)
            {
                enemyController.enemiesManager.enemiesSkills.SkillBigSphere();
                return;
            }
        }
        else
        {
            if (enemyController.enemyName == enemyController.enemiesManager.enemyTypes[1].enemyName)
            {
                enemyController.enemiesManager.enemiesSkills.SkillBigCube();
            }
            enemyController._pool.AcceptReturning(enemyController);
            enemyController.PrepareForPooling();
        }
    }
}
