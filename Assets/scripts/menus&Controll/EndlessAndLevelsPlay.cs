using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndlessAndLevelsPlay : MonoBehaviour
{
    static public EndlessAndLevelsPlay init;
    public GameObject _Parent_of_endless;
    public GameObject begin_game_endlees;
    [SerializeField] private Button Pause_btn_endlees;
    public TextMeshProUGUI congrats_endless;
    [NonSerialized] public GameObject Parent_of_endless;
    [SerializeField] public TextMeshProUGUI coin_from_game_endlees;
    [Header("Pause :\n")]
    [SerializeField] private Button pause_btn;
    [SerializeField] private Button resume_btn1;
    [SerializeField] private Button resume_btn_2;
    [SerializeField] private Button home_btn;
    bool over_score;
    Rigidbody ball;
    // Start is called before the first frame update
    void Start()
    {
        init = this;
        over_score = false;
        coin_from_game_endlees.text = SimpelDb.read("TotalCoin");
        Pause_btn_endlees.onClick.AddListener(() => OnPauseBtn_click());
        pause_btn.onClick.AddListener(() => OnPauseBtn_click());
        resume_btn1.onClick.AddListener(() => On_resume_Click());
        resume_btn_2.onClick.AddListener(() => On_resume_Click());
        Pause_btn_endlees.onClick.AddListener(() => OnPauseBtn_click());
        home_btn.onClick.AddListener(() => On_home_Click_from_pause_panel());
    }
    void Update()
    {
        if (GamePlayControler.score > int.Parse(SimpelDb.read("score")) && over_score == false)
        {
            over_score = true;
            UiAnimation.instance.score_haver(begin_game_endlees.transform.GetChild(0).gameObject);
        }
    }

    public void EndlessPalyBegin()
    {
        over_score = false;
        GamePlayControler.init.endlees_begin = true;
        coin_from_game_endlees.text = GamePlayControler.init.Totalcoin.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        IEnumerator time_for_close()
        {
            yield return new WaitForSeconds(0.2f);
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.Totalcoin.SetActive(false);
            GameOver.init.Fiaild_label.SetActive(true);
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
    }

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
            GamePlayControler.init.All_panel_desactive();
            if (GamePlayControler.init.endlees_begin == false)
            {
                GamePlayControler.init.begin_game_panel.SetActive(true);
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

    public void OnPauseBtn_click()//with endlees and normal
    {
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        ball.isKinematic = true;
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.1f);
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.pause_panel.SetActive(true);
            UiAnimation.PausePaneleEAffects(resume_btn_2.gameObject, home_btn.gameObject, resume_btn1.gameObject);
            Time.timeScale = 0;
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
            UiAnimation.start_home(MainMenu.init.play_from_home.gameObject, MainMenu.init.setting_btn_from_home.gameObject, MainMenu.init.Shop_btn_from_home.gameObject
                                         , MainMenu.init.Balls_btn_from_home.gameObject, MainMenu.init.level_from_home.gameObject);
            UiAnimation.betwen_scines(false);
            foreach (Transform child in GamePlayControler.init.parent_of_map.transform)
            {
                if (child)
                    Destroy(child.gameObject);
            }
            if (GamePlayControler.init.endlees_begin == true)
            {
                if (GamePlayControler.score > int.Parse(SimpelDb.read("score")))
                    SimpelDb.update(GamePlayControler.score.ToString(), "score");
                if (Parent_of_endless)
                    Destroy(Parent_of_endless);
            }
            GamePlayControler.score = 0;
            ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
            ball.isKinematic = true;
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.Totalcoin.SetActive(true);
            GamePlayControler.init.main_panel.SetActive(true);
            GamePlayControler.init.endlees_begin = false;
        }
        StartCoroutine(betwin());

    }
    // Update is called once per frame

}
