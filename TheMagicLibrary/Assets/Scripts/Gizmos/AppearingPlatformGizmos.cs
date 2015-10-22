using UnityEngine;
using System.Collections;

public class AppearingPlatformGizmos : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size);
    }
}
