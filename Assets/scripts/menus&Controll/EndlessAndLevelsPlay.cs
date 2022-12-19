using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using LitJson;

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

    [Header("Effect of box\n")]
    [SerializeField] private GameObject Particle;
    [SerializeField] private GameObject crista_obj;
    [SerializeField] private GameObject initial_ball_particle;
    ParticleSystemRenderer p;
    public GameObject shield_effect;
    TMP_Text _cristal;
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
        p = Particle.GetComponent<ParticleSystemRenderer>();
        _cristal = Resources.FindObjectsOfTypeAll<GameObject>()
                                    .FirstOrDefault(g => g.CompareTag("coin_from_endless"))
                                    .gameObject.GetComponent<TextMeshProUGUI>();
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
        FindObjectOfType<AudioManager>().PlaySound("active_shield");
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
        FindObjectOfType<AudioManager>().PauseSound("active_shield");
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
        GameObject[] ball01 = GameObject.FindGameObjectsWithTag("ball");
        for (int i = 0; i < ball01.Length; i++)
        {
            if (ball01[i])
                Destroy(ball01[i]);
        }
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.start_home(MainMenu.init.play_from_home.gameObject, MainMenu.init.setting_btn_from_home.gameObject
                       , MainMenu.init.Shop_btn_from_home.gameObject, MainMenu.init.Balls_btn_from_home.gameObject
                       , MainMenu.init.level_from_home.gameObject, GamePlayControler.init.Totalcoin);
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
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.Totalcoin.SetActive(true);
            GamePlayControler.init.main_panel.SetActive(true);
            GamePlayControler.init.endlees_begin = false;
        }
        StartCoroutine(betwin());

    }
    void initial_cristal(Vector3 obj)
    {
        Vector3 _random = new Vector3(UnityEngine.Random.Range(1f, -1f)
                                               , UnityEngine.Random.Range(1f, -1f)
                                               , 0);
        float random_time = UnityEngine.Random.Range(0.8f, 1.2f);
        {
            GameObject Particle_cristal = Instantiate(crista_obj, obj + _random, crista_obj.transform.rotation);
            FindObjectOfType<AudioManager>().PlaySound("cristal_win");
            Particle_cristal.transform.LeanMove(_cristal.gameObject.transform.parent.transform.position, random_time)
                        .setEaseInOutCubic()
                        .setOnComplete(() =>
                        {  //executes whenever coin reach target position
                            int Shopcoin = int.Parse(SimpelDb.read("TotalCoin"));
                            Shopcoin++;
                            SimpelDb.update(Shopcoin.ToString(), "TotalCoin");
                            _cristal.text = Shopcoin.ToString();
                            if (Particle_cristal)
                                Destroy(Particle_cristal);
                        });
            Particle_cristal.LeanScale(new Vector3(0.06f, 0.06f, 0.06f), random_time).setEaseInOutCubic();
        }
    }

    public void destroy_box(GameObject collision,GameObject ball_obj)
    {
        Vector3 DestroyPosetion = collision.transform.position;
        GamePlayControler.score++;
        p.material = collision.GetComponent<MeshRenderer>().material;
        Instantiate(Particle, DestroyPosetion, Particle.transform.rotation);
        int cristal_win = UnityEngine.Random.Range(1, 4);
        int _random = UnityEngine.Random.Range(0, 30);
        Destroy(collision);
        if (_random == 1)//chance to get cristal
        {
            for (int i = 0; i < cristal_win; i++)
            {
                initial_cristal(DestroyPosetion);
            }
        }
        else if (_random == 2)//chance to get more balls
        {
            Instantiate(initial_ball_particle, DestroyPosetion
                                , initial_ball_particle.transform.rotation);
            FindObjectOfType<AudioManager>().PlaySound("ather_ball");
            IEnumerator wait_ball()
            {
                yield return new WaitForSeconds(0.3f);
                string s = SimpelDb.read("SaveDataShop");
                JsonData j = JsonMapper.ToObject(s);
                int index = (int)j["SelectedIndex"];
                GameObject G = Instantiate(InitBall.instiate.ball[index], DestroyPosetion
                        , InitBall.instiate.ball[index].transform.rotation);
                s = SimpelDb.read("SaveTrailDataShop");
                j = JsonMapper.ToObject(s);
                index = (int)j["SelectedIndex"];
                if (index != 0)
                    G.transform.GetChild(index - 1).gameObject.SetActive(true);
            }
            StartCoroutine(wait_ball());

        }
        else if (_random == 3 && GamePlayControler.init.shield == false)//shield effect
        {
            FindObjectOfType<AudioManager>().MuteShield("active_shield", false);
            FindObjectOfType<AudioManager>().PlaySound("active_shield");
            GamePlayControler.init.shield = true;
            Instantiate(shield_effect, ball_obj.transform.position, shield_effect.transform.rotation, ball_obj.transform);
            IEnumerator wait_child()
            {
                yield return new WaitForSeconds(10);
                GamePlayControler.init.shield = false;
            }
            StartCoroutine(wait_child());
        }
        else
            FindObjectOfType<AudioManager>().PlaySound("pop_box");
    }

}
