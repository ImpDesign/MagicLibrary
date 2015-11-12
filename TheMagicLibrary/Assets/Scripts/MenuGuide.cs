using UnityEngine;
using System.Collections;

public class MenuGuide : MonoBehaviour {

    public GameObject menuPages;
    public GameObject book;
    public GameObject backButton;
    public GameObject nextButton;

    private GameObject[] pages;
    private int currentpage = 0;
    private int maxPage = 0;
    private bool isOpen = false;

	void Start () {

        pages = new GameObject[menuPages.transform.childCount];
        for(int i = 0; i < menuPages.transform.childCount; i++)
        {
            pages[i] = menuPages.transform.GetChild(i).gameObject;
            maxPage = i;
        }
	
	}

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isOpen)
            {
                Time.timeScale = 0;
                Open();
            }
            else if (currentpage != 0)
            {
                pages[currentpage].SetActive(false);
                currentpage = 0;
                pages[currentpage].SetActive(true);
            }
            else
            {
                Close();
            }

        }
    }

    public void Open ()
    {
        currentpage = 0;
        book.SetActive(true);
        pages[currentpage].SetActive(true);
    }

    public void Close ()
    {
        book.SetActive(false);
        pages[currentpage].SetActive(false);
    }

    public void Info ()
    {
        pages[currentpage].SetActive(false);
        currentpage = 1;
        pages[currentpage].SetActive(true);
        nextButton.SetActive(true);
        backButton.SetActive(true);
    }

    public void NextPage()
    {
        pages[currentpage].SetActive(false);
        currentpage++;
        pages[currentpage].SetActive(true);
        if(currentpage == maxPage)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }

    public void BackPage()
    {
        pages[currentpage].SetActive(false);
        currentpage--;
        pages[currentpage].SetActive(true);
        if(currentpage == 0)
        {
            nextButton.SetActive(false);
            backButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }
}
