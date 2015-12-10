using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class Video : MonoBehaviour {

    public MovieTexture movie;


	void Start ()
    {
        GetComponent<Renderer>().material.mainTexture = movie as MovieTexture;
		movie.Play();
		StartCoroutine (Logic (movie));
	}

	IEnumerator Logic(MovieTexture movie)
	{
		yield return new WaitForSeconds (movie.duration);
		Application.LoadLevel(Application.loadedLevel+1);
		
	}
}
