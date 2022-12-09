using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Creation_of_map_from_butten : MonoBehaviour
{


    private Criation_new_map Criation_of_map_obj;
    private GamePlayControler Game_Play;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => On_butten_level_click());
    }

    public void On_butten_level_click()
    {
        Criation_of_map_obj = GameObject.Find("parent_of_map").GetComponent<Criation_new_map>();
        Game_Play = GameObject.Find("GamePlayControler").GetComponent<GamePlayControler>();
        Criation_of_map_obj.Make_map(int.Parse(transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text) - 1);
        GamePlayControler.corent_scene = int.Parse(transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text) - 1;
        Game_Play.All_panel_desactive();
        Game_Play.Totalcoin.SetActive(false);
        Game_Play.begin_game_panel.SetActive(true);
        GameObject ball = GameObject.FindWithTag("ball");
        if (ball != null)
            Destroy(ball);
        InitBall temp = GameObject.Find("init_ball").GetComponent<InitBall>();
        temp.init_ball();
    }
   

}
