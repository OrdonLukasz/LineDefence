using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using ObjectPooling;

public class EnemyController : MonoBehaviour, IPoolable
{
    public float maxHealthPoints;
    public float healthPoints;
    public float enemySpeed;
    public string enemyName;

    [HideInInspector] public MeshFilter meshFilter;
    [HideInInspector] public MeshRenderer meshRenderer;

    [HideInInspector] public EnemiesManager enemiesManager;
    [HideInInspector] public bool active;
    [HideInInspector] public PrefabPool _pool;

    public void SetParentPool(PrefabPool pool)
    {
        _pool = pool;
    }

    public void PrepareForPooling()
    {
        active = false;
        gameObject.SetActive(false);
        return;
    }

    public void HandleSpawn()
    {
        active = true;
        gameObject.SetActive(true);
    }
}
