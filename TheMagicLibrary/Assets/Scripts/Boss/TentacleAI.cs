using UnityEngine;
using System.Collections;

public class TentacleAI : MonoBehaviour {

    public float particleDelay;
    public float deathDelay;
    public float injuredDelay;
    public float speed = 0.3f;
    public float injuredSpeed = 1f;
    public ParticleSystem particlePrefab;
    public bool isTemp = false;

    private bool isAlive = false;
    private bool injured = false;
    private bool startInjured = false;
    private bool isInjured = false;
    private bool dying = false;
    private bool startDying = false;
    private bool appearing = false;
    private bool disappearing = false;
    private bool startDisappearing = false;
    private bool healing = false;
    private bool respawning = false;
    private float delayTimer = 0f;
    private float healingTimer = 0f;
    private float respawnTimer = 0f;
    private Vector3 originalPos;
    private Vector3 injuredPos;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 customPos;
    private ParticleSystem particles;
    private Color color;

	void Awake ()
    {
        customPos = transform.position;
        originalPos = transform.position;
        startPos = new Vector3(transform.position.x, (transform.position.y - 9f), transform.position.z);
        injuredPos = new Vector3(transform.position.x, (transform.position.y - 4f), transform.position.z);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        transform.position = startPos;

        if(!isTemp)
        {
            Appear();
        }
    }
	
	void Update ()
    {
        //particles.transform.rotation = new Quaternion(-90f, 0, 0, 0);
        if (appearing)
        {
            delayTimer += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, originalPos, delayTimer);
            if (delayTimer > 0.5f)
            {
                GetComponent<PolygonCollider2D>().enabled = true;
            }
            if (delayTimer > 1)
            {
                appearing = false;
                delayTimer = 0;
            }
        }
        else if(disappearing)
        {
            if (startDisappearing)
            {
                delayTimer = 0;
                currentPos = transform.position;
                startDisappearing = false;
            }
            delayTimer += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(currentPos, startPos, delayTimer);
            if (delayTimer > 0.5f)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
            }
            if (delayTimer > 1)
            {
                disappearing = false;
                delayTimer = 0;
                GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(FadeOutSequence());
            }
        }
        else if(injured)
        {
            if (startInjured)
            {
                currentPos = transform.position;
                delayTimer = 0;
                startInjured = false;
            }
            delayTimer += Time.deltaTime * injuredSpeed;
            transform.position = Vector3.Lerp(currentPos, injuredPos, delayTimer);
            if (delayTimer > 1)
            {
                injured = false;
                healing = true;
                healingTimer = 0;
                delayTimer = 0;
                currentPos = transform.position;
            }
        }
        else if (dying)
        {
            if (startDying)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
                currentPos = transform.position;
                delayTimer = 0;
                startDying = false;
            }
            delayTimer += Time.deltaTime * injuredSpeed;
            transform.position = Vector3.Lerp(currentPos, startPos, delayTimer);
            if (delayTimer > 1)
            {
                dying = false;
                respawning = true;
                respawnTimer = 0;
                delayTimer = 0;
                currentPos = transform.position;
            }
        }
        else if (healing)
        {
            healingTimer += Time.deltaTime;
            if(healingTimer > injuredDelay)
            {
                delayTimer += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(currentPos, originalPos, delayTimer);
                if (delayTimer > 1)
                {
                    healing = false;
                    isInjured = false;
                    delayTimer = 0;
                }
            }
        }
        else if (respawning)
        {
            respawnTimer += Time.deltaTime;
            if(respawnTimer > deathDelay)
            {
                delayTimer += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(currentPos, originalPos, delayTimer);
                if (delayTimer > 0.5f)
                {
                    GetComponent<PolygonCollider2D>().enabled = true;
                }
                if (delayTimer > 1)
                {
                    respawning = false;
                    isInjured = false;
                    delayTimer = 0;
                }
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "FireBall")
        {
            if(!isInjured)
            {
                injured = true;
                startInjured = true;
                isInjured = true;
            }
            else
            {
                injured = false;
                healing = false;
                dying = true;
                startDying = true;
            }
            DestroyObject(col.gameObject);
        }
    }

    public void Appear()
    {
        isAlive = true;
        particles = Instantiate(particlePrefab, new Vector3(customPos.x, (customPos.y - 9f), customPos.z), new Quaternion(-90f, 0, 0, 0)) as ParticleSystem;
        if(isTemp)
        {
            ChangeParticleSpeed();
        }
        StopAllCoroutines();
        StartCoroutine(FadeInSequence());
    }

    public void Disappear()
    {
        disappearing = true;
        startDisappearing = true;
        isAlive = false;
    }

    public void Custom(Vector3 newPos, float newAttackSpeed, float newParticleDelay)
    {
        customPos = newPos;
        originalPos = newPos;
        startPos = new Vector3(newPos.x, (newPos.y - 9f), newPos.z);
        injuredPos = new Vector3(newPos.x, (newPos.y - 4f), newPos.z);
        transform.position = startPos;
        isTemp = true;
        speed = newAttackSpeed;
        particleDelay = newParticleDelay;
    }

    public void ChangeParticleSpeed()
    {
        particles.startSize = 2f;
        particles.startSpeed = 8f;
        particles.startLifetime = .75f;
        particles.emissionRate = 1000;
        particles.maxParticles = 3000;
    }

    IEnumerator FadeInSequence()
    {
        yield return new WaitForSeconds(particleDelay);
        GetComponent<SpriteRenderer>().enabled = true;
        appearing = true;
    }

    IEnumerator FadeOutSequence()
    {
        yield return new WaitForSeconds(particleDelay);
        particles.enableEmission = false;
        yield return new WaitForSeconds(particleDelay);
        if(isTemp)
        {
            Destroy(particles.gameObject);
            Destroy(this.gameObject);
        }
    }
}
