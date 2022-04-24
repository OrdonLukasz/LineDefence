using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

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

}
