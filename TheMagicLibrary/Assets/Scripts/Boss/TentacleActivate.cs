using UnityEngine;
using System.Collections;

public class TentacleActivate : MonoBehaviour {

    public GameObject tentaclePrefab;
    public float tentacleDelay;
    public float attackDelay;

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

    public void AttackPlayer()
    {
        StopAllCoroutines();
        StartCoroutine(AttackSequence());
    }

    public void AttackCenter()
    {
        Debug.Log("AttackCenter");
        Debug.Log("Player x:" + player.transform.position.x + " Player y: " + player.transform.position.y);
        var tentacle = Instantiate(tentaclePrefab) as GameObject;
        tentacle.transform.position = player.transform.position;
        StartCoroutine(TentacleSequence(tentacle));
    }
    public void AttackAhead()
    {
        Debug.Log("AttackAhead");
        Debug.Log("Player x:" + player.transform.position.x + " Player y: " + player.transform.position.y);
        var tentacle = Instantiate(tentaclePrefab) as GameObject;
        spawnPos = new Vector3((player.transform.position.x+10), (player.transform.position.y-2f), 0);
        tentacle.transform.position = spawnPos;
        StartCoroutine(TentacleSequence(tentacle));
    }

    IEnumerator TentacleSequence(GameObject tentacle)
    {
        tentacle.GetComponent<TentacleAI>().Appear();
        yield return new WaitForSeconds(tentacleDelay);
        tentacle.GetComponent<TentacleAI>().Disappear();
    }

    IEnumerator AttackSequence()
    {
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
}
