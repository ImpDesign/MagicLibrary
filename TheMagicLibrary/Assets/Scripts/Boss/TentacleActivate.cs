using UnityEngine;
using System.Collections;

public class TentacleActivate : MonoBehaviour {

    public GameObject tentaclePrefab;
    public float tentacleDelay;
    public float attackDelay;
    public float attackSpeed;
    public float particleDelay;

    private GameObject player;
    private Vector3 spawnPos;

	void Start ()
    {

        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }
    }

    public void AttackPlayer(bool tier2, bool tier3)
    {
        if(tier2)
        {
            //StopAllCoroutines();
            StartCoroutine(AttackSequence2(6));
        }
        else if (tier3)
        {
            //StopAllCoroutines();
            StartCoroutine(AttackSequence3(6));
        }
        else
        {
            //StopAllCoroutines();
            StartCoroutine(AttackSequence1(4));
        }
    }

    public void AttackCenter()
    {
        var tentacle = Instantiate(tentaclePrefab) as GameObject;
        spawnPos = new Vector3((player.transform.position.x), (player.transform.position.y + 1f), 0);
        tentacle.GetComponent<TentacleAI>().Custom(spawnPos, attackSpeed, particleDelay);
        StartCoroutine(TentacleSequence(tentacle));
    }
    public void AttackAhead()
    {
        var tentacle = Instantiate(tentaclePrefab) as GameObject;
        spawnPos = new Vector3((player.transform.position.x+(10*player.GetComponent<PlayerController>().GetDirection())), (player.transform.position.y + 1f), 0);
        tentacle.GetComponent<TentacleAI>().Custom(spawnPos, attackSpeed, particleDelay);
        StartCoroutine(TentacleSequence(tentacle));
    }

    IEnumerator TentacleSequence(GameObject tentacle)
    {
        tentacle.GetComponent<TentacleAI>().Appear();
        yield return new WaitForSeconds(tentacleDelay);
        tentacle.GetComponent<TentacleAI>().Disappear();
    }

    IEnumerator AttackSequence3(float eyeDelay)
    {
        yield return new WaitForSeconds(eyeDelay);
        AttackCenter();
        yield return new WaitForSeconds(attackDelay);
        AttackCenter();
        yield return new WaitForSeconds(attackDelay);
        AttackAhead();
        yield return new WaitForSeconds(attackDelay);
        AttackCenter();
        yield return new WaitForSeconds(attackDelay);
        AttackCenter();
        yield return new WaitForSeconds(attackDelay);
        AttackAhead();
    }

    IEnumerator AttackSequence1(float eyeDelay)
    {
        yield return new WaitForSeconds(eyeDelay);
        AttackCenter();
    }

    IEnumerator AttackSequence2(float eyeDelay)
    {
        yield return new WaitForSeconds(eyeDelay);
        AttackCenter();
        yield return new WaitForSeconds(attackDelay);
        AttackAhead();
    }
}
