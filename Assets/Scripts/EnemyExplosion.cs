using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : IAenemy
{
    private bool hasExploded = false;
    public GameObject explosionPrefab;
    private void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.CompareTag("Player"))
       {
            hasExploded = true;
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            other.gameObject.GetComponent<HealthPlayer>().TakeDamage(20, other.GetContact(0).normal);
            Destroy(explosion, 0.4f);
            Destroy(gameObject);
       }

    }


}
