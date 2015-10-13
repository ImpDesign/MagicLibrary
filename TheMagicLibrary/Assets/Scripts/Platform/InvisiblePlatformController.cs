using UnityEngine;
using System.Collections;

public class InvisiblePlatformController : MonoBehaviour {

    public GameObject platformList;
    public bool canReveal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            foreach(SpriteRenderer s in platformList.GetComponentsInChildren<SpriteRenderer>())
            {
                s.enabled = true;
            }
        }
        else
        {
            foreach (SpriteRenderer s in platformList.GetComponentsInChildren<SpriteRenderer>())
            {
                s.enabled = false;
            }

        }

    }
}
