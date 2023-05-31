using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float health;

    
 
    public void TakeDamage(float damage, Vector2 position)
    {
        health -= damage;
       if(health == 0)
        {
            Destroy(gameObject);
        }
    }


    
}
