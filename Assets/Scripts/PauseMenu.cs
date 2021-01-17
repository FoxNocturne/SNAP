using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject ConfirmationRespawnToCheckpointUI;
    public GameObject player;
    public GameObject item;
   // private Vector3 itemPosition;
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

      




    private void Start()
    {
        if (transform.tag == "Item")
        {
            //itemPosition = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 0);
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
    }
    public void RestartLevel()
    {
        //respawn du joueur au dernier checkpoint
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = CheckPoints.reachedPoint;

        //respawn de la caisse a sa position initial
        //item = GameObject.FindGameObjectWithTag("Item");
        //item.transform.position = itemPosition;
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





}
