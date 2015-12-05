using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour
{

    public GameObject tutorialPage;
    private GameObject player;
    private bool used = false;

    void Start()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            if (!used)
            {
                tutorialPage.SetActive(true);
                used = true;
                Time.timeScale = 0;
            }
        }
    }
}
