using UnityEngine;
using System.Collections;

public class SpinWeb : MonoBehaviour {
    private LineRenderer lineRenderer;


	void Start () {

        lineRenderer = GetComponent<LineRenderer>();
        Vector3 start = transform.position;
        start.y += 1f;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetWidth(.1f, .1f);
    }
	
	void Update () {

        lineRenderer.SetPosition(1, transform.position - new Vector3(0, -.7f, 0));
	
	}
}
