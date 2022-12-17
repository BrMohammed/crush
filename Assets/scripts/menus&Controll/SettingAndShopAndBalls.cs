using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingAndShopAndBalls : MonoBehaviour
{
    [SerializeField] private Button home_from_levels;
    [SerializeField] private Button home_from_Shop;
    [SerializeField] private Button home_from_Balls;
    [SerializeField] private Button cancel_from_settings;
    [SerializeField] private Button back_from_settings;
    [SerializeField] private Button About_from_settings;
    [SerializeField] private Button cancel_about;

    // Start is called before the first frame update
    void Start()
    {
        home_from_levels.onClick.AddListener(() => OnHomeBtn());
        home_from_Shop.onClick.AddListener(() => OnHomeBtn());
        home_from_Balls.onClick.AddListener(() => OnHomeBtn());
        //settings
        cancel_from_settings.onClick.AddListener(() => On_Cancel_click_form_main());
        back_from_settings.onClick.AddListener(() => On_Cancel_click_form_main());

        About_from_settings.onClick.AddListener(() => About_page());
        //About
        cancel_about.onClick.AddListener(() => On_Cancel_click_form_main());

    }
    public void OnHomeBtn()
    {
        UiAnimation.betwen_scines(true);
        IEnumerator betwin()
        {
            yield return new WaitForSeconds(0.2f);
            UiAnimation.betwen_scines(false);
            GamePlayControler.score = 0;
            GamePlayControler.init.All_panel_desactive();
            GamePlayControler.init.main_panel.SetActive(true);
            GamePlayControler.init.Totalcoin.SetActive(true);
        }
        StartCoroutine(betwin());
    }

    public void OnShopBtn_click_from_levelse()
    {
        GamePlayControler.init.All_panel_desactive();
        GamePlayControler.init.Totalcoin.SetActive(true);
        GamePlayControler.init.main_panel.SetActive(true);
    }
    public void On_Cancel_click_form_main()
    {
        UiAnimation.instance.pop_up(GamePlayControler.init.settings_Panel.transform.GetChild(1).gameObject, true);
        UiAnimation.instance.pop_up(GamePlayControler.init.About_pannel.transform.GetChild(1).gameObject, true);
        IEnumerator pop_up_in()
        {
            yield return new WaitForSeconds(0.3f);
            GamePlayControler.init.settings_Panel.SetActive(false);
            MainMenu.init.play_from_home.gameObject.SetActive(true);
            GamePlayControler.init.About_pannel.SetActive(false);
        }
        StartCoroutine(pop_up_in());
    }
    private void About_page()
    {
        UiAnimation.instance.pop_up(GamePlayControler.init.About_pannel.transform.GetChild(1).gameObject, false);
        GamePlayControler.init.settings_Panel.SetActive(false);
        GamePlayControler.init.About_pannel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
