using UnityEngine;
using System.Collections;

public class AppearingOneWayPlatform : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size);
    }
}
