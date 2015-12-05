using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

    public int health = 100;
    public int damage = 10;

    private int currentHealth;
    private GameObject tentacle;

    void Start() {

        currentHealth = health;
        tentacle = transform.GetChild(0).gameObject;

    }

    void Update() {

    }

    public void TakeDamage()
    {
        currentHealth = -damage;
        tentacle.GetComponent<TentacleAI>().Appear();
    }
}
