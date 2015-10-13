using UnityEngine;
using System.Collections;

public class InvisibleOneWayGizmo : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);
    }
}
