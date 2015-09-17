using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartLevel(){

        Application.LoadLevel(Application.loadedLevel);

	}

	public void ExitLevel(){

        Application.LoadLevel(0);

    }

    public void Play(){

        Application.LoadLevel(1);

    }

    public void ExitGame(){

        Application.Quit();

    }
}
