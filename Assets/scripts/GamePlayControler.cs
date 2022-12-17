using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using LitJson;

public partial class GamePlayControler : MonoBehaviour
{
    static public GamePlayControler init;
    static public int score;
    static public int corent_scene;
    GameObject c;
    private bool gameover;
    private Criation_new_map Criation_of_map_obj;
    private bool winning_game;
    public bool endlees_begin;
    Rigidbody ball;
    //bool over_score;

    [Header("Buttons :\n")]

    [SerializeField] private Button home_from_levels;

    [SerializeField] private Button cancel_from_settings;
    [SerializeField] private Button back_from_settings;
    [SerializeField] private Button About_from_settings;

    [SerializeField] private Button cancel_about;

    [Header("menus : \n")]
    public GameObject begin_game_panel;
    public GameObject pause_panel;
    public GameObject winning_panel;
    public GameObject main_panel;
    public GameObject levels_panel;
    public GameObject settings_Panel;
    public GameObject Balls_Panel;
    public GameObject Shop_Panel;
    public GameObject About_pannel;
    public GameObject gameOver_panel;
    public GameObject begin_game_endlees;

    [Header("items : \n")]
    public Text target_score;
    [SerializeField] public GameObject parent_of_map;
    [SerializeField] private GameObject maps_parent_panel;
    [SerializeField] private GameObject level_pref;
    [SerializeField] public GameObject Totalcoin;
    [SerializeField] private Button SounOffObj;
    [SerializeField] private Button SoundOnObj;
    [SerializeField] private Button MusicOffObj;
    [SerializeField] private Button MusicOnObj;


    //public TextMeshProUGUI coin_from_game_endlees;
    public Button reset_dat;

    // Start is called before the first frame update
    void Start()
    {
        //over_score = false;
        init = this;
        //coin_from_game_endlees.text = SimpelDb.read("TotalCoin");
        
        endlees_begin = false;
        Listners();
        winning_game = false;
        Criation_of_map_obj = GameObject.Find("parent_of_map").GetComponent<Criation_new_map>();
        //loop on levels
        for (int i = 0; i < Criation_new_map.maps_count; i++)
        {
            c = Instantiate(level_pref, maps_parent_panel.transform.position, maps_parent_panel.transform.rotation, maps_parent_panel.transform);
            GameObject child = c.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            if (int.Parse(SimpelDb.read("level")) >= i + 1)
                c.transform.GetChild(0).gameObject.SetActive(true);
            else
            {
                c.transform.GetChild(1).gameObject.SetActive(true);
                c.GetComponent<Button>().interactable = false;
            }
        }
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
        //if (score > int.Parse(SimpelDb.read("score")) && over_score == false)
        //{
        //    over_score = true;
        //    UiAnimation.instance.score_haver(begin_game_endlees.transform.GetChild(0).gameObject);
        //} 
        if (begin_game_panel.active == true)
        {
            winning_game = false;
            gameover = false;
        }
        if (endlees_begin == false)
            target_score.text = score + "/" + Criation_new_map.count_of_cubes.ToString();
        else
            begin_game_endlees.transform.GetChild(0).gameObject.transform.GetComponent<Text>().text = score.ToString();
        if (score == Criation_new_map.count_of_cubes 
            && winning_game == false && endlees_begin == false && timer.timelift > 0)
        {
            target_score.text = score.ToString();
            Winning.init.WinningOnGame();
        }
        if (timer.timelift <= 0 && gameover == false && begin_game_panel.active == true)
        {
            GameOver.init.Game_over();
        }
    }

    //public void EndlessPalyBegin()
    //{
    //    over_score = false;
    //    endlees_begin = true;
    //    coin_from_game_endlees.text = Totalcoin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    //    IEnumerator time_for_close()
    //    {
    //        yield return new WaitForSeconds(0.2f);
    //        All_panel_desactive();
    //        Totalcoin.SetActive(false);
    //        GameOver.init.Fiaild_label.SetActive(true);
    //        begin_game_endlees.SetActive(true);
    //        coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
    //        GameObject ball = GameObject.FindWithTag("ball");
    //        if (ball != null)
    //            Destroy(ball);
    //        InitBall temp = GameObject.Find("init_ball").GetComponent<InitBall>();
    //        temp.init_ball();
    //        if (ball)
    //            ball.GetComponent<Rigidbody>().isKinematic = false;
    //        Parent_of_endless = Instantiate(_Parent_of_endless, new Vector3(0, 5, 0), _Parent_of_endless.transform.rotation);
    //    }
    //    StartCoroutine(time_for_close());
    //}
    private void Listners()
    {
        //pause_pannel
        //pause_btn.onClick.AddListener(() => OnPauseBtn_click());
        //resume_btn1.onClick.AddListener(() => On_resume_Click());
        //resume_btn_2.onClick.AddListener(() => On_resume_Click());
        //begingam pannel
        //home_btn.onClick.AddListener(() => On_home_Click_from_pause_panel());
        //levels
        home_from_levels.onClick.AddListener(() => OnHomeBtn_click_from_levelse());
        //settings
        cancel_from_settings.onClick.AddListener(() => On_Cancel_click_form_main());
        back_from_settings.onClick.AddListener(() => On_Cancel_click_form_main());
        SounOffObj.onClick.AddListener(() => SoundOn());
        SoundOnObj.onClick.AddListener(() => SoundOff());
        MusicOffObj.onClick.AddListener(() => MusicOn());
        MusicOnObj.onClick.AddListener(() => MusicOff());
        About_from_settings.onClick.AddListener(() => About_page());
        //About
        cancel_about.onClick.AddListener(() => On_Cancel_click_form_main());
        ///reset
        reset_dat.onClick.AddListener(() => Reset_data());
    }

    private void Reset_data()
    {
        SimpelDb.update("1", "level");
        SimpelDb.update("0", "score");
        SimpelDb.update("0", "TotalCoin");
        Loop_on_levels_card();
    }

    private void About_page()
    {
        UiAnimation.instance.pop_up(About_pannel.transform.GetChild(1).gameObject, false);
        settings_Panel.SetActive(false);
        About_pannel.SetActive(true);
    }

    




    #region Levels_panel ------------------------------------------------
    public void OnHomeBtn_click_from_levelse()
    {
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
            score = 0;
            All_panel_desactive();
            main_panel.SetActive(true);
            Totalcoin.SetActive(true);
        }
        StartCoroutine(betwin());
    }
    public void OnShopBtn_click_from_levelse()
    {
        All_panel_desactive();
        Totalcoin.SetActive(true);
        main_panel.SetActive(true);
    }

    #endregion

    #region GamePaly ------------------------------------------------

    //public void OnPauseBtn_click()//with endlees and normal
    //{
    //    ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
    //    ball.isKinematic = true;
    //    IEnumerator betwin()
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        All_panel_desactive();
    //        pause_panel.SetActive(true);
    //        UiAnimation.PausePaneleEAffects(resume_btn_2.gameObject, home_btn.gameObject, resume_btn1.gameObject);
    //        Time.timeScale = 0;
    //    }
    //    StartCoroutine(betwin());
    //}

    #endregion

    #region Pause Menue ------------------------------------------------
    //public void On_resume_Click()
    //{
    //    Time.timeScale = 1;
    //    UiAnimation.instance.closePausePaneleEAffects(resume_btn_2.gameObject, home_btn.gameObject, resume_btn1.gameObject);
    //    IEnumerator betwin()
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
    //        if (ball)
    //            ball.isKinematic = false;
    //        All_panel_desactive();
    //        if (endlees_begin == false)            {
    //            begin_game_panel.SetActive(true);
    //            coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
    //            begin_game_endlees.SetActive(true);
    //        }
               
    //    }
    //    StartCoroutine(betwin());
    //}

    //public void On_home_Click_from_pause_panel()
    //{
    //    Time.timeScale = 1;
    //    UiAnimation.betwen_scines(true);
    //    IEnumerator betwin()
    //    {
    //        yield return new WaitForSeconds(0.2f);
    //        UiAnimation.start_home(MainMenu.init.play_from_home.gameObject, MainMenu.init.setting_btn_from_home.gameObject, MainMenu.init.Shop_btn_from_home.gameObject
    //                                     , MainMenu.init.Balls_btn_from_home.gameObject, MainMenu.init.level_from_home.gameObject);
    //        UiAnimation.betwen_scines(false);
    //        foreach (Transform child in parent_of_map.transform)
    //        {
    //            if (child)
    //                Destroy(child.gameObject);
    //        }
    //        if(endlees_begin == true)
    //        {
    //            if (score > int.Parse(SimpelDb.read("score")))
    //                SimpelDb.update(score.ToString(), "score");
    //            if (Parent_of_endless)
    //                Destroy(Parent_of_endless);
    //        }
    //        score = 0;
    //        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
    //        ball.isKinematic = true;
    //        All_panel_desactive();
    //        Totalcoin.SetActive(true);
    //        main_panel.SetActive(true);
    //        endlees_begin = false;
    //    }
    //    StartCoroutine(betwin());
        
    //}
    #endregion

    #region Game Over Menu------------------------------------------------

    public void ReturnFromLevels()
    {
        foreach (Transform child in parent_of_map.transform)
        {
            if(child)
                Destroy(child.gameObject);
        }
        UiAnimation.instance.butten_haver(gameObject);
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
            All_panel_desactive();
            begin_game_panel.SetActive(true);
            EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            Criation_of_map_obj = GameObject.Find("parent_of_map").GetComponent<Criation_new_map>();
            Criation_of_map_obj.Make_map(corent_scene);
            Totalcoin.SetActive(false);
            GameObject ball = GameObject.FindWithTag("ball");
            if (ball != null)
                Destroy(ball);
            InitBall temp = GameObject.Find("init_ball").GetComponent<InitBall>();
            temp.init_ball();
        }
        StartCoroutine(betwin());

    }
    public void On_home_Click_from_Gameover_panel()///
    {
        UiAnimation.ResetAnimation();
        Time.timeScale = 1;
        winning_game = false;
        gameover = false;
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.start_home(MainMenu.init.play_from_home.gameObject, MainMenu.init.setting_btn_from_home.gameObject, MainMenu.init.Shop_btn_from_home.gameObject
                          , MainMenu.init.Balls_btn_from_home.gameObject, MainMenu.init.level_from_home.gameObject);
            UiAnimation.betwen_scines(false);
            foreach (Transform child in parent_of_map.transform)
            {
                if (child)
                    Destroy(child.gameObject);
            }
            if (endlees_begin || gameover || winning_game)
                ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
            if (ball)
                ball.isKinematic = true;
            All_panel_desactive();
            Totalcoin.SetActive(true);
            main_panel.SetActive(true);
        }
        StartCoroutine(betwin());
        endlees_begin = false;
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

    #region Seting_pannel -------------------------------------

    public void On_Cancel_click_form_main()
    {
        UiAnimation.instance.pop_up(settings_Panel.transform.GetChild(1).gameObject, true);
        UiAnimation.instance.pop_up(About_pannel.transform.GetChild(1).gameObject, true);
        IEnumerator pop_up_in()
        {
            yield return new WaitForSeconds(0.3f);
            settings_Panel.SetActive(false);
            MainMenu.init.play_from_home.gameObject.SetActive(true);
            About_pannel.SetActive(false);
        }
        StartCoroutine(pop_up_in());
    }

    public void SoundOn()
    {
       // FindObjectOfType<AudioManager>().PlaySound("click_on");
        SimpelDb.update(0.ToString(), "Sound");
        SounOffObj.gameObject.SetActive(false);
        SoundOnObj.gameObject.SetActive(true);
    }
    public void SoundOff()
    {
        SimpelDb.update(1.ToString(), "Sound");
       // FindObjectOfType<AudioManager>().PlaySound("click_off");
        SounOffObj.gameObject.SetActive(true);
        SoundOnObj.gameObject.SetActive(false);
    }
    public void MusicOn()
    {
        //FindObjectOfType<AudioManager>().PlaySound("click_on");
        SimpelDb.update(0.ToString(), "Music");
        //Debug.Log(SimpelDb.read("Music"));
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

    public void Loop_on_levels_card()
    {
        GameObject parent_of_card_level = levels_panel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        int i = 0;
        foreach (Transform child in parent_of_card_level.transform)
        {
            if (int.Parse(SimpelDb.read("level")) >= i + 1)
            {
                child.transform.GetChild(0).gameObject.SetActive(true);
                child.transform.GetChild(1).gameObject.SetActive(false);
                child.GetComponent<Button>().interactable = true;
            }
            else
            {
                child.transform.GetChild(0).gameObject.SetActive(false);
                child.transform.GetChild(1).gameObject.SetActive(true);
                child.GetComponent<Button>().interactable = false;
            }

            i++;
        }
    }

    public void All_panel_desactive()
    {
        pause_panel.SetActive(false);
        main_panel.SetActive(false);
        winning_panel.SetActive(false);
        begin_game_panel.SetActive(false);
        levels_panel.SetActive(false);
        settings_Panel.SetActive(false);
        Balls_Panel.SetActive(false);
        Shop_Panel.SetActive(false);
        About_pannel.SetActive(false);
        gameOver_panel.SetActive(false);
        begin_game_endlees.SetActive(false);
        EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
    }
}
public partial class GamePlayControler : MonoBehaviour 
{
    //[Header("Endless Part \n")]

    //public GameObject _Parent_of_endless;


    //[SerializeField] private Button Pause_btn_endlees;

    //public TextMeshProUGUI congrats_endless;


    //[NonSerialized] public GameObject Parent_of_endless;

    //private void Listners_in_endlees()
    //{
       
    //   // Pause_btn_endlees.onClick.AddListener(() => OnPauseBtn_click());
    //}
}
