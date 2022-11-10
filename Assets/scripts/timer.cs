using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    Image timerbar;
    public float maxtime = 5f;
    public static float timelift;
    bool gameover;
    //private GameConterolerFromMenu gp;

    void Start()
    {
        timerbar = GetComponent<Image>();
        timelift = maxtime;
        //gp = GameObject.Find("Gameplay Controller gp").GetComponent<GameConterolerFromMenu>();

        gameover = true;
    }

    
    void Update()
    {
        
        if (timelift > 0)
        {
            timelift -= Time.deltaTime;
            timerbar.fillAmount = timelift / maxtime;
        }
        else
        {
            
            //Debug.Log("time out");
            //Time.timeScale = 0;
            //GameplayController.instance.gameover();
            //gp.gameover();
        }
       
        if (timelift <= 0 && gameover == true)
        {
            Debug.Log("time out");
            Time.timeScale = 0;
            gameover = false;
        }

    }
    
}
