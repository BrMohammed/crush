using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    static public GameOver init;
    public GameObject _Parent_of_endless;
    [SerializeField] private TextMeshProUGUI high_score;
    [SerializeField] private Button returne;
    public GameObject Fiaild_label;
    [SerializeField] private Button Conrianer_of_buy;
    [SerializeField] private Button Conrianer_of_watch_to_reward;
    [SerializeField] private Button home;
    Rigidbody ball;
    // Start is called before the first frame update
    void Start()
    {
        init = this;
        returne.onClick.AddListener(() => return_event());
        Conrianer_of_buy.onClick.AddListener(() => Conrianer_of_buy_event());
        Conrianer_of_watch_to_reward.onClick.AddListener(() => Conrianer_of_watch_to_reward_event());
        home.onClick.AddListener(() => home_event());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void home_event()
    {
        UiAnimation.ResetAnimation();

        if (EndlessAndLevelsPlay.init.Parent_of_endless)
        {
            GamePlayControler.init.endlees_begin = false;
            Destroy(EndlessAndLevelsPlay.init.Parent_of_endless);
            UiAnimation.betwen_scines(true);
            IEnumerator betwin()
            {
                yield return new WaitForSeconds(0.2f);
                UiAnimation.start_home(MainMenu.init.play_from_home.gameObject, MainMenu.init.setting_btn_from_home.gameObject, MainMenu.init.Shop_btn_from_home.gameObject
                          , MainMenu.init.Balls_btn_from_home.gameObject, MainMenu.init.level_from_home.gameObject);
                UiAnimation.betwen_scines(false);
                Time.timeScale = 1;
                GamePlayControler.score = 0;
                GamePlayControler.init.All_panel_desactive();
                Destroy(EndlessAndLevelsPlay.init.Parent_of_endless);
                GamePlayControler.score = 0;
                ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
                ball.isKinematic = true;
                GamePlayControler.init.All_panel_desactive();
                GamePlayControler.init.Totalcoin.SetActive(true);
                GamePlayControler.init.main_panel.SetActive(true);
                GamePlayControler.init.target_score.text = GamePlayControler.score.ToString();
                GamePlayControler.init.begin_game_endlees.SetActive(false);
                EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
                GamePlayControler.init.gameOver_panel.SetActive(false);

            }
            StartCoroutine(betwin());
        }
        else
        {
            GamePlayControler.init.On_home_Click();
        }
        UiAnimation.instance.return_red_to_default();
    }

    public void Conrianer_of_watch_to_reward_event()
    {
        //reward_video

        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        if (ball)
            ball.isKinematic = false;
        if (EndlessAndLevelsPlay.init.Parent_of_endless)
            MainMenu.init.On_Play_Click();
        else
        {
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.Totalcoin.SetActive(false);
            GamePlayControler.init.begin_game_panel.SetActive(true);
            EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
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
        if (EndlessAndLevelsPlay.init.Parent_of_endless)
            MainMenu.init.On_Play_Click();
        else
        {
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.Totalcoin.SetActive(false);
            GamePlayControler.init.begin_game_panel.SetActive(true);
            EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            timer.maxtime = 10;
            timer.timelift = timer.maxtime;

        }
        UiAnimation.instance.return_red_to_default();
    }
    private void return_event()
    {

        GamePlayControler.score = 0;
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        if (ball)
            ball.isKinematic = false;
        if (GamePlayControler.init.endlees_begin == true)
        {
            Debug.Log(GamePlayControler.init.endlees_begin);
            MainMenu.init.On_Play_Click();
        }
        else
        {
            GamePlayControler.init.ReturnFromLevels();
        }
        UiAnimation.instance.return_red_to_default();
    }

   

    public void Game_over()
    {
        UiAnimation.instance.gameovereffect(returne.gameObject,
                                    Fiaild_label.transform.parent.gameObject);
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
        ball.isKinematic = true;
        Conrianer_of_buy.gameObject.transform.parent.gameObject.SetActive(true);
        GamePlayControler.init.Totalcoin.SetActive(true);
        Fiaild_label.SetActive(true);
        GamePlayControler.init.gameOver_panel.SetActive(true);
        if (GamePlayControler.init.endlees_begin == true)
        {
            if (EndlessAndLevelsPlay.init.Parent_of_endless)
                Destroy(EndlessAndLevelsPlay.init.Parent_of_endless);
            GamePlayControler.init.begin_game_endlees.SetActive(false);
            EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
            if (GamePlayControler.score < int.Parse(SimpelDb.read("score")))
                high_score.text = GamePlayControler.score + "/" + SimpelDb.read("score");
            else
            {
                SimpelDb.update(GamePlayControler.score.ToString(), "score");
                high_score.text = GamePlayControler.score.ToString();
            }
        }
        else
        {
            GamePlayControler.init.begin_game_panel.SetActive(false);
            EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(false);
            high_score.text = GamePlayControler.init.target_score.text;
        }
        int label_of_buying = int.Parse(Conrianer_of_buy.gameObject.transform.GetChild(0).gameObject.
                            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
        if (int.Parse(SimpelDb.read("TotalCoin")) < label_of_buying)
            Conrianer_of_buy.enabled = false;
        else
            Conrianer_of_buy.enabled = true;
    }
}
