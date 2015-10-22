using UnityEngine;
using System.Collections;

public class AppearingPlatform : MonoBehaviour {


    public bool active = false;

    private Color color;
    private bool set = true;

    void Start()
    {

        color = gameObject.GetComponent<SpriteRenderer>().color;

    }

    void Update()
    {
        if (set && active)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            StopAllCoroutines();
            StartCoroutine("FadeInSequence");
            set = false;
            active = false;
        }

    }

    IEnumerator FadeInSequence()
    {
        color.a = 0;
        while (color.a < 255)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
            color.a += Time.deltaTime;
            yield return null;
        }
    }
}
