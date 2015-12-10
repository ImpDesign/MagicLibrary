using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class Video : MonoBehaviour {

    public MovieTexture movie;

	void Start ()
    {
        GetComponent<Renderer>().material.mainTexture = movie as MovieTexture;
        //GetComponent<AudioSource>() = movie.audioClip;
        movie.Play();
        GetComponent<AudioSource>().Play();
	}

	void Update ()
    {
	
	}
}
