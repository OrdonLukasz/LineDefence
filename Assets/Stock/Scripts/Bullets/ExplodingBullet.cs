using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    public float dieTime = 4f;
    public GameObject explosionEffect;

    public void Start()
    {
        explosionEffect.SetActive(false);
    }

    public void OnBombDetonation()
    {
        Explode();
        //Destroy(this.gameObject, 0f);
    }

    private void Explode()
    {
        explosionEffect.SetActive(true);
        //GameObject explolosion = Instantiate(explosionEffect);
        explosionEffect.transform.position = transform.position;
        //explolosion.transform.parent = transform;
        explosionEffect.transform.SetParent(null);
        Destroy(explosionEffect, 4f);
    }
}
