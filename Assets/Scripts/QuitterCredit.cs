using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class QuitterCredit : MonoBehaviour
{
    public GameObject CreditUI;
    public GameObject optionUI;
    public GameObject creditButton;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Dash")|| Input.GetMouseButtonDown(0))
        {
            CreditUI.SetActive(false);
            optionUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditButton);
        }
    }
}
