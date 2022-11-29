using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using LitJson;

public class GamePlayControler : MonoBehaviour
{
    [Header("Buttons :\n")]
    [SerializeField] private Button pause_btn;

    [SerializeField] private Button resume_btn1;
    [SerializeField] private Button resume_btn_2;
    [SerializeField] private Button home_btn;

    [SerializeField] private Button home_btn_gameover;
    //[SerializeField] private Button levels_btn_gameover;
    [SerializeField] private Button returne_btn_gameover;

    [SerializeField] private Button home_btn_winning;
    //[SerializeField] private Button settings_btn_winning;
    [SerializeField] private Button next_btn_winning;

    [SerializeField] private Button play_from_home;
    [SerializeField] private Button level_from_home;
    [SerializeField] private Button setting_btn_from_home;
    [SerializeField] private Button Balls_btn_from_home;
    [SerializeField] private Button Shop_btn_from_home;


    [SerializeField] private Button home_from_levels;


    [SerializeField] private Button cancel_from_settings;
    [SerializeField] private Button back_from_settings;


    [Header("menus : \n")]
    [SerializeField] public GameObject begin_game_panel;
    [SerializeField] private GameObject pause_panel;
    [SerializeField] private GameObject game_over_panel;
    [SerializeField] private GameObject winning_panel;
    [SerializeField] private GameObject main_panel;
    [SerializeField] private GameObject levels_panel;
    [SerializeField] private GameObject settings_Panel;
    [SerializeField] private GameObject Balls_Panel;
    [SerializeField] private GameObject Shop_Panel;

    [Header("items : \n")]
    [SerializeField] private Text target_score;
    [SerializeField] private GameObject parent_of_map;
    [SerializeField] private GameObject maps_parent_panel;
    [SerializeField] private GameObject level_pref;
    [SerializeField] private GameObject Totalcoin;
    [SerializeField] private Button SounOffObj;
    [SerializeField] private Button SoundOnObj;
    [SerializeField] private Button MusicOffObj;
    [SerializeField] private Button MusicOnObj;


    GameObject c;
    static public int score;
    private bool gameover;
    static public  int corent_scene;
    private Criation_new_map Criation_of_map_obj;
    // Start is called before the first frame update
    void Start()
    {
        
        Listners();
        Criation_of_map_obj = GameObject.Find("parent_of_map").GetComponent<Criation_new_map>();
        for (int i = 0; i < Criation_new_map.maps_count; i++)
        {
            c = Instantiate(level_pref, maps_parent_panel.transform.position, maps_parent_panel.transform.rotation, maps_parent_panel.transform);
            GameObject child = c.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }
        Time.timeScale = 0;
        score = 0;
        if (int.Parse(SimpelDb.read("Music")) == 0)
            MusicOn();
        else
            MusicOff();
        if (int.Parse(SimpelDb.read("Sound")) == 0)
            SoundOn();
        else
            SoundOff();
    }

    // Update is called once per frame
    void Update()
    {
        target_score.text = score + "/" + Criation_new_map.count_of_cubes.ToString();
        if (score == Criation_new_map.count_of_cubes)
            Winning();
        if (timer.timelift <= 0 && gameover == false && begin_game_panel.active == true)
        {
            score = 0;
            Gameover();
        }
            
    }
    private void Listners()
    {
        //pause_pannel
        pause_btn.onClick.AddListener(() => OnPauseBtn_click());
        resume_btn1.onClick.AddListener(() => On_resume_Click());
        resume_btn_2.onClick.AddListener(() => On_resume_Click());
        //begingam pannel
        home_btn.onClick.AddListener(() => On_home_Click_from_pause_panel());
        //gameover
        home_btn_gameover.onClick.AddListener(() => On_home_Click_from_Gameover_panel());
       // levels_btn_gameover.onClick.AddListener(() => On_Levels_Click_from_Gameover_panel());
        returne_btn_gameover.onClick.AddListener(() => Returne());
        //winning
        home_btn_winning.onClick.AddListener(() => On_home_Click_from_Gameover_panel());/////
        next_btn_winning.onClick.AddListener(() => On_next_btn_Click_from_winning()); 

        //home
        play_from_home.onClick.AddListener(() =>On_Play_Click());
        level_from_home.onClick.AddListener(() =>On_Levels_Click_from_main());
        setting_btn_from_home.onClick.AddListener(() => On_setting_click_form_main());
        Balls_btn_from_home.onClick.AddListener(() => On_Balls_btn_Click__from_home_panel());
        Shop_btn_from_home.onClick.AddListener(() => On_Shop_btn_Click_from_home_panel());
        //levels
        home_from_levels.onClick.AddListener(() =>OnHomeBtn_click_from_levelse());
       
        //settings
        cancel_from_settings.onClick.AddListener(() => On_Cancel_click_form_main());
        back_from_settings.onClick.AddListener(() => On_Cancel_click_form_main());
        SounOffObj.onClick.AddListener(() => SoundOn());
        SoundOnObj.onClick.AddListener(() => SoundOff());
        MusicOffObj.onClick.AddListener(() => MusicOn());
        MusicOnObj.onClick.AddListener(() => MusicOff());
        //Shop

        //Balls

    }

    private void On_next_btn_Click_from_winning()
    {
        
    }

    private void On_Shop_btn_Click_from_home_panel()
    {
        All_panel_desactive();
        Shop_Panel.SetActive(true);
    }

    private void On_Balls_btn_Click__from_home_panel()
    {
        All_panel_desactive();
        Balls_Panel.SetActive(true);
        //balls
        GameObject panelof_ball_scroll = Balls_Panel.transform.GetChild(1).gameObject;
        panelof_ball_scroll.transform.GetChild(0).transform.position = new Vector3(panelof_ball_scroll.transform.position.x, 0, 0);
        //trails
        panelof_ball_scroll = Balls_Panel.transform.GetChild(3).gameObject;
        panelof_ball_scroll.transform.GetChild(0).transform.position = new Vector3(panelof_ball_scroll.transform.position.x, 0, 0);
    }

    #region Levels_panel ------------------------------------------------
    public void OnHomeBtn_click_from_levelse()
    {
        All_panel_desactive();
        main_panel.SetActive(true);
        Totalcoin.SetActive(true);
    }
    public void OnShopBtn_click_from_levelse()
    {
        All_panel_desactive();
        Totalcoin.SetActive(true);
        main_panel.SetActive(true);
    }

    #endregion

    #region GamePaly ------------------------------------------------

    public void OnPauseBtn_click()
    {
        All_panel_desactive();
        pause_panel.SetActive(true);
        Time.timeScale = 0;
    }

    #endregion

    #region Pause Menue ------------------------------------------------
    public void On_resume_Click()
    {
        Time.timeScale = 1;
        All_panel_desactive();
        begin_game_panel.SetActive(true);
    }

    public void On_home_Click_from_pause_panel()
    {
        foreach (Transform child in parent_of_map.transform)
        {
            Destroy(child.gameObject);
        }
        Time.timeScale = 0;
        All_panel_desactive();
        Totalcoin.SetActive(true);
        main_panel.SetActive(true);
    }
    #endregion

    #region Game Over Menu------------------------------------------------

    private void Gameover()
    {
        All_panel_desactive();
        Totalcoin.SetActive(false);
        game_over_panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Returne()
    {
        foreach (Transform child in parent_of_map.transform)
        {
            Destroy(child.gameObject);
        }
        Time.timeScale = 1;
        Criation_of_map_obj.Make_map(corent_scene);
        All_panel_desactive();
        begin_game_panel.SetActive(true);
        
    }
    public void On_home_Click_from_Gameover_panel()
    {
        foreach (Transform child in parent_of_map.transform)
        {
            Destroy(child.gameObject);
        }
        Time.timeScale = 0;
        All_panel_desactive();
        Totalcoin.SetActive(true);
        main_panel.SetActive(true);
    }

    public void On_Levels_Click_from_Gameover_panel()
    {
        foreach (Transform child in parent_of_map.transform)
        {
            Destroy(child.gameObject);
        }
        All_panel_desactive();
        levels_panel.SetActive(true);
    }
    #endregion

    #region Winning Menu------------------------------------------------


    private void Winning()
    {
        All_panel_desactive();
        winning_panel.SetActive(true);
        Time.timeScale = 0;
    }

    #endregion

    #region Main Menu ------------------------------------------

    public void On_Play_Click()
    {
        All_panel_desactive();
        Totalcoin.SetActive(false);
        begin_game_panel.SetActive(true);
        //InitBall init = new InitBall();
        //init.init_ball();
        GameObject ball = GameObject.FindWithTag("ball");
        if (ball != null)
            Destroy(ball);
        InitBall temp = GameObject.Find("init_ball").GetComponent<InitBall>();
        temp.init_ball();
        Time.timeScale = 1;

    }
    public void On_Levels_Click_from_main()
    {
        All_panel_desactive();
        levels_panel.SetActive(true);
    }

    public void On_setting_click_form_main()
    {
        settings_Panel.SetActive(true);
        play_from_home.gameObject.SetActive(false);
    }

    #endregion

    #region Seting_pannel -------------------------------------

    public void On_Cancel_click_form_main()
    {
        settings_Panel.SetActive(false);
        play_from_home.gameObject.SetActive(true);
    }

    public void SoundOn()
    {
        //FindObjectOfType<AudioManager>().PlaySound("click_on");
        SimpelDb.update(0.ToString(), "Sound");
        SounOffObj.gameObject.SetActive(false);
        SoundOnObj.gameObject.SetActive(true);
    }
    public void SoundOff()
    {
        SimpelDb.update(1.ToString(), "Sound");
        //FindObjectOfType<AudioManager>().PlaySound("click_off");
        SounOffObj.gameObject.SetActive(true);
        SoundOnObj.gameObject.SetActive(false);
    }
    public void MusicOn()
    {
        //FindObjectOfType<AudioManager>().PlaySound("click_on");
        SimpelDb.update(0.ToString(), "Music");
        // Debug.Log(SimpelDb.read("Music"));
        MusicOffObj.gameObject.SetActive(false);
        MusicOnObj.gameObject.SetActive(true);

    }
    public void MusicOff()
    {
        SimpelDb.update(1.ToString(), "Music");
        //FindObjectOfType<AudioManager>().PlaySound("click_off");
        MusicOffObj.gameObject.SetActive(true);
        MusicOnObj.gameObject.SetActive(false);
    }

    #endregion

    public void All_panel_desactive()
    {
        pause_panel.SetActive(false);
        main_panel.SetActive(false);
        winning_panel.SetActive(false);
        game_over_panel.SetActive(false);
        begin_game_panel.SetActive(false);
        levels_panel.SetActive(false);
        settings_Panel.SetActive(false);
        Balls_Panel.SetActive(false);
        Shop_Panel.SetActive(false);
    }
}
