using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool optionIsActived = false;
    public GameObject PauseMenuUI;
    public GameObject ConfirmationRespawnToCheckpointUI;
    public GameObject player;
    public GameObject item;
    private Vector3 itemPosition;
    public Animator resumeBanim;
    public Animator collectableBanim;
    public Animator optionBanim;
    public Animator resumeToCpBanim;
    public Animator quitterPartieBanim;
    public Animator ouiMBanim;
    public Animator nonMBanim;
    public Animator ouiRBanim;
    public Animator nonRBanim;
    public Animator creditBanim;
    public Animator creditSceneBanim;

    public GameObject optionUI;
    public GameObject collectableUI;
    public GameObject respawnBDUI;
    public GameObject quitterBDUI;

    public GameObject firstPauseButton;
    public GameObject firstOptionButton;
    public GameObject firstCollectableButton;
    public GameObject[] allCollectableButton;
    public GameObject firstCRButton;
    public GameObject firstCQButton;

    public GameObject closeOptionButton;
    public GameObject closeCollectableButton;
    public GameObject closeCRButton;
    public GameObject closeCQButton;


    private void Start()
    {
        if (transform.tag == "Item")
        {
            itemPosition = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 0);
        }

        //pardon pour ça 

        resumeBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        collectableBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        optionBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        resumeToCpBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        quitterPartieBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        ouiMBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        nonMBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        ouiRBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        nonRBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        creditBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
        creditSceneBanim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }


    // Update is called once per frame
    void Update()
    {
        //Input.GetButtonDown("Options")==>faut corriger ça 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
        /*
        if (optionIsActived)
            
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("dhqsjkdhjdhqsjdqsh");
                PauseMenuUI.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(closeOptionButton);

            }



        }*/
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(firstPauseButton);
        
    }
    public void RestartLevel()
    {
        //respawn du joueur au dernier checkpoint
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = CheckPoints.reachedPoint;

        RespawnPositionItem();

    }



    public void ResumeToCheckPoint()
    {

        RestartLevel();

        ConfirmationRespawnToCheckpointUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu");

    }

    public void OptionMenu()
    {
        optionUI.SetActive(true);
        optionIsActived = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionButton);

        if (Input.GetKeyDown(KeyCode.W))
        {
            PauseMenuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(closeOptionButton);
        }


    }
    public void CollectableMenu()
    {
        collectableUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCollectableButton);

        if (Input.GetButtonDown("Dash"))
        {
            PauseMenuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(closeCollectableButton);
        }
    }
    public void ConfirmationRespawnMenu()
    {
        respawnBDUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCRButton);

        if (Input.GetButtonDown("Dash"))
        {
            PauseMenuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(closeCRButton);
        }
    }
    public void ConfirmationQuitterMenu()
    {
        quitterBDUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCQButton);

        if (Input.GetButtonDown("Dash"))
        {
            PauseMenuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(closeCQButton);
        }
    }
    private void RespawnPositionItem()
    {

        transform.position = itemPosition;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dead")
        {
            RespawnPositionItem();
        }
    }



}
