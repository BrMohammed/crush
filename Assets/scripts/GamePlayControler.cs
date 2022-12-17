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
    [Header("Buttons :\n")]
    [SerializeField] private Button pause_btn;

    [SerializeField] private Button resume_btn1;
    [SerializeField] private Button resume_btn_2;
    [SerializeField] private Button home_btn;

    [SerializeField] private Button home_btn_winning;
    [SerializeField] private Button next_btn_winning;

    [SerializeField] private Button play_from_home;
    [SerializeField] private Button level_from_home;
    [SerializeField] private Button setting_btn_from_home;
    [SerializeField] private Button Balls_btn_from_home;
    [SerializeField] private Button Shop_btn_from_home;

    [SerializeField] private Button home_from_levels;

    [SerializeField] private Button cancel_from_settings;
    [SerializeField] private Button back_from_settings;
    [SerializeField] private Button About_from_settings;

    [SerializeField] private Button cancel_about;

    [Header("menus : \n")]
    [SerializeField] public GameObject begin_game_panel;
    [SerializeField] private GameObject pause_panel;
    [SerializeField] private GameObject winning_panel;
    [SerializeField] private GameObject main_panel;
    [SerializeField] private GameObject levels_panel;
    [SerializeField] private GameObject settings_Panel;
    [SerializeField] private GameObject Balls_Panel;
    [SerializeField] private GameObject Shop_Panel;
    [SerializeField] private GameObject About_pannel;

    [Header("items : \n")]
    [SerializeField] private Text target_score;
    [SerializeField] private GameObject parent_of_map;
    [SerializeField] private GameObject maps_parent_panel;
    [SerializeField] private GameObject level_pref;
    [SerializeField] public GameObject Totalcoin;
    [SerializeField] private Button SounOffObj;
    [SerializeField] private Button SoundOnObj;
    [SerializeField] private Button MusicOffObj;
    [SerializeField] private Button MusicOnObj;
    [SerializeField] private GameObject image_of_winning;
    [SerializeField] private GameObject winning_efect;


    [SerializeField] public TextMeshProUGUI coin_from_game_endlees;
    public Button reset_dat;


    static public GamePlayControler init;

    GameObject c;
    static public int score;
    private bool gameover;
    static public  int corent_scene;
    private Criation_new_map Criation_of_map_obj;
    private bool winning_game;
    public bool endlees_begin;
    Rigidbody ball;
    bool over_score;
    // Start is called before the first frame update
    void Start()
    {
        over_score = false;
        init = this;
        coin_from_game_endlees.text = SimpelDb.read("TotalCoin");
        UiAnimation.start_home(play_from_home.gameObject, setting_btn_from_home.gameObject , Shop_btn_from_home.gameObject
                          ,Balls_btn_from_home.gameObject, level_from_home.gameObject);
        endlees_begin = false;
        Listners();
        Listners_in_endlees();
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
        if (score > int.Parse(SimpelDb.read("score")) && over_score == false)
        {
            over_score = true;
            UiAnimation.instance.score_haver(begin_game_endlees.transform.GetChild(0).gameObject);
        } 
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
            Winning();
        }
        if (timer.timelift <= 0 && gameover == false && begin_game_panel.active == true)
        {
            Game_over_endlees();
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

    private void On_next_btn_Click_from_winning()
    {
        corent_scene++;
        foreach (Transform child in parent_of_map.transform)
        {
            if (child)
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
            coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
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

    private void On_Shop_btn_Click_from_home_panel()
    {
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
            All_panel_desactive();
            Shop_Panel.SetActive(true);
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
            All_panel_desactive();
            Balls_Panel.SetActive(true);
            //balls
            GameObject panelof_ball_scroll = Balls_Panel.transform.GetChild(1).gameObject;
            panelof_ball_scroll.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
        }
        StartCoroutine(betwin());
        
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

    public void OnPauseBtn_click()//with endlees and normal
    {
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        ball.isKinematic = true;
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.1f);
            All_panel_desactive();
            pause_panel.SetActive(true);
            UiAnimation.PausePaneleEAffects(resume_btn_2.gameObject, home_btn.gameObject, resume_btn1.gameObject);
            Time.timeScale = 0;
        }
        StartCoroutine(betwin());
    }

    #endregion

    #region Pause Menue ------------------------------------------------
    public void On_resume_Click()
    {
        Time.timeScale = 1;
        UiAnimation.instance.closePausePaneleEAffects(resume_btn_2.gameObject, home_btn.gameObject, resume_btn1.gameObject);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.5f);
            ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
            if (ball)
                ball.isKinematic = false;
            All_panel_desactive();
            if (endlees_begin == false)            {
                begin_game_panel.SetActive(true);
                coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
                begin_game_endlees.SetActive(true);
            }
               
        }
        StartCoroutine(betwin());
    }

    public void On_home_Click_from_pause_panel()
    {
        Time.timeScale = 1;
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.start_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                                         , Balls_btn_from_home.gameObject, level_from_home.gameObject);
            UiAnimation.betwen_scines(false);
            foreach (Transform child in parent_of_map.transform)
            {
                if (child)
                    Destroy(child.gameObject);
            }
            if(endlees_begin == true)
            {
                if (score > int.Parse(SimpelDb.read("score")))
                    SimpelDb.update(score.ToString(), "score");
                if (Parent_of_endless)
                    Destroy(Parent_of_endless);
            }
            score = 0;
            ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
            ball.isKinematic = true;
            All_panel_desactive();
            Totalcoin.SetActive(true);
            main_panel.SetActive(true);
            endlees_begin = false;
        }
        StartCoroutine(betwin());
        
    }
    #endregion

    #region Game Over Menu------------------------------------------------

    public void Returne()
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
            coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
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
            UiAnimation.start_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                          , Balls_btn_from_home.gameObject, level_from_home.gameObject);
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

    #region Winning Menu------------------------------------------------


    private void Winning()
    {
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.1f);
            All_panel_desactive();
            winning_panel.SetActive(true);
            Totalcoin.SetActive(true);
            GameObject ball01 = GameObject.FindWithTag("ball");
            if (ball01 != null)
                Destroy(ball01);
            FindObjectOfType<AudioManager>().PlaySound("winning");
            UiAnimation.PausePaneleEAffects(resume_btn_2.gameObject, next_btn_winning.gameObject, image_of_winning );
            IEnumerator wait_effect()
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(winning_efect, winning_efect.transform.localPosition
                                               , winning_efect.transform.localRotation, winning_panel.transform);
            }
            StartCoroutine(wait_effect());
            int level = int.Parse(SimpelDb.read("level"));
            int next_level = level + 1;
            int corent_level = Criation_new_map.this_map + 1;
            int count_of_maps = Criation_new_map.maps_count + 1;
            next_btn_winning.gameObject.SetActive(true);
            if (next_level < count_of_maps && corent_level  == level) 
            {
                level++;
                SimpelDb.update(level.ToString(), "level");
                Loop_on_levels_card();
            }
            if(next_level >= count_of_maps)
                next_btn_winning.gameObject.SetActive(false);
        }
        StartCoroutine(betwin());
        score = 0;
    }

    private void Loop_on_levels_card()
    {
        GameObject parent_of_card_level =  levels_panel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
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

    #endregion

    #region Main Menu ------------------------------------------


    public void On_Levels_Click_from_main()
    {
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.start_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                          , Balls_btn_from_home.gameObject, level_from_home.gameObject);
            UiAnimation.betwen_scines(false);
            All_panel_desactive();
            levels_panel.SetActive(true);
        }
        StartCoroutine(betwin());
        
    }

    public void On_setting_click_form_main()
    {
        UiAnimation.instance.pop_up(settings_Panel.transform.GetChild(1).gameObject,false);
        settings_Panel.SetActive(true);
        play_from_home.gameObject.SetActive(false);
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
            play_from_home.gameObject.SetActive(true);
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
        Pannel_of_endless.SetActive(false);
        begin_game_endlees.SetActive(false);
        coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
    }
}
public partial class GamePlayControler : MonoBehaviour  //endlees_game
{
    [Header("Endless Part \n")]
    [SerializeField] private GameObject Pannel_of_endless;
    [SerializeField] private GameObject _Parent_of_endless;
    [SerializeField] private GameObject begin_game_endlees;


    [SerializeField] private TextMeshProUGUI high_score;

    [SerializeField] private Button returne_fron_endlees;

    [SerializeField] private GameObject Fiaild_label;
    [SerializeField] private Button Conrianer_of_buy;
    [SerializeField] private Button Conrianer_of_watch_to_reward;
    [SerializeField] private Button home_btn_endlees;
    [SerializeField] private Button Pause_btn_endlees;

    public TextMeshProUGUI congrats_endless;
    

    private GameObject Parent_of_endless;

    public void On_Play_Click()
    {
        over_score = false;
        coin_from_game_endlees.text = Totalcoin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        UiAnimation.instance.close_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                         , Balls_btn_from_home.gameObject, level_from_home.gameObject);
        UiAnimation.betwen_scines(true);
        endlees_begin = true;
        IEnumerator time_for_close()
        {
            yield return new WaitForSeconds(0.2f);
            
            All_panel_desactive();
            Totalcoin.SetActive(false);
            Fiaild_label.SetActive(true);
            begin_game_endlees.SetActive(true);
            coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            GameObject ball = GameObject.FindWithTag("ball");
            if (ball != null)
                Destroy(ball);
            InitBall temp = GameObject.Find("init_ball").GetComponent<InitBall>();
            temp.init_ball();
            if (ball)
                ball.GetComponent<Rigidbody>().isKinematic = false;
            Parent_of_endless = Instantiate(_Parent_of_endless, new Vector3(0, 5, 0), _Parent_of_endless.transform.rotation);
        }
        StartCoroutine(time_for_close());
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
        }
        StartCoroutine(betwin());
    }

    private void Listners_in_endlees()
    {
        returne_fron_endlees.onClick.AddListener(() => returne_fron_endlees_event());
        Conrianer_of_buy.onClick.AddListener(() => Conrianer_of_buy_event());
        Conrianer_of_watch_to_reward.onClick.AddListener(() => Conrianer_of_watch_to_reward_event());
        home_btn_endlees.onClick.AddListener(() => home_btn_endlees_event());
        Pause_btn_endlees.onClick.AddListener(() => OnPauseBtn_click());
    }

    private void home_btn_endlees_event()
    {
        UiAnimation.ResetAnimation();
        
        if (Parent_of_endless)
        {
            endlees_begin = false;
            Destroy(Parent_of_endless);
            UiAnimation.betwen_scines(true);
            IEnumerator betwin()
            {
                yield return new WaitForSeconds(0.2f);
                UiAnimation.start_home(play_from_home.gameObject, setting_btn_from_home.gameObject, Shop_btn_from_home.gameObject
                          , Balls_btn_from_home.gameObject, level_from_home.gameObject);
                UiAnimation.betwen_scines(false);
                Time.timeScale = 1;
                score = 0;
                All_panel_desactive();
                Destroy(Parent_of_endless);
                score = 0;
                ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
                ball.isKinematic = true;
                All_panel_desactive();
                Totalcoin.SetActive(true);
                main_panel.SetActive(true);
                target_score.text = score.ToString();
                begin_game_endlees.SetActive(false);
                coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
                Pannel_of_endless.SetActive(false);
                
            }
            StartCoroutine(betwin());
        }
        else
        {
            On_home_Click_from_Gameover_panel();
        }
        UiAnimation.instance.return_red_to_default();
    }

    public void Conrianer_of_watch_to_reward_event()
    {
        //reward_video
        
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        if (ball)
            ball.isKinematic = false;
        if (Parent_of_endless)
            On_Play_Click();
        else
        {
            All_panel_desactive();
            Totalcoin.SetActive(false);
            begin_game_panel.SetActive(true);
            coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            timer.maxtime = 10;
            timer.timelift = timer.maxtime;
        }
        UiAnimation.instance.return_red_to_default();
    }

    public void Conrianer_of_buy_event()
    {
        
        int label_of_buying = int.Parse(Conrianer_of_buy.gameObject.transform.GetChild(0).gameObject.
                               transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
        int new_total_coin = int.Parse(SimpelDb.read("TotalCoin")) - label_of_buying;
        SimpelDb.update(new_total_coin.ToString(), "TotalCoin");
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        if (ball)
            ball.isKinematic = false;
        if (Parent_of_endless)
            On_Play_Click();
        else
        {
            All_panel_desactive();
            Totalcoin.SetActive(false);
            begin_game_panel.SetActive(true);
            coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            timer.maxtime = 10;
            timer.timelift = timer.maxtime;
            
        }
        UiAnimation.instance.return_red_to_default();
    }

    private void returne_fron_endlees_event()
    {
        
        score = 0;
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        if (ball)
            ball.isKinematic = false;
        if(endlees_begin == true)
        {
            Debug.Log(endlees_begin);
            On_Play_Click();
        }
        else
        {
            Returne();
        }
        UiAnimation.instance.return_red_to_default();
    }
    public void Game_over_endlees()
    {
        UiAnimation.instance.gameovereffect(returne_fron_endlees.gameObject,
                                    Fiaild_label.transform.parent.gameObject);
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        ball.isKinematic = true;
        Conrianer_of_buy.gameObject.transform.parent.gameObject.SetActive(true);
        Totalcoin.SetActive(true);
        Fiaild_label.SetActive(true);
        Pannel_of_endless.SetActive(true);
        if (endlees_begin == true)
        {
            if(Parent_of_endless)
            Destroy(Parent_of_endless);
            begin_game_endlees.SetActive(false);
            coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
            if (score < int.Parse(SimpelDb.read("score")))
                high_score.text = score + "/" + SimpelDb.read("score");
            else
            {
                SimpelDb.update(score.ToString(), "score");
                high_score.text = score.ToString();
            }
        }
        else
        {
            begin_game_panel.SetActive(false);
            coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
            high_score.text = target_score.text;
        }
        int label_of_buying = int.Parse(Conrianer_of_buy.gameObject.transform.GetChild(0).gameObject.
                            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
        if (int.Parse(SimpelDb.read("TotalCoin")) < label_of_buying)
            Conrianer_of_buy.enabled = false;
        else
            Conrianer_of_buy.enabled = true;
    }
}//endlees


