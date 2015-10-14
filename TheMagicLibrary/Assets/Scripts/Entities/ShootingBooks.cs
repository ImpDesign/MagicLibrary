using UnityEngine;
using System.Collections;

public class ShootingBooks : MonoBehaviour {

    public Transform bookPrefab;
    public int direction = 1;
    public float books = 2;
    public float path = 30;
    public float speed = 5;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    private float cooldown;
    private float cooldownTimer = 0;

    void Start()
    {
        startPosition = gameObject.transform.position;
        endPosition = startPosition;
        endPosition.x += path * direction;
        cooldown = 1f / books;

        float distance = Vector3.Distance(startPosition, endPosition);
        if (distance != 0)
        {
            speed = speed / distance;
        }
    }



    void Update()
    {
        cooldownTimer += Time.deltaTime * speed;
        if (cooldownTimer > cooldown)
        {
            cooldownTimer = 0;
            var shotTransform = Instantiate(bookPrefab) as Transform;
            shotTransform.position = startPosition;
            BookMover move = shotTransform.gameObject.GetComponent<BookMover>();
            if (move != null)
            {
                move.direction = direction;
                move.speed = speed;
                move.endPosition = endPosition;
                move.startPosition = startPosition;

            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 point1, point2;
        Gizmos.color = Color.red;
        point1 = new Vector3(transform.position.x + (path * direction), transform.position.y, 0);
        Gizmos.DrawLine(transform.position, point1);

        for (int i = 0; i < books; i++)
        {
            point1 = new Vector3((transform.position.x + (path * (i / books)) * direction), transform.position.y + 1f, 0);
            point2 = new Vector3((transform.position.x + (path * (i / books)) * direction), transform.position.y - 1f, 0);
            Gizmos.DrawLine(point1, point2);

            point1 = new Vector3((transform.position.x + (path * (i / books)) * direction) + (2f* direction), transform.position.y + 1f, 0);
            point2 = new Vector3((transform.position.x + (path * (i / books)) * direction) + (2f* direction), transform.position.y - 1f, 0);
            Gizmos.DrawLine(point1, point2);

            point1 = new Vector3((transform.position.x + (path * (i / books)) * direction) +(2f* direction), transform.position.y + 1f, 0);
            point2 = new Vector3((transform.position.x + (path * (i / books)) * direction), transform.position.y + 1f, 0);
            Gizmos.DrawLine(point1, point2);

            point1 = new Vector3((transform.position.x + (path * (i / books)) * direction) +(2f* direction), transform.position.y - 1f, 0);
            point2 = new Vector3((transform.position.x + (path * (i / books)) * direction), transform.position.y - 1f, 0);
            Gizmos.DrawLine(point1, point2);
        }
    }
}
