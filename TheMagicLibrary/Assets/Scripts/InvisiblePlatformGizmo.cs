using UnityEngine;
using System.Collections;

public class InvisiblePlatformGizmo : MonoBehaviour {

    // Use this for initialization

    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(this.transform.position, this.GetComponent<BoxCollider2D>().size*2);
    }
}
