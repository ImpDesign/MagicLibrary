using UnityEngine;
using System.Collections;

public class AppearingMovingPlatform : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size);
    }
}
