using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    //TODO instantiate a healthbar on load and have it match the parent transform with a vertial offset

    [SerializeField] private int _maxHealth, _currentHealth;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            UpdateFillBar();
        }
    }

    public void Damage(int amount) {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0) StartCoroutine(Die());
    }

    private IEnumerator Die() {
        //hide health bar
        this.transform.Find("Healthbar").gameObject.SetActive(false);
        this.transform.Rotate(-75, 0, 0);

        //kill ai if exists
        WanderingAI behavior = GetComponent<WanderingAI>();
        if(behavior != null) behavior.SetAlive(false);
        
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }

    #region Healthbar UI
    [SerializeField] private Image fillBar;

    void UpdateFillBar()
    {
        fillBar.fillAmount = (float)_currentHealth / _maxHealth;
    }

    void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
    #endregion
}
