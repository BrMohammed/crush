using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    Image timerbar;
    public static float maxtime;
    public static float timelift;
    //private GameConterolerFromMenu gp;

    void Awake()
    {
        maxtime = Criation_new_map.count_of_cubes * 2;
        timerbar = GetComponent<Image>();
        timelift = maxtime;
        //gp = GameObject.Find("Gameplay Controller gp").GetComponent<GameConterolerFromMenu>();
    }

    
    void Update()
    {
        //
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
       
        //if (timelift <= 0 && gameover == true)
        //{
        //    Debug.Log("time out");
        //    Time.timeScale = 0;
        //    gameover = false;
        //}

    }
    
}
