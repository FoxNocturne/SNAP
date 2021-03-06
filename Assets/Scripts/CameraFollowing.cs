﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraFollowing : MonoBehaviour
{
    public Transform player;
    [Range(1, 3)]
    public int Tableau;
    public float shakeSpeed, shakeMagnitude;
    public Image fading;
    public float fadingSpeed;
    public Light2D globalLightPostApo;
    public Light2D lightBouclier;

    [Header("UI")]
    public GameObject UI;
    public Sprite dictPortail;
    public Sprite postApoPortail;

    private bool following = true;

    void Start()
    {
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
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
            UI.SetActive(true);

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
        if (player.position.x < 19)
        {
            transform.position = new Vector3(player.position.x, -27.98654f, -10);
            foreach (Transform child in transform)
            {
                child.GetComponent<Camera>().orthographicSize += (5 - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
            }
        }
        else if (player.position.x < 42)
        {
            player.GetComponent<PlacementPortail>().tutoriel = false;
            UI.transform.GetChild(0).GetComponent<Image>().sprite = dictPortail;
            UI.transform.GetChild(1).GetComponent<Image>().sprite = postApoPortail;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(30.39f, -33.56f, -10), 15 * Time.deltaTime);
            foreach (Transform child in transform)
            {
                child.GetComponent<Camera>().orthographicSize += (10.13f - child.GetComponent<Camera>().orthographicSize) * Time.deltaTime;
            }
        }
        else
        {
            if (player.position.x > 86)
            {
                Tableau = 3;
                return;
            }

            transform.position = new Vector3(player.position.x, Mathf.Clamp(player.position.y + 2f, -40f, float.MaxValue), -10);
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
        player.GetComponent<PlacementPortail>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        bouclier.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DeplacementCamera(new Vector3(player.transform.position.x, player.transform.position.y - 1, -10)));
        StartCoroutine(SizeCamera(3.5f));
        StartCoroutine(TomberBouclier(bouclier, player));
        globalLightPostApo.intensity = 0.55f; // Changement de la luminosité pour le sous-terrain
    }

    IEnumerator TomberBouclier(Transform bouclier, GameObject player)
    {
        lightBouclier.enabled = false;
        Vector2 targetPosP = new Vector2(player.transform.position.x, 0);
        Vector2 targetPosB = new Vector2(bouclier.transform.position.x, 0);
        float timeStart = Time.time;
        while (Time.time < timeStart + 3f)
        {
            bouclier.transform.Rotate(new Vector3(0, 0, (Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude)));
            yield return new WaitForEndOfFrame();
        }


        while (player.transform.position.y > 1)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPosP, 8 * Time.deltaTime);
            bouclier.position = Vector2.MoveTowards(bouclier.position, targetPosB, 8 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        while (fading.color.a < 1)
        {
            fading.color = new Color(0, 0, 0, fading.color.a + fadingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        //Destroy(bouclier);
        player.transform.position = new Vector2(0, -31.51f);
        Tableau = 2;
        player.GetComponent<Hero>().enabled = true;
        player.GetComponent<Snap>().enabled = true;
        player.GetComponent<PlacementPortail>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 2;
        following = true;

        while (fading.color.a > 0)
        {
            fading.color = new Color(0, 0, 0, fading.color.a - fadingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        player.GetComponent<Hero>().enabled = true;
        player.GetComponent<Snap>().enabled = true;
        player.GetComponent<PlacementPortail>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 2;

        StopAllCoroutines();
    }

    IEnumerator DeplacementCamera(Vector3 targetPos)
    {
        while (transform.position != targetPos)
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
