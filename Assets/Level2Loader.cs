using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Loader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public float timer = 3f;


    void Update()
    {
        if (Input.GetButtonDown("Sauter"))
        {
            LoadNextLevel();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                LoadNextLevel();
            }
        }



    }
    public void LoadNextLevel()
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
