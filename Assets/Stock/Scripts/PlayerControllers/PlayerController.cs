using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public BulletController bulletController;

    public float playerDamage = 5f;
    public int playerLives = 5;
    [SerializeField] private float movementSpeed = 5f;


    [SerializeField] private bool alreadyShoted;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private bool cooldownExplosion;
    [SerializeField] private int cooldownExplosionCounter = 0;
    [SerializeField] private int playerLevel = 0;
    [SerializeField] private int playerExpiriencePoints = 0;


    //properties
    public float MaxTimeToClick { get { return _maxTimeToClick; } set { _maxTimeToClick = value; } }
    public float MinTimeToClick { get { return _minTimeToClick; } set { _minTimeToClick = value; } }
    public bool IsDebug { get { return _Isdebug; } set { _Isdebug = value; } }

    //property variables
    private float _maxTimeToClick = 3f;
    private float _minTimeToClick = 0.01f;
    private bool _Isdebug = false;

    //private variables to keep track
    private float _minCurrentTime;
    private float _maxCurrentTime;

    void Update()
    {
        ControllMovement();
    }

    private void ControllMovement()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (this.transform.position.x > -6f)
                MoveObjectTo(this.transform, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z), movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.position.x < 6f)
                MoveObjectTo(this.transform, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z), movementSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            cooldownExplosionCounter++;
            BombShot();
 
        }
    }



    public bool DoubleClick()////////
    {
        if (Time.time >= _minCurrentTime && Time.time <= _maxCurrentTime)
        {
            if (_Isdebug)
            {
                Debug.Log("Double Click");
            }
            _minCurrentTime = 0;
            _maxCurrentTime = 0;
            Debug.Log("true");
            return true;
        }
        _minCurrentTime = Time.time + MinTimeToClick; _maxCurrentTime = Time.time + MaxTimeToClick;
        Debug.Log("false");
        return false;
    }

    private void Shot()
    {
        if (alreadyShoted == false)
        {
            alreadyShoted = true;
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.SetParent(transform.parent);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 1f);
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 5f, -transform.position.z);
            Destroy(bullet, 4);
            //bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, bulletSpeed, -transform.position.z);
            //Destroy(bullet.gameObject,4);
        }
        else
        {
            alreadyShoted = false;
        }
    }

    private void BombShot()
    {

        var bomb = Instantiate(bombPrefab);
        Debug.Log("1");
        if (DoubleClick() == true)
        {
            bomb.GetComponent<ExplodingBullet>().OnBombDetonation();
            Debug.Log("2");
        }
        else
        {
            Debug.Log("3");
            bomb.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 1f);
            bomb.GetComponent<Rigidbody>().velocity = new Vector3(0, 5f, -transform.position.z);
            Destroy(bomb, 4);
        }
        
        
    }

    private void MoveObjectTo(Transform objectToMove, Vector3 targetPosition, float moveSpeed)
    {
        StopCoroutine(MoveObject(objectToMove, targetPosition, moveSpeed));
        StartCoroutine(MoveObject(objectToMove, targetPosition, moveSpeed));
    }

    public static IEnumerator MoveObject(Transform playerToMove, Vector3 targetPosition, float moveSpeed)
    {
        float currentProgress = 0;
        Vector3 startPosition = playerToMove.transform.position;

        while (currentProgress <= 1)
        {
            currentProgress += moveSpeed * Time.deltaTime;

            playerToMove.position = Vector3.Lerp(startPosition, targetPosition, currentProgress);

            yield return null;
        }
    }

    public void OnLevelUp()
    {
        playerDamage++;
        OnCooldown();
    }

    public void CollectExpiriencePoints()
    {

    }

    public void OnCooldown()
    {
        
    }

    public void OnEndGame()
    {
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }

}
