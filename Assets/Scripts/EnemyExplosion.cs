using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : IAenemy
{

    [SerializeField] private GameObject hitEffect;


    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            CinemachineMovement.Instance.MoveCamera(10, 10, 0.5f);

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }

    }


}