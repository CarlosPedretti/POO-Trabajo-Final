using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAenemy : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private Transform[] points;
   [SerializeField] private float distance;
    private int randomNumber;
    private SpriteRenderer spriteRenderer;



    private void Start()
    {
     randomNumber = Random.Range(0, points.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Spin();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[randomNumber].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, points[randomNumber].position)<distance)
        {
            randomNumber = Random.Range(0, points.Length);
            Spin();
        }
    }

    private void Spin()
    {
        if(transform.position.x < points[randomNumber].position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}