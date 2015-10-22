﻿using UnityEngine;
using System.Collections;

public class InvisibleTeleportingPlatform : MonoBehaviour {


    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);

        Gizmos.color = Color.white;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 1.9f);

        Gizmos.color = Color.black;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size);

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * .9f);
    }
}
