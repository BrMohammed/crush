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
        UiAnimation.instance.butten_haver(gameObject);
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
            Game_Play = GameObject.Find("GamePlayControler").GetComponent<GamePlayControler>();
            Game_Play.All_panel_desactive();
            Game_Play.begin_game_panel.SetActive(true);
            Game_Play.coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            Criation_of_map_obj = GameObject.Find("parent_of_map").GetComponent<Criation_new_map>();
            Criation_of_map_obj.Make_map(int.Parse(transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text) - 1);
            GamePlayControler.corent_scene = int.Parse(transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text) - 1;
            Game_Play.Totalcoin.SetActive(false);
            GameObject ball = GameObject.FindWithTag("ball");
            if (ball != null)
                Destroy(ball);
            InitBall temp = GameObject.Find("init_ball").GetComponent<InitBall>();
            temp.init_ball();
            GamePlayControler.corent_scene = int.Parse(transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text) - 1;
        }
        StartCoroutine(betwin());
    }
}
