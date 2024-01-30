using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;

    void Update() {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Damageable playerDamageable = other.gameObject.GetComponent<Damageable>();
            if(playerDamageable) playerDamageable.Damage(damage);
            else Debug.LogError("Error: PlayerDamageable missing.");

            Destroy(this.gameObject);
        }
        
    }
}
