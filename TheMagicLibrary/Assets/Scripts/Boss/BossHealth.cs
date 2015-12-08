using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

    public float delay;
    public int health;
    public int damage;
    public GameObject healthbar;
    public GameObject boarder;
    public GameObject background;
    public GameObject text;

    private int currentHealth;
    private bool isDead = false;
    private float delayTimer = 0;

    void Start()
    {
        currentHealth = health;
    }

    void Update()
    {
        if(isDead)
        {
            delayTimer += Time.deltaTime;
            if(delayTimer > delay)
            {
                GetComponent<BossKill>().active = true;
            }
        }
    }

    public void TakeDamage()
    {
        currentHealth = currentHealth -damage;
        float normalizedHealth = (float)currentHealth / (float)health;

        healthbar.GetComponent<RectTransform>().anchorMax = new Vector2((normalizedHealth * .63f) + .18f, .945f);
        if (currentHealth <= 0)
        {
            healthbar.SetActive(false);
            background.SetActive(false);
            boarder.SetActive(false);
            text.SetActive(false);
            isDead = true;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
