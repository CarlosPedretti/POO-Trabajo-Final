using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAenemy : MonoBehaviour
{
    public GameObject[] points;
    public float speed;
    public float time;
    private int random;
    private int currentIndex = 0;
    private bool isMovingRight = true;

    private void Start()
    {
        points = GameObject.FindGameObjectsWithTag("point");
        random = Random.Range(0, points.Length);
    }

    private void Update()
    {
        MoveToNextPoint();
        CheckArrivedAtPoint();
        UpdateCharacterDirection();
    }

    private void MoveToNextPoint()
    {
        Vector3 targetPosition = points[random].transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void CheckArrivedAtPoint()
    {
        float distance = Vector2.Distance(transform.position, points[random].transform.position);
        if (distance < 0.2f)
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                time = 0;
                currentIndex = random;
                random = GetRandomPointIndex();
            }
        }
    }

    private int GetRandomPointIndex()
    {
        int newIndex = Random.Range(0, points.Length);
        while (newIndex == currentIndex)
        {
            newIndex = Random.Range(0, points.Length);
        }
        return newIndex;
    }

    private void UpdateCharacterDirection()
    {
        if (transform.position.x < points[random].transform.position.x && !isMovingRight)
        {
            isMovingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x > points[random].transform.position.x && isMovingRight)
        {
            isMovingRight = false;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}