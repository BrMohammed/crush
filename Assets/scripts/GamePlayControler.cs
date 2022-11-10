using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayControler : MonoBehaviour
{
    [Header("Buttons :\n")]
    [SerializeField]
    private Button pause_btn;


    [Header("menus : \n")]
    [SerializeField]
    private GameObject pause_menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPauseBtn_click()
    {
        Debug.Log("hi");
        pause_menu.SetActive(true);
        Time.timeScale = 0;
    }


}
