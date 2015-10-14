using UnityEngine;
using System.Collections;

public class CircleBook : MonoBehaviour
{

    public float radius = 6;
    public float speed = 8;

    private Vector3 startPosition;

    void Start()
    {
        radius -= 1.25f;
        speed *= 4;
        startPosition = transform.position;
        transform.position = new Vector3(transform.position.x + radius, transform.position.y, 0);
    }

    void Update()
    {
        transform.RotateAround(startPosition, Vector3.forward, speed * Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.back, speed * Time.deltaTime);

    }

    void OnDrawGizmos()
    {
        Vector3 point;
        Gizmos.color = Color.white;

        point = transform.position;
        point.x -= radius;
        Gizmos.DrawLine(transform.position, point);

        point = transform.position;
        point.x += radius;
        Gizmos.DrawLine(transform.position, point);

        point = transform.position;
        point.y -= radius;
        Gizmos.DrawLine(transform.position, point);

        point = transform.position;
        point.y += radius;
        Gizmos.DrawLine(transform.position, point);

    }
}
