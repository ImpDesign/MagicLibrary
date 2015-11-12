using UnityEngine;
using System.Collections;

public class MenuGuide : MonoBehaviour {

    public bool canClose = true;

    private GameObject bookBackground;
    private GameObject bookPages;
    private GameObject mainPage;
    private GameObject infoPageList;
    private GameObject levelPageList;
    private GameObject nextButton;
    private GameObject backButton;

    private GameObject[] infoPages;
    private GameObject[] levelPages;

    private int currentpage = 0;
    private int infoMaxPage = 0;
    private int levelMaxPage = 0;

    private bool isOpen = false;
    private bool isInfo = false;
    private bool isLevel = false;

	void Start () {

        bookBackground = transform.GetChild(0).gameObject;
        bookPages = transform.GetChild(1).gameObject;
        nextButton = transform.GetChild(2).gameObject;
        backButton = transform.GetChild(3).gameObject;

        mainPage = bookPages.transform.GetChild(0).gameObject;
        infoPageList = bookPages.transform.GetChild(1).gameObject;
        levelPageList = bookPages.transform.GetChild(2).gameObject;

        infoPages = new GameObject[infoPageList.transform.childCount];
        for(int i = 0; i < infoPageList.transform.childCount; i++)
        {
            infoPages[i] = infoPageList.transform.GetChild(i).gameObject;
            infoMaxPage = i;
        }

        levelPages = new GameObject[levelPageList.transform.childCount];
        for (int i = 0; i < levelPageList.transform.childCount; i++)
        {
            levelPages[i] = levelPageList.transform.GetChild(i).gameObject;
            levelMaxPage = i;
        }

    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isOpen && !isInfo && !isLevel)
            {
                Time.timeScale = 0;
                Open();
            }
            else if (isInfo)
            {
                Debug.Log("currentpage: " + currentpage);
                infoPages[currentpage].SetActive(false);
                currentpage = 0;
                mainPage.SetActive(true);
                nextButton.SetActive(false);
                backButton.SetActive(false);
                isInfo = false;
            }
            else if (isLevel)
            {
                levelPages[currentpage].SetActive(false);
                currentpage = 0;
                mainPage.SetActive(true);
                nextButton.SetActive(false);
                backButton.SetActive(false);
                isLevel = false;
            }
        }
    }

    public void Open ()
    {
        currentpage = 0;
        bookBackground.SetActive(true);
        mainPage.SetActive(true);
    }

    public void Close ()
    {
        if (isInfo)
        {
            infoPages[currentpage].SetActive(false);
            currentpage = 0;
        }
        else if (isLevel)
        {
            levelPages[currentpage].SetActive(false);
            currentpage = 0;
        }
        mainPage.SetActive(false);
        bookBackground.SetActive(false);
    }

    public void Info ()
    {
        isInfo = true;
        mainPage.SetActive(false);
        currentpage = 0;

        infoPages[currentpage].SetActive(true);
        nextButton.SetActive(true);
    }

    public void Levels ()
    {
        isLevel = true;
        mainPage.SetActive(false);
        currentpage = 0;

        levelPages[currentpage].SetActive(true);
        nextButton.SetActive(true);
    }

    public void NextPage()
    {
        if(isLevel)
        {
            levelPages[currentpage].SetActive(false);
            currentpage++;
            levelPages[currentpage].SetActive(true);
            if (currentpage == levelMaxPage)
            {
                nextButton.SetActive(false);
            }
            else
            {
                nextButton.SetActive(true);
            }
        }
        else if (isInfo)
        {
            infoPages[currentpage].SetActive(false);
            currentpage++;
            infoPages[currentpage].SetActive(true);
            if (currentpage == infoMaxPage)
            {
                nextButton.SetActive(false);
            }
            else
            {
                nextButton.SetActive(true);
            }
        }

        if(currentpage > 0)
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
        if (isLevel)
        {
            levelPages[currentpage].SetActive(false);
            currentpage--;
            levelPages[currentpage].SetActive(true);

            if (currentpage == levelMaxPage)
            {
                nextButton.SetActive(false);
            }
            else
            {
                nextButton.SetActive(true);
            }
        }
        else if (isInfo)
        {
            infoPages[currentpage].SetActive(false);
            currentpage--;
            infoPages[currentpage].SetActive(true);

            if (currentpage == levelMaxPage)
            {
                nextButton.SetActive(false);
            }
            else
            {
                nextButton.SetActive(true);
            }
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

    public void Quit ()
    {
        Application.Quit();
    }

    public void Exit ()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }

    public void loadLevel ()
    {
        Time.timeScale = 1;
        Application.LoadLevel((currentpage + 1));
    }

    public void Play ()
    {
        Time.timeScale = 1;
        Application.LoadLevel(1);
    }

    public void Resume ()
    {
        Time.timeScale = 1;
        Close();
    }
}
