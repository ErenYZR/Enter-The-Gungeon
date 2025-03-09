using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    PlayerMovement playerMovement;
	[SerializeField] private float invincibilityDuration = 0.5f;
	private bool invincible = false;

    public event Action<int> OnTakeDamage;

	void Start()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        if (playerMovement.state == PlayerMovement.State.DodgeRoll || invincible) return;
        currentHealth -= damage;

        OnTakeDamage?.Invoke(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator InvincibilityCoroutine()
    {
        invincible = true;
        Debug.Log("Ýnvincible");
        yield return new WaitForSeconds(invincibilityDuration);
        invincible = false;
        Debug.Log("Hasar alýnýr");
    }

    public bool IsFullHealth()
    {
        return currentHealth == maxHealth;
    }

}
