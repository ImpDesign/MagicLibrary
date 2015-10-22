using UnityEngine;
using System.Collections;

public class VanishingDoor : MonoBehaviour {

    public bool active = false;

    private Color color;
    private bool set = true;

	void Start () {

        color = gameObject.GetComponent<SpriteRenderer>().color;
	
	}
	
	void Update () {

        if(active)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if(set && active)
        {
            StopAllCoroutines();
            StartCoroutine("FadeOutSequence");
            set = false;
            active = false;
        }
	
	}

    IEnumerator FadeOutSequence()
    {
        while (color.a >= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
            color.a -= Time.deltaTime;
            //Debug.Log("FadeOut " + color.a);
            yield return null;
        }
        color.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
