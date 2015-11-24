using UnityEngine;
using System.Collections;

public class TutorialBook : MonoBehaviour
{

    public bool canClose = true;

    private GameObject bookBackground;
    private GameObject pages;
    private GameObject nextButton;
    private GameObject backButton;
    private GameObject resumeButton;

    private GameObject[] bookPages;

    private int currentpage = 0;
    private int MaxPage = 0;

    private bool isOpen = false;
    private bool isInfo = false;
    private bool isLevel = false;

    void Start()
    {

        bookBackground = transform.GetChild(0).gameObject;
        pages = transform.GetChild(1).gameObject;
        nextButton = transform.GetChild(2).gameObject;
        backButton = transform.GetChild(3).gameObject;
        resumeButton = transform.GetChild(4).gameObject;

        bookPages = new GameObject[pages.transform.childCount];
        for (int i = 0; i < pages.transform.childCount; i++)
        {
            bookPages[i] = pages.transform.GetChild(i).gameObject;
            MaxPage = i;
        }

    }

    public void Close()
    {
        bookPages[currentpage].SetActive(false);
        bookBackground.SetActive(false);
    }


    public void NextPage()
    {
        bookPages[currentpage].SetActive(false);
        currentpage++;
        bookPages[currentpage].SetActive(true);
        if (currentpage == MaxPage)
        {
            nextButton.SetActive(false);
            resumeButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            resumeButton.SetActive(false);
        }

        if (currentpage > 0)
        {
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);
        }
    }

    public void BackPage()
    {
        bookPages[currentpage].SetActive(false);
        currentpage--;
        bookPages[currentpage].SetActive(true);
        if (currentpage == MaxPage)
        {
            nextButton.SetActive(false);
            resumeButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            resumeButton.SetActive(false);
        }
        if (currentpage > 0)
        {
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);
        }
    }

    public void Resume()
    {
        Debug.Log("HELLO!");
        Time.timeScale = 1;
        Close();
        Debug.Log("HELLO!2");
    }
}
