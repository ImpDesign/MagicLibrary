using UnityEngine;
using System.Collections;

public class InvisibleMovingPlatformGizmo : MonoBehaviour {
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);
    }
}
