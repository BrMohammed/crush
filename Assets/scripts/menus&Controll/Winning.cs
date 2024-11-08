using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winning : MonoBehaviour
{
    static public Winning init;
    [SerializeField] private GameObject parent_of_map;
    [SerializeField] private Button home_btn_winning;
    [SerializeField] private Button next_btn_winning;
    private Criation_new_map Criation_of_map_obj;
    [SerializeField] private GameObject winning_efect;
    [SerializeField] private GameObject image_of_winning;
    GameObject[] ball;

    // Start is called before the first frame update
    void Start()
    {
        init = this;
        home_btn_winning.onClick.AddListener(() => GamePlayControler.init.On_home_Click());
        next_btn_winning.onClick.AddListener(() => On_next_btn_Click_from_winning());
    }

    private void On_next_btn_Click_from_winning()
    {
        GamePlayControler.corent_scene++;
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
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.begin_game_panel.SetActive(true);
            EndlessAndLevelsPlay.init.coin_from_game_endlees.transform.parent.gameObject.SetActive(true);
            Criation_of_map_obj = GameObject.Find("parent_of_map").GetComponent<Criation_new_map>();
            Criation_of_map_obj.Make_map(GamePlayControler.corent_scene);
            GamePlayControler.init.Totalcoin.SetActive(false);
            GameObject ball = GameObject.FindWithTag("ball");
            if (ball != null)
                Destroy(ball);
            InitBall temp = GameObject.Find("init_ball").GetComponent<InitBall>();
            temp.init_ball();
        }
        StartCoroutine(betwin());
    }
    public  void WinningOnGame()
    {
        if (GamePlayControler.init.shield == true)
            FindObjectOfType<AudioManager>().StopeSound("active_shield");
        ball = GameObject.FindGameObjectsWithTag("ball");
        for (int i = 0; i < ball.Length; i++)
        {
            if (ball[i])
                Destroy(ball[i]);
        }
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.1f);
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.winning_panel.SetActive(true);
            GamePlayControler.init.Totalcoin.SetActive(true);
            ManageAudio.instance.M_Music();
            IEnumerator wait_sound()
            {
                yield return new WaitForSeconds(3);
                ManageAudio.instance.M_Music();
            }
            StartCoroutine(wait_sound());
            FindObjectOfType<AudioManager>().PlaySound("winning");
            UiAnimation.PausePaneleEAffects(home_btn_winning.gameObject, next_btn_winning.gameObject, image_of_winning);
            IEnumerator wait_effect()
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(winning_efect, winning_efect.transform.localPosition
                                               , winning_efect.transform.localRotation, GamePlayControler.init.winning_panel.transform);
            }
            StartCoroutine(wait_effect());
            int level = int.Parse(SimpelDb.read("level"));
            int next_level = level + 1;
            int corent_level = Criation_new_map.this_map + 1;
            int count_of_maps = Criation_new_map.maps_count + 1;
            next_btn_winning.gameObject.SetActive(true);
            if (next_level < count_of_maps && corent_level == level)
            {
                level++;
                SimpelDb.update(level.ToString(), "level");
                GamePlayControler.init.Loop_on_levels_card();
            }
            if (next_level >= count_of_maps)
                next_btn_winning.gameObject.SetActive(false);
        }
        StartCoroutine(betwin());
        GamePlayControler.score = 0;
    }
}
