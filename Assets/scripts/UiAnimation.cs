using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimation : MonoBehaviour
{

    static public UiAnimation instance;
    void Awake()
    { //called when an instance awakes in the game
        instance = this; //set our static reference to our newly initialized instance
    }
    static public void start_home(GameObject startobj, GameObject paramobj, GameObject shapobj,
                                GameObject Balls, GameObject levels)
    {
        make_it_zero(startobj, paramobj, shapobj, Balls, levels);
        LeanTween.scale(startobj, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(levels, new Vector3(0.48f, 0.48f, 0.6f), 0.8f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Balls, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.4f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(shapobj, new Vector3(1.6f, 1.6f, 0.6f), 0.8f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(paramobj, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.6f).setEase(LeanTweenType.easeOutElastic);
    }

    public void close_home(GameObject startobj, GameObject paramobj, GameObject shapobj,
                                GameObject Balls, GameObject levels)
    {
        LeanTween.scale(startobj, new Vector3(), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(levels, new Vector3(), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Balls, new Vector3(), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(shapobj, new Vector3(), 0.6f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(paramobj, new Vector3(), 0.6f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);
        float[] i = { 1, 0.48f, 1, 1.6f, 1 };
        IEnumerator origin()
        {
            yield return new WaitForSeconds(1);
            make_it_origine(i, startobj, levels, Balls, shapobj, paramobj);
        }
        StartCoroutine(origin());
    }

    static public void PausePaneleEAffects(GameObject resumeicoobj, GameObject homeicoobj, GameObject returnicoob)
    {
        resumeicoobj.transform.localScale = new Vector3(0f, 0f, 0f);
        homeicoobj.transform.localScale = new Vector3(0f, 0f, 0f);
        returnicoob.transform.localScale = new Vector3(0f, 0f, 0f);

        LeanTween.scale(resumeicoobj, new Vector3(0.5349266f, 0.5349266f, 0.5349266f), 1f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(homeicoobj, new Vector3(0.6832607f, 3.211845f, 0.6832607f), 1f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(returnicoob, new Vector3(0.557861f, 2.957656f, 0.557861f), 1f).setDelay(0.4f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }

    static public void closePausePaneleEAffects(GameObject resumeicoobj, GameObject homeicoobj, GameObject returnicoob)
    {
        LeanTween.scale(resumeicoobj, new Vector3(0, 0, 0), 0.5f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(homeicoobj, new Vector3(0, 0, 0), 0.5f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(returnicoob, new Vector3(0, 0, 0), 0.5f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }

    static public void butten_haver(GameObject buttenobj)
    {
        LeanTween.scale(buttenobj, new Vector3(buttenobj.transform.localScale.x / 1.5f, buttenobj.transform.localScale.y / 1.5f, buttenobj.transform.localScale.z / 1.5f), 0.05f).setIgnoreTimeScale(true);
        LeanTween.scale(buttenobj, new Vector3((buttenobj.transform.localScale.x / 1.5f) * 1.5f, (buttenobj.transform.localScale.y / 1.5f) * 1.5f, (buttenobj.transform.localScale.z / 1.5f) * 1.5f), 0.05f).setDelay(0.05f).setIgnoreTimeScale(true);
    }
    static public void ADwatchEffect(GameObject adwatch)
    {
        adwatch.transform.localScale = new Vector3(0.8f, 0.8f, 0f);
        LeanTween.scale(adwatch, new Vector3(1.1f, 1.1f, 0), 0.8f)
            .setEaseLinear()
            .setLoopPingPong()
            .setDelay(0.2f);
    }
    static public void gameovereffect(GameObject Gogameovericoobj, GameObject Gohighscoreicoobj, GameObject Goscoreicoobj, GameObject Gohomeicoobj, GameObject Goretrygameicoobj, GameObject Adwatch)
    {
        Gogameovericoobj.transform.localScale = new Vector3(0f, 0f, 0f);
        Gohighscoreicoobj.transform.localScale = new Vector3(0f, 0f, 0f);
        Goscoreicoobj.transform.localScale = new Vector3(0f, 0f, 0f);
        Gohomeicoobj.transform.localScale = new Vector3(0f, 0f, 0f);
        Goretrygameicoobj.transform.localScale = new Vector3(0f, 0f, 0f);
        Adwatch.transform.localScale = new Vector3(0f, 0f, 0f); 

        LeanTween.scale(Adwatch, new Vector3(1f, 1f, 0f), 1f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Gogameovericoobj, new Vector3(1.4579f, 1.4579f, 1.4579f), 1f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Gohighscoreicoobj, new Vector3(0.99371476f, 0.9371476f, 0.9371476f), 1f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Goscoreicoobj, new Vector3(0.9371476f, 0.9371476f, 0.9371476f), 1f).setDelay(0.4f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Gohomeicoobj, new Vector3(0.7405146f, 3.586867f, 0.7405146f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Goretrygameicoobj, new Vector3(0.702653f, 3.725312f, 0.702653f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
    }

    static public void close_gameovereffect(GameObject Gogameovericoobj, GameObject Gohighscoreicoobj, GameObject Goscoreicoobj, GameObject Gohomeicoobj, GameObject Goretrygameicoobj, GameObject Adwatch)
    {
        LeanTween.scale(Adwatch, new Vector3(0, 0, 0), 0.5f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Gogameovericoobj, new Vector3(0, 0, 0), 0.5f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Gohighscoreicoobj, new Vector3(0, 0, 0), 0.5f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Goscoreicoobj, new Vector3(0, 0, 0), 0.5f).setDelay(0.4f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Gohomeicoobj, new Vector3(0, 0, 0), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Goretrygameicoobj, new Vector3(0, 0, 0), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
    }

    static public void betwen_scines_open()
    {
        GameObject betwin;
        betwin = GameObject.Find("betwin_sceen");
        Image r = betwin.GetComponent<Image>();
        LeanTween.value(betwin, 0, 1, 0.2f).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        }).setEase(LeanTweenType.easeInCirc);
    }
    static public void betwen_scines_colose()
    {
        GameObject betwin;
        betwin = GameObject.Find("betwin_sceen");
        Image r = betwin.GetComponent<Image>();
        LeanTween.value(betwin, 1, 0, 0.2f).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        }).setEase(LeanTweenType.easeInCirc);
    }

    static public void tutor(GameObject up, GameObject down , GameObject hande)
    {
        LeanTween.moveLocal(up,new Vector2 (0,40),0.3f)
            .setEaseLinear()
            .setLoopPingPong()
            .setDelay(0.2f);
        LeanTween.moveLocal(down, new Vector2(0, -500), 0.3f)
            .setEaseLinear()
            .setLoopPingPong()
            .setDelay(0.2f);
        LeanTween.moveLocal(hande, new Vector2(0, -180), 0.5f)
            .setEaseLinear()
            .setLoopPingPong()
            .setDelay(0.1f);
    }


    static private void make_it_zero(params GameObject[] obj)
    {
        for(int i = 0; i < obj.Length; i++)
            obj[i].transform.localScale = new Vector3(0f, 0f, 0f);

    }
    static private void make_it_origine(float[] origin, params GameObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
            obj[i].transform.localScale = new Vector3(origin[i], origin[i], origin[i]);
    }
}
