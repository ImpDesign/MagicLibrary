using UnityEngine;
using System.Collections;

public class InvisibleOneWayGizmo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size * 2);
    }
}
