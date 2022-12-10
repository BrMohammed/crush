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
        LeanTween.scale(Balls, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(shapobj, new Vector3(1.6f, 1.6f, 0.6f), 0.8f).setDelay(0.4f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(paramobj, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void close_home(GameObject startobj, GameObject paramobj, GameObject shapobj,
                                GameObject Balls, GameObject levels)
    {
        LeanTween.scale(startobj, new Vector3(), 0.6f).setDelay(0f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(levels, new Vector3(), 0.6f).setDelay(0f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Balls, new Vector3(), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(shapobj, new Vector3(), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(paramobj, new Vector3(), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);
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

    public void butten_haver(GameObject b)
    {
        LeanTween.scale(b, new Vector3(b.transform.localScale.x / 1.5f, b.transform.localScale.y / 1.5f, b.transform.localScale.z / 1.5f), 0.05f).setIgnoreTimeScale(true);
        LeanTween.scale(b, new Vector3((b.transform.localScale.x / 1.5f) * 1.5f, (b.transform.localScale.y / 1.5f) * 1.5f, (b.transform.localScale.z / 1.5f) * 1.5f), 0.05f).setDelay(0.05f).setIgnoreTimeScale(true);
    }


     public void gameovereffect(GameObject Retry, GameObject faild)
    {
        GameObject Label = faild.transform.GetChild(1).gameObject;
        //label move to right
        Vector2 label_posetion = Label.transform.localPosition;
        GameObject Contaner = faild.transform.GetChild(2).gameObject;
        Retry.transform.localScale = new Vector3(0f, 0f, 0f);
        faild.transform.localPosition = new Vector2(Screen.width, faild.transform.localPosition.y);
        LeanTween.moveLocal(faild, new Vector2(0, faild.transform.localPosition.y), 0.2f)
            .setEaseLinear()
            .setDelay(0.1f);

        LeanTween.moveLocal(Label, new Vector2(-Screen.width, Label.transform.localPosition.y), 0.3f)
            .setEaseLinear()
            .setDelay(1.5f);

        //contaner move from left to right
        Contaner.transform.localPosition = new Vector2(Screen.width, Contaner.transform.localPosition.y);
        LeanTween.moveLocal(Contaner, new Vector2(0, Contaner.transform.localPosition.y), 0.3f)
            .setEaseLinear()
            .setDelay(1.8f);

        LeanTween.scale(Retry, new Vector3(1f, 1f, 1f), 0.8f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);

        IEnumerator origin()//return Faild to olace and desactivate
        {
            yield return new WaitForSeconds(2);
            Label.transform.localPosition = label_posetion;
            Label.SetActive(false);
        }
        StartCoroutine(origin());
    }

    static public void betwen_scines(bool open)
    {
        GameObject betwin;
        betwin = GameObject.Find("betwin_sceen");
        Image r = betwin.GetComponent<Image>();
        if (open == true)
        {
            LeanTween.value(betwin, 0, 0.61f, 0.2f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            }).setEase(LeanTweenType.easeInCirc);
        }
        else
        {
            LeanTween.value(betwin, 0.61f, 0, 0.2f).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            }).setEase(LeanTweenType.easeInCirc);
        }
    }


    public void pop_up(GameObject obj,bool revers)
    {
        if(revers == false)
        {
            obj.transform.localScale = new Vector3(0f, 0f, 0f);
            LeanTween.scale(obj, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        }
        else
        {
            LeanTween.scale(obj, new Vector3(), 0.6f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
            float[] i = { 1f };
            IEnumerator origin()
            {
                yield return new WaitForSeconds(0.5f);
                make_it_origine(i, obj);
            }
            StartCoroutine(origin());
        }

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

    //static public void tutor(GameObject up, GameObject down , GameObject hande)
    //{
    //    LeanTween.moveLocal(up,new Vector2 (0,40),0.3f)
    //        .setEaseLinear()
    //        .setLoopPingPong()
    //        .setDelay(0.2f);
    //    LeanTween.moveLocal(down, new Vector2(0, -500), 0.3f)
    //        .setEaseLinear()
    //        .setLoopPingPong()
    //        .setDelay(0.2f);
    //    LeanTween.moveLocal(hande, new Vector2(0, -180), 0.5f)
    //        .setEaseLinear()
    //        .setLoopPingPong()
    //        .setDelay(0.1f);
    //}

    //static public void ADwatchEffect(GameObject adwatch)
    //{
    //    adwatch.transform.localScale = new Vector3(0.8f, 0.8f, 0f);
    //    LeanTween.scale(adwatch, new Vector3(1.1f, 1.1f, 0), 0.8f)
    //        .setEaseLinear()
    //        .setLoopPingPong()
    //        .setDelay(0.2f);
    //}
}
