using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

    public GameObject tentaclePrefab;
    public float attackDelayMin;
    public float attackDelayMax;
    public bool underPlayer;
    public bool frontPlayer;

    private TentacleActivate activate;
    private float delayTimer;
    private float attackType;
    private bool off;
    private int randomNum;
    private float attackDelay;

	void Start ()
    {
        activate = GetComponent<TentacleActivate>();
        delayTimer = 0;
        attackDelay = Random.Range(attackDelayMin, attackDelayMax);
        if(!underPlayer && !frontPlayer)
        {
            off = true;
        }
        else
        {
            off = false;
        }
	}

	void Update ()
    {
        if(!off)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer > attackDelay)
            {
                if (underPlayer && !frontPlayer)
                {
                    activate.AttackCenter();
                    delayTimer = 0;
                    attackDelay = Random.Range(attackDelayMin, attackDelayMax);
                }
                else if (!underPlayer && frontPlayer)
                {
                    activate.AttackAhead();
                    delayTimer = 0;
                    attackDelay = Random.Range(attackDelayMin, attackDelayMax);
                }
                else
                {
                    randomNum = Random.Range(0, 10);
                    if (randomNum > 5)
                    {
                        activate.AttackCenter();
                        delayTimer = 0;
                        attackDelay = Random.Range(attackDelayMin, attackDelayMax);
                    }
                    else
                    {
                        activate.AttackAhead();
                        delayTimer = 0;
                        attackDelay = Random.Range(attackDelayMin, attackDelayMax);
                    }
                }
            }
        }
	}
}
