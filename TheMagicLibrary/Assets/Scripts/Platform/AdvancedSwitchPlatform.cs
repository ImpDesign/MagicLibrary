using UnityEngine;
using System.Collections;

public class AdvancedSwitchPlatform : MonoBehaviour {

    public GameObject fadeOutList;
    public GameObject fadeInList;
    public bool active = false;

    private float speed = 2;
    private float timer = 0;
    private float timer2 = 0;
    private bool isUsed = false;
    private bool isBlocked = false;
    private bool loopBack = false;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private GameObject player;
    private GameObject currentFocus;

    void Start()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }

        startPosition = gameObject.transform.position;
        endPosition = startPosition;
        endPosition.y -= 2f;

        float distance = Vector3.Distance(startPosition, endPosition);
        if (distance != 0)
        {
            speed = speed / distance;
        }
    }

    void Update()
    {
        if (active && !isBlocked)
        {
            player.GetComponent<PlayerController>().SetIsAlive(false);
            timer += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer);
            if (timer >= 1)
            {
                isBlocked = true;
                StopAllCoroutines();
                StartCoroutine(Logic());
            }
        }

        if (loopBack)
        {
            StopAllCoroutines();
            timer2 += Time.deltaTime * 1.25f;
            player.GetComponent<PlayerController>().gameCamera.GetComponent<CameraFollow2D>().setTarget(player);
            if (timer2 >= 1)
            {
                player.GetComponent<PlayerController>().SetIsAlive(true);
                isUsed = true;
                loopBack = false;
            }
        }
    }

    public bool Used
    {
        get
        {
            return isUsed;
        }
    }

    IEnumerator Logic()
    {
        while(true)
        {
            foreach (VanishingDoor door in fadeOutList.GetComponentsInChildren<VanishingDoor>())
            {
                player.GetComponent<PlayerController>().gameCamera.GetComponent<CameraFollow2D>().setTarget(door.gameObject);
                door.active = true;
                yield return new WaitForSeconds(1f);
            }
            foreach (AppearingPlatform platform in fadeInList.GetComponentsInChildren<AppearingPlatform>())
            {
                player.GetComponent<PlayerController>().gameCamera.GetComponent<CameraFollow2D>().setTarget(platform.gameObject);
                platform.active = true;
                yield return new WaitForSeconds(1f);
            }
            loopBack = true;
            yield return null;
        }

    }

    void OnDrawGizmos()
    {
        Vector3 centerPoint = transform.position;
        centerPoint.y -= 2f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(centerPoint, GetComponent<BoxCollider2D>().size * 2);
    }
}
