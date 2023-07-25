using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthPlayer : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;
    private Player player;
    private DamageFlash damageFlash;
    [SerializeField] private GameObject hitEffect;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GetComponent<Player>();
        damageFlash = GetComponent<DamageFlash>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
          
            Destroy(gameObject, 1f);
            SceneManager.LoadScene("Dead");

        }
        damageFlash.CallDamageFlash();
    }







}
