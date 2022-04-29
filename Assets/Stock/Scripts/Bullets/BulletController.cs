using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float damage = 5f;

    [SerializeField] private float bulletSpeed = 5f;
    
    
    public EnemyController enemyController;
    public EnemiesManager enemiesManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            OnContactHit();
        }
    }

    private void OnContactHit()
    {
        Destroy(this.gameObject);
    }

}
