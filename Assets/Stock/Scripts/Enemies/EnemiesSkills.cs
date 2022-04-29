using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSkills  : MonoBehaviour
{
    [SerializeField] private float cubeSkillPointsPercentHeal;
    [SerializeField] private float bigCubeSkillUnderHealthValue;
    [SerializeField] private EnemiesManager enemiesManager;
 

    public void SkillCube()
    {
        float healthToadd = enemiesManager.enemyTypes[0].maxHealthPoints * cubeSkillPointsPercentHeal / 100;
        foreach (EnemyController enemyController in enemiesManager.enemyControllers)
        {
            if (enemyController.name == enemiesManager.enemyTypes[0].enemyName)
            {
                enemyController.healthPoints += healthToadd;
            }
        }
    }
    public void SkillBigCube()
    {
        foreach (EnemyController enemyController in enemiesManager.enemyControllers)
        {
            if (enemyController.healthPoints < bigCubeSkillUnderHealthValue * enemyController.maxHealthPoints)
            {
                enemyController.healthPoints += enemyController.maxHealthPoints;
            }
        }
    }
    public void SkillSphere()
    {
        foreach (EnemyController enemyController in enemiesManager.enemyControllers)
        {
            if (enemyController.name == enemiesManager.enemyTypes[2].enemyName)
            {
                enemyController.enemySpeed += 0.1f * enemyController.enemySpeed;
            }
        }
        return;

    }
    public void SkillBigSphere()
    {
        foreach (EnemyController enemyController in enemiesManager.enemyControllers)
        {
            if (enemyController.name == enemiesManager.enemyTypes[3].enemyName || enemyController.name == enemiesManager.enemyTypes[2].enemyName)
            {
                enemyController.enemySpeed -= 0.1f * enemyController.enemySpeed;
            }
        }
    }
}
