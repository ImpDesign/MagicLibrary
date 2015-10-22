using UnityEngine;
using System.Collections;

public class AppearingCollapsingPlatform : MonoBehaviour {


    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 1.9f);

        Gizmos.color = Color.black;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * .9f);
    }
}
