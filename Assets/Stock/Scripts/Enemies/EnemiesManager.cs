using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using ObjectPooling;

[RequireComponent(typeof(EnemyType))]

public class EnemiesManager : MonoBehaviour
{
    public PlayerController playerController;
    public EnemiesSkills enemiesSkills;
    [SerializeField] public List<EnemyType> enemyTypes = new List<EnemyType>();
    [SerializeField] public List<float> spawnChance = new List<float>();
    [SerializeField] public List<float> sordedspawnChance = new List<float>();
    [SerializeField] private bool canSpawn;
    [SerializeField] public List<EnemyController> enemyControllers = new List<EnemyController>();


    [Range(1, 30)]
    [SerializeField] private int maxEnemiesCount;
    [SerializeField] private int currentPoolEnemy;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 spawningRate;
    [SerializeField] private Vector2 xSpawnPosition;
    [SerializeField] private Vector2 zSpawnPosition;
    [SerializeField] private float ySpawnPosition;

    private float spawnTimer;
    private PrefabPool _pool;


    public List<float> chance = new List<float>();

    private void Awake()
    {
        sordedspawnChance[0] = spawnChance[0] / 100;
        for (int i = 1; i < enemyTypes.Count; i++)
        {
            sordedspawnChance[i] += (sordedspawnChance[i - 1] + spawnChance[i] / 100);
        }
        AdditionalSetupEnemies();
        _pool = new PrefabPool(enemyPrefab, 1);
    }


    //for (int i = 0; i <= enemyTypes.Count; i++)
    //    {
    //        sordedspawnChance[i] = spawnChance[i] - spawnChance[i];
    //    }


    private void AdditionalSetupEnemies()
    {
        foreach (EnemyType enemyType in enemyTypes)
        {
            if (enemyType.enemyName == "bigCube")
            {
                enemyType.enemySpeed = getEnemyTypeByName("cube").enemySpeed * 0.8f;
            }
            if (enemyType.enemyName == "sphere")
            {
                enemyType.enemySpeed = getEnemyTypeByName("cube").enemySpeed * 1.5f;
            }
            if (enemyType.enemyName == "bigSphere")
            {
                enemyType.enemySpeed = getEnemyTypeByName("cube").enemySpeed * 1.5f;
            }
        }
    }

    private EnemyType getEnemyTypeByName(string enemyName)
    {
        foreach (EnemyType enemyType in enemyTypes)
        {
            if (enemyType.enemyName == enemyName)
            {
                return enemyType;
            }
        }
        return null;
    }

    private void Start()
    {
        StartCoroutine(SpawningCoroutine());
    }

    private void Update()
    {
        SpawnTimerUpdate();
    }

    private void SpawnTimerUpdate()
    {
        spawnTimer += Time.deltaTime;
    }

    IEnumerator SpawningCoroutine()
    {
        yield return new WaitUntil(() => CheckCanSpawnAgent());
        spawnTimer = 0;
        yield return new WaitForSeconds(RandomSpawnTime());

        var enemyType = RandomEnemy();

        var agent = _pool.GetInstance() as EnemyController;

        agent.enemiesManager = this;
        agent.transform.position = RandomPosition();
        agent.meshRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        agent.name = enemyType.enemyName;
        agent.transform.localScale = Vector3.one * enemyType.enemySize;
        agent.maxHealthPoints = enemyType.maxHealthPoints;
        agent.healthPoints = enemyType.maxHealthPoints;
        agent.meshFilter.mesh = enemyType.mesh;
        agent.enemySpeed = enemyType.enemySpeed;
        agent.enemyName = enemyType.enemyName;
        enemyControllers.Add(agent);
        StartCoroutine(SpawningCoroutine());
    }

    private EnemyType RandomEnemy()
    {
        float randValue = Random.value ;


        for (int i = 0; i < enemyTypes.Count; i++)
        {
            //Debug.Log(randValue);

            //if (randValue < spawnChance[i])
            if (randValue < sordedspawnChance[i])
            {
                return enemyTypes[i];
            }

        }

        return null;
    }

    private bool CheckCanSpawnAgent()
    {
        if (!canSpawn)
        {
            return false;
        }
        if (spawnTimer <= RandomSpawnTime())
        {
            return false;
        }
        if (enemyControllers.Count >= maxEnemiesCount)
        {
            return false;
        }
        return true;
    }

    private Vector3 RandomPosition()
    {
        float xRandomSpawnPositon = UnityEngine.Random.Range(xSpawnPosition.x, xSpawnPosition.y);
        float zRandomSpawnPositon = UnityEngine.Random.Range(zSpawnPosition.x, zSpawnPosition.y);
        return new Vector3(xRandomSpawnPositon, ySpawnPosition, zRandomSpawnPositon);
    }

    private float RandomSpawnTime()
    {
        return UnityEngine.Random.Range(spawningRate.x, spawningRate.y);
    }
}
