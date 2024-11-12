using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    private AudioManager audioManager;
    private bool isActivated = false;
    private bool isAtTargetPosition = false;
    private bool playerOnButton = false;

    public float transitionTime = 0.8f;
    public float objectTransitionTime = 1.6f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    public GameObject[] objectsToMove;
    private Vector3[] initialPositions;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        initialPosition = transform.position;
        targetPosition = initialPosition;

        initialPositions = new Vector3[objectsToMove.Length];
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            initialPositions[i] = objectsToMove[i].transform.position;
        }
    }

    void Update()
    {
        if (playerOnButton && !isActivated)
        {
            isActivated = true;
            targetPosition = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
            StartCoroutine(ActivateLever(targetPosition));
            StartCoroutine(MoveObjectsToPosition(1.45f, objectTransitionTime));
        }
        else if (!playerOnButton && isActivated)
        {
            isActivated = false;
            StartCoroutine(ActivateLever(initialPosition));
            StartCoroutine(MoveObjectsToInitialPosition(objectTransitionTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WG") || collision.CompareTag("FB"))
        {
            audioManager.PlaySFX(audioManager.clickButton);
            playerOnButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WG") || collision.CompareTag("FB"))
        {
            playerOnButton = false;
        }
    }

    private IEnumerator ActivateLever(Vector3 destination)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < transitionTime)
        {
            transform.position = Vector3.Lerp(startPosition, destination, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        isAtTargetPosition = (destination == targetPosition);
    }

    private IEnumerator MoveObjectsToPosition(float offsetY, float transitionTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            for (int i = 0; i < objectsToMove.Length; i++)
            {
                Vector3 startPosition = objectsToMove[i].transform.position;
                Vector3 targetPosition = new Vector3(initialPositions[i].x, initialPositions[i].y + offsetY, initialPositions[i].z);

                objectsToMove[i].transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < objectsToMove.Length; i++)
        {
            objectsToMove[i].transform.position = new Vector3(initialPositions[i].x, initialPositions[i].y + offsetY, initialPositions[i].z);
        }
    }

    private IEnumerator MoveObjectsToInitialPosition(float transitionTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            for (int i = 0; i < objectsToMove.Length; i++)
            {
                Vector3 startPosition = objectsToMove[i].transform.position;
                objectsToMove[i].transform.position = Vector3.Lerp(startPosition, initialPositions[i], elapsedTime / transitionTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < objectsToMove.Length; i++)
        {
            objectsToMove[i].transform.position = initialPositions[i];
        }
    }
}
