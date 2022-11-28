using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop_Manager : MonoBehaviour
{
    public Button Buttons_balls;
    public Button Buttons_trails;
    public GameObject panel_balls,panel_trails;
    public GameObject TrailObj, CharCanvObj, CharactersObj, SoundObj, SoundOnObj, SounOffObj, MusicOnObj, MusicOffObj,
                      homeiconobj, charicoobj, trailicoobj, SoundicoObj;
    void Start()
    {
        //SoinAndMusicFromDb();
        Buttons_trails.onClick.AddListener(() => On_click_Up_Buttons_trails());
        Buttons_balls.onClick.AddListener(() => On_click_Up_Buttons_balls());
    }
    void SoinAndMusicFromDb()
    {
        if (int.Parse(SimpelDb.read("Sound")) == 0)
        {
            SounOffObj.SetActive(false);
            SoundOnObj.SetActive(true);
        }
        else
        {
            SounOffObj.SetActive(true);
            SoundOnObj.SetActive(false);
        }
        if (int.Parse(SimpelDb.read("Music")) == 0)
        {
            MusicOffObj.SetActive(false);
            MusicOnObj.SetActive(true);
        }
        else
        {
            MusicOffObj.SetActive(true);
            MusicOnObj.SetActive(false);
        }
    }

    public void Sound()
    {
        UiAnimeShop.butten_haver(SoundicoObj.gameObject);
        FindObjectOfType<AudioManager>().PlaySound("click");
        if (SoundObj.activeSelf)
            StartCoroutine(deley());
        else
            SoundObj.SetActive(true);
    }
    IEnumerator deley()
    {
        yield return new WaitForSeconds(0.3f);
        SoundObj.SetActive(false);
    }
    public void SoundOn()
    {
        M_Sound();
        FindObjectOfType<AudioManager>().PlaySound("click_on");
        SimpelDb.update(0.ToString(), "Sound");
        SounOffObj.SetActive(false);
        SoundOnObj.SetActive(true);

    }
    public void SoundOff()
    {
        M_Sound();
        SimpelDb.update(1.ToString(), "Sound");
        FindObjectOfType<AudioManager>().PlaySound("click_off");
        SounOffObj.SetActive(true);
        SoundOnObj.SetActive(false);
    }
    public void MusicOn()
    {
        M_Music();
        FindObjectOfType<AudioManager>().PlaySound("click_on");
        SimpelDb.update(0.ToString(), "Music");
        Debug.Log(SimpelDb.read("Music"));
        MusicOffObj.SetActive(false);
        MusicOnObj.SetActive(true);

    }
    public void MusicOff()
    {
        M_Music();
        SimpelDb.update(1.ToString(), "Music");
        FindObjectOfType<AudioManager>().PlaySound("click_off");
        MusicOffObj.SetActive(true);
        MusicOnObj.SetActive(false);
    }

    public void M_Sound()
    {
        FindObjectOfType<AudioManager>().MuteSound("cancel");
        FindObjectOfType<AudioManager>().MuteSound("loos");
        FindObjectOfType<AudioManager>().MuteSound("femal jump");
        FindObjectOfType<AudioManager>().MuteSound("man jump");
        FindObjectOfType<AudioManager>().MuteSound("run");
        FindObjectOfType<AudioManager>().MuteSound("coin");
        FindObjectOfType<AudioManager>().MuteSound("slide");
        FindObjectOfType<AudioManager>().MuteSound("click");
    }
    public void M_Music()
    {
        FindObjectOfType<AudioManager>().MuteSound("background");
    }
    private void On_click_Up_Buttons_trails()
    {
        Buttons_trails.gameObject.GetComponent<Image>().color = new Color32(255, 52, 197, 226);
        Buttons_balls.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 137);
        Buttons_trails.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        Buttons_balls.gameObject.GetComponent<Image>().color = new Color32(177, 132, 190, 255);
        panel_balls.SetActive(false);
        panel_trails.SetActive(true);
    }
    private void On_click_Up_Buttons_balls()
    {
        Buttons_balls.gameObject.GetComponent<Image>().color = new Color32(255, 52, 197, 226);
        Buttons_balls.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        Buttons_trails.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 137);
        Buttons_trails.gameObject.GetComponent<Image>().color = new Color32(177, 132, 190, 255);
        panel_balls.SetActive(true);
        panel_trails.SetActive(false);
    }
    void Update()
    {
        
    }
}
