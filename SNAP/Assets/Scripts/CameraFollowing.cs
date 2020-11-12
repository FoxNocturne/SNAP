using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollowing : MonoBehaviour
{
    public Transform player;
    [Range(1, 3)]
    public int Tableau;
    public float shakeSpeed, shakeMagnitude;
    public Image fading;
    public float fadingSpeed;

    private bool following = true;

    // Update is called once per frame
    void Update()
    {
        if(following)
        {
            switch (Tableau)
            {
                case 1:
                    CameraFollowingTableau1();
                    break;

                case 2:
                    CameraFollowingTableau2();
                    break;

                case 3:
                    CameraFollowingTableau3();
                    break;
            }
        }
    }

    private void CameraFollowingTableau1()
    {
        if (player.position.x < 32.54)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, -10);
        }
        else if (player.position.x < 50)
        {
            player.GetComponent<Snap>().tutoriel = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(41.43f, 3.5f, -10), 10 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(65.82f, 4.5f, -10), 15 * Time.deltaTime);
            foreach (Transform child in transform)
            {
                child.GetComponent<Camera>().orthographicSize += (6 - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
            }
        }
    }

    private void CameraFollowingTableau2()
    {
        if (player.position.x < 19 )
        {
            transform.position = new Vector3(player.position.x, -27.98654f, -10);
            foreach (Transform child in transform)
            {
                child.GetComponent<Camera>().orthographicSize += (5 - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
            }
        }
        else if (player.position.x < 42)
        {
            player.GetComponent<CreationPortail>().tutoriel = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(30.39f, -33.56f, -10), 15 * Time.deltaTime);
            foreach (Transform child in transform)
            {
                child.GetComponent<Camera>().orthographicSize += (10.13f - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
            }
        }
        else
        {
            if(player.position.x > 86)
            {
                Tableau = 3;
                return;
            }

            transform.position = new Vector3(player.position.x, Mathf.Clamp(player.position.y + 2.5f, -38.61296f, float.MaxValue), -10);
            foreach (Transform child in transform)
            {
                child.GetComponent<Camera>().orthographicSize += (5 - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
            }
        }
    }

    private void CameraFollowingTableau3()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(105.61f, -22.6f, -10), 15 * Time.deltaTime);
        foreach (Transform child in transform)
        {
            child.GetComponent<Camera>().orthographicSize += (11.2f - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
        }
    }

    public void EvenementChuteBouclier(Transform bouclier)
    {
        if (!following)
            return;

        following = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Hero>().enabled = false;
        player.GetComponent<Snap>().enabled = false;
        player.GetComponent<CreationPortail>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        bouclier.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DeplacementCamera(new Vector3(player.transform.position.x, player.transform.position.y - 1, -10)));
        StartCoroutine(SizeCamera(3.5f));
        // StartCoroutine(ViewPortCamera(new Rect(0, 0.15f, 1f, 0.7f)));
        StartCoroutine(TomberBouclier(bouclier, player));
    }

    IEnumerator TomberBouclier(Transform bouclier, GameObject player)
    {
        float timeStart = Time.time;
        while (Time.time < timeStart + 3f)
        {
            bouclier.transform.Rotate(new Vector3(0, 0, (Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude)));
            yield return new WaitForEndOfFrame();
        }

        Vector2 targetPos = new Vector2(player.transform.position.x, 0);

        while(player.transform.position.y > 2)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPos, 8 * Time.deltaTime);
            bouclier.position = Vector2.MoveTowards(bouclier.position, targetPos, 8 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        while(fading.color.a < 1)
        {
            Debug.Log($"On change le fading ({fading.color.a})");
            fading.color = new Color(0, 0, 0, fading.color.a + fadingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Destroy(bouclier);
        player.transform.position = new Vector2(0, -31.51f);
        Tableau = 2;
        following = true;

        while (fading.color.a > 0)
        {
            Debug.Log($"On change le fading ({fading.color.a})");
            fading.color = new Color(0, 0, 0, fading.color.a - fadingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        player.GetComponent<Hero>().enabled = true;
        player.GetComponent<Snap>().enabled = true;
        player.GetComponent<CreationPortail>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 2;

        StopAllCoroutines();
    }

    IEnumerator DeplacementCamera(Vector3 targetPos)
    {
        while(transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 15 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator SizeCamera(float targetSize)
    {
        Camera camera = transform.GetChild(0).GetComponent<Camera>();

        while (camera.orthographicSize != targetSize)
        {
            camera.orthographicSize += (targetSize - camera.orthographicSize) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ViewPortCamera(Rect targetViewport)
    {
        Camera camera = transform.GetChild(0).GetComponent<Camera>();

        while (camera.rect != targetViewport)
        {
            camera.rect = new Rect(camera.rect.x + (targetViewport.x - camera.rect.x) * Time.deltaTime,
                                   camera.rect.y + (targetViewport.y - camera.rect.y) * Time.deltaTime,
                                   camera.rect.width + (targetViewport.width - camera.rect.width) * Time.deltaTime,
                                   camera.rect.height + (targetViewport.height - camera.rect.height) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
