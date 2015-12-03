using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

    public int health = 100;
    public int damage = 10;

    private int currentHealth;

    void Start() {

        currentHealth = health;

    }

    void Update() {

    }

    public void TakeDamage()
    {
        currentHealth = -damage;
    }
}
