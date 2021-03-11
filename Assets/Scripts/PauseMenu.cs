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

    public bool CollectableInstance = false;
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
    public GameObject MomentLectureUI;    
    public GameObject respawnBDUI;
    public GameObject quitterBDUI;
    public GameObject quitterMenuButton;
    public GameObject respawnMenuButton;

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

    public LayerMask itemLayer;

    public static bool collectableUIisActtived;
    private void Start()
    {
        collectableUIisActtived = false;
        optionIsActived = false;

        player = GameObject.FindGameObjectWithTag("Player");
        item = GameObject.FindGameObjectWithTag("Item");
        itemPosition = new Vector3(item.transform.position.x, item.transform.position.y, 0);
        itemLayer = item.layer;
        

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
        
        if (Input.GetButtonDown("Option")|| Input.GetKeyDown(KeyCode.Escape))
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstPauseButton);
        
    }
    public void RestartLevel()
    {
        //respawn du joueur au dernier checkpoint
 
        player.transform.position = CheckPoints.reachedPoint;
        item.transform.position = itemPosition;
        item.layer = itemLayer;

        //RespawnPositionItem();

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
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionButton);


    }
    public void CollectableMenu()
    {
        collectableUI.SetActive(true);
        collectableUI.GetComponent<CollectablesUI>().UpdateCollectables();
        Time.timeScale = 0f;
        collectableUIisActtived = true;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCollectableButton);
   
    }

    public void ConfirmationRespawnMenu()
    {
        respawnBDUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCRButton);

    }
    public void ConfirmationQuitterMenu()
    {
        quitterBDUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstCQButton);

    }

    public void ConfirmationQuitterNONMenu()
    {
        quitterBDUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitterMenuButton);

    }

    public void ConfirmationRespawnNONMenu()
    {
        respawnBDUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(respawnMenuButton);

    }
    public void retourCollectable()
    {
        
        if(!CollectableInstance)
        {
            PauseMenuUI.SetActive(true);
        }
        else
        {
            CollectableInstance = false;
            collectableUI.GetComponent<CollectablesUI>().CollectableInstance = false;
            Time.timeScale = 1; 
        }
        collectableUI.SetActive(false);
    }
   /* private void RespawnPositionItem()
    {

        transform.position = itemPosition;
        gameObject.layer = itemLayer;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dead")
        {
            RespawnPositionItem();
        }
    }*/



}
