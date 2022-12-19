using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    static public MainMenu init;
    public Button play_from_home;
    public Button level_from_home;
    public Button setting_btn_from_home;
    public Button Balls_btn_from_home;
    public Button Shop_btn_from_home;

    // Start is called before the first frame update
    void Start()
    {
        init = this;
        UiAnimation.start_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                           , Balls_btn_from_home.gameObject, level_from_home.gameObject,GamePlayControler.init.Totalcoin);

        play_from_home.onClick.AddListener(() => On_Play_Click());
        level_from_home.onClick.AddListener(() => On_Levels_Click_from_main());
        setting_btn_from_home.onClick.AddListener(() => On_setting_click_form_main());
        Balls_btn_from_home.onClick.AddListener(() => On_Balls_btn_Click__from_home_panel());
        Shop_btn_from_home.onClick.AddListener(() => On_Shop_btn_Click_from_home_panel());
    }
    public void On_Play_Click()
    {
       
        UiAnimation.instance.close_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                         , Balls_btn_from_home.gameObject, level_from_home.gameObject, GamePlayControler.init.Totalcoin);
        UiAnimation.betwen_scines(true);
        EndlessAndLevelsPlay.init.EndlessPalyBegin();
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
        }
        StartCoroutine(betwin());
    }

    public void On_setting_click_form_main()
    {
        UiAnimation.instance.pop_up(GamePlayControler.init.settings_Panel.transform.GetChild(1).gameObject, false);
        GamePlayControler.init.settings_Panel.SetActive(true);
        play_from_home.gameObject.SetActive(false);
    }

    public void On_Levels_Click_from_main()
    {
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.start_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                          , Balls_btn_from_home.gameObject, level_from_home.gameObject, GamePlayControler.init.Totalcoin);
            UiAnimation.betwen_scines(false);
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.levels_panel.SetActive(true);
        }
        StartCoroutine(betwin());

    }

    private void On_Balls_btn_Click__from_home_panel()
    {
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.Balls_Panel.SetActive(true);
            //balls
            GameObject panelof_ball_scroll = GamePlayControler.init.Balls_Panel.transform.GetChild(1).gameObject;
            panelof_ball_scroll.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
        }
        StartCoroutine(betwin());

    }
    private void On_Shop_btn_Click_from_home_panel()
    {
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.Shop_Panel.SetActive(true);
        }
        StartCoroutine(betwin());
    }
}
