using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayControler : MonoBehaviour
{
    [Header("Buttons :\n")]
    [SerializeField] private Button pause_btn;

    [SerializeField] private Button resume_btn1;
    [SerializeField] private Button resume_btn_2;
    [SerializeField] private Button home_btn;

    [SerializeField] private Button home_btn_gameover;
    [SerializeField] private Button levels_btn_gameover; 
    [SerializeField] private Button returne_btn_gameover;

    [SerializeField] private Button home_btn_winning;
    [SerializeField] private Button settings_btn_winning;
    [SerializeField] private Button next_btn_winning;

    [SerializeField] private Button play_from_home;
    [SerializeField] private Button level_from_home;
    [SerializeField] private Button setting_btn_from_home;


    [SerializeField] private Button home_from_levels;
    [SerializeField] private Button settings_from_levels;

    [SerializeField] private Button home_from_settings;
    [SerializeField] private Button levels_from_settings;


    [Header("menus : \n")]
    [SerializeField] private GameObject pause_panel;
    [SerializeField] private GameObject main_panel;
    [SerializeField] private GameObject winning_panel;
    [SerializeField] private GameObject game_over_panel;
    [SerializeField] public GameObject begin_game_panel;
    [SerializeField] private GameObject levels_panel;
    [SerializeField] private GameObject settings_Panel;

    [Header("items : \n")]
    [SerializeField] private Text target_score;
    [SerializeField] private GameObject parent_of_map;
    [SerializeField] private GameObject maps_parent_panel;
    [SerializeField] private GameObject level_pref;



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
            GameObject c = Instantiate(level_pref, level_pref.transform.position, level_pref.transform.rotation, maps_parent_panel.transform);
            GameObject child = c.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }
        Time.timeScale = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        target_score.text = score + "/" + Criation_new_map.count_of_cubes.ToString();
        if (score == Criation_new_map.count_of_cubes)
            Winning();
        if (timer.timelift <= 0 && gameover == false && begin_game_panel.active == true)
        {
           
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
        levels_btn_gameover.onClick.AddListener(() => On_Levels_Click_from_Gameover_panel());
        returne_btn_gameover.onClick.AddListener(() => Returne());
        //winning
        //home_btn_winning.onClick.AddListener(() =>());
        //settings_btn_winning.onClick.AddListener(() =>());
        //next_btn_winning.onClick.AddListener(() =>()); ;

        //home
        play_from_home.onClick.AddListener(() =>On_Play_Click());
        level_from_home.onClick.AddListener(() =>On_Levels_Click_from_main());
        setting_btn_from_home.onClick.AddListener(() => On_setting_click_form_main());
        //levels
        home_from_levels.onClick.AddListener(() =>OnHomeBtn_click_from_levelse());
        settings_from_levels.onClick.AddListener(() => On_setting_click_form_main());
        //settings
        home_from_settings.onClick.AddListener(() => On_home_Click_from_Gameover_panel());
        levels_from_settings.onClick.AddListener(() => On_Levels_Click_from_Gameover_panel());

    }

    #region Levels_panel ------------------------------------------------
    public void OnHomeBtn_click_from_levelse()
    {
        All_panel_desactive();
        main_panel.SetActive(true);
    }
    public void OnShopBtn_click_from_levelse()
    {
        All_panel_desactive();
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
        main_panel.SetActive(true);
    }
    #endregion

    #region Game Over Menu------------------------------------------------

    private void Gameover()
    {
        All_panel_desactive();
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
        begin_game_panel.SetActive(true);
        Time.timeScale = 1;

    }
    public void On_Levels_Click_from_main()
    {
        All_panel_desactive();
        levels_panel.SetActive(true);
    }

    public void On_setting_click_form_main()
    {
        All_panel_desactive();
        settings_Panel.SetActive(true);
    }
    #endregion

    #region Seting_pannel -------------------------------------



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
    }
}
