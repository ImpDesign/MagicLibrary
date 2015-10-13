using UnityEngine;
using System.Collections;

public class InvisiblePlatformGizmo : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size*2);
    }
}
