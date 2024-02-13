using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private Image fillBar;

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            UpdateFillBar();
        }
    }

    // Method to apply damage to the object
    public void Damage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    // Coroutine to handle the object's death
    private IEnumerator Die()
    {
        // Hide health bar
        transform.Find("Healthbar").gameObject.SetActive(false);
        transform.Rotate(-75, 0, 0);

        // Kill AI if it exists
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }

    // Update the health bar UI
    private void UpdateFillBar()
    {
        fillBar.fillAmount = (float)_currentHealth / _maxHealth;
    }

    // Reset the object's health
    private void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
