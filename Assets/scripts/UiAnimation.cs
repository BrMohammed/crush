using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimation : MonoBehaviour
{

    static public UiAnimation instance;
    private GameObject Container_of_red;
    [SerializeField] private GameObject particle_for_congrats_endless;
    void Awake()
    { //called when an instance awakes in the game
        instance = this; //set our static reference to our newly initialized instance
    }
    static public void start_home(GameObject startobj, GameObject paramobj, GameObject shapobj,
                                GameObject Balls, GameObject levels)
    {
        make_it_zero(startobj, paramobj, shapobj, Balls, levels);
        LeanTween.scale(startobj, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(levels, new Vector3(0.48f, 0.48f, 0.6f), 0.8f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(Balls, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(shapobj, new Vector3(1.6f, 1.6f, 0.6f), 0.8f).setDelay(0.4f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(paramobj, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        IEnumerator wait()
        {
            yield return new WaitForSeconds(0.3f);
            LeanTween.scale(startobj, new Vector3(1.08f, 1.08f, 0), 0.9f)
             .setEaseLinear()
             .setLoopPingPong()
             .setDelay(0.2f);
        }
        instance.StartCoroutine(wait());
    }

    public void close_home(GameObject startobj, GameObject paramobj, GameObject shapobj,
                                GameObject Balls, GameObject levels)
    {
        ResetAnimation();
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
        make_it_zero(resumeicoobj, homeicoobj, returnicoob);
        LeanTween.scale(resumeicoobj, new Vector3(1, 1, 1), 0.4f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(homeicoobj, new Vector3(1, 1, 1), 0.4f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(returnicoob, new Vector3(1, 1, 1), 0.4f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }

    public void closePausePaneleEAffects(GameObject resumeicoobj, GameObject homeicoobj, GameObject returnicoob)
    {
        LeanTween.scale(resumeicoobj, new Vector3(0, 0, 0), 0.4f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(homeicoobj, new Vector3(0, 0, 0), 0.4f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        LeanTween.scale(returnicoob, new Vector3(0, 0, 0), 0.4f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
        float[] i = { 1f, 1, 1 };
        IEnumerator origin()
        {
            yield return new WaitForSeconds(1);
            make_it_origine(i, resumeicoobj, homeicoobj, returnicoob);
        }
        StartCoroutine(origin());
    }

    public void butten_haver(GameObject b)
    {
        LeanTween.scale(b, new Vector3(b.transform.localScale.x / 1.5f, b.transform.localScale.y / 1.5f, b.transform.localScale.z / 1.5f), 0.05f).setIgnoreTimeScale(true);
        LeanTween.scale(b, new Vector3((b.transform.localScale.x / 1.5f) * 1.5f, (b.transform.localScale.y / 1.5f) * 1.5f, (b.transform.localScale.z / 1.5f) * 1.5f), 0.05f).setDelay(0.05f).setIgnoreTimeScale(true);
    }


     public void gameovereffect(GameObject Retry, GameObject faild)
    {
        GameObject Label = faild.transform.GetChild(2).gameObject;
        //label move to right
        Vector2 label_posetion = Label.transform.localPosition;
        GameObject Contaner = faild.transform.GetChild(3).gameObject;
        Retry.transform.localScale = new Vector3(0f, 0f, 0f);
        Label.transform.localPosition = new Vector2(0, 0);
        faild.transform.localPosition = new Vector2(Screen.width, faild.transform.localPosition.y);
        LeanTween.moveLocal(faild, new Vector2(0, faild.transform.localPosition.y), 0.2f)
            .setEaseLinear()
            .setDelay(0.1f);
        //label move tp right
        LeanTween.moveLocal(Label, new Vector2(-Screen.width, Label.transform.localPosition.y), 0.3f)
            .setEaseLinear()
            .setDelay(1.5f);
        //contaner move from left to right
        Contaner.transform.localPosition = new Vector2(Screen.width, Contaner.transform.localPosition.y);
        LeanTween.moveLocal(Contaner, new Vector2(0, Contaner.transform.localPosition.y), 0.3f)
            .setEaseLinear()
            .setDelay(1.8f);

        LeanTween.scale(Retry, new Vector3(1f, 1f, 1f), 0.8f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);

        IEnumerator wait()//retry pingpong scale
        {
            yield return new WaitForSeconds(0.8f);
            LeanTween.scale(Retry, new Vector3(1.08f, 1.08f, 0), 0.9f)
             .setEaseLinear()
             .setLoopPingPong()
             .setDelay(0.2f);
        }
        instance.StartCoroutine(wait());

        /*wait for end of timing to remove add watch*/
        GameObject red = faild.transform.GetChild(0).gameObject;
        float value_locall = 1;
        LeanTween.value(red, 1f, 0f, 5)
            .setOnUpdate((value) =>
            {
                red.transform.localScale = new Vector3(
                        value, red.transform.localScale.y
                        , red.transform.localScale.z);
                value_locall = value;
            }).setDelay(1.5f);
        ///wait end_timing to remove contenue
        IEnumerator return_to_right()
        {
            yield return new WaitWhile(() => value_locall > 0);
            LeanTween.moveLocal(Contaner, new Vector2(Screen.width, Contaner.transform.localPosition.y), 0.3f)
                     .setEaseLinear();

            LeanTween.moveLocal(Label, new Vector2(0, Label.transform.localPosition.y), 0.3f)
                     .setEaseLinear()
                     .setDelay(0.3f);
        }
        instance.StartCoroutine(return_to_right());

        Container_of_red = red;
    }

    public void congrats_endless(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.localPosition = new Vector3(0, -600, obj.transform.localPosition.z);
        GameObject particle = null;
        LeanTween.scale(obj, new Vector3(2.5f, 2.5f, 2.5f), 1f)
            .setEaseOutElastic();
        IEnumerator wait()
        {
            yield return new WaitForSeconds(0.4f);
            particle = Instantiate(particle_for_congrats_endless, obj.transform.position
                                                , particle_for_congrats_endless.transform.rotation
                                                , obj.transform.parent.gameObject.transform);
        }
        StartCoroutine(wait());
        LeanTween.moveLocal(obj, new Vector2(0, -800), 0.8f)
                     .setEaseOutElastic();

        LeanTween.scale(obj, new Vector3(1f, 1f, 1f), 0.4f)
           .setDelay(1f);
        LeanTween.moveLocal(obj, new Vector2(0, -600), 0.4f)
                     .setDelay(1f).setOnComplete(() =>
                     {
                        Destroy(particle);
                     }); 

    }

    static public void ResetAnimation()
    {
        LeanTween.reset();
    }

    public void return_red_to_default()
    {
        Container_of_red.transform.localScale = new Vector3(1, 1 , 1);

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
        if (revers == false)
        {
            obj.transform.localScale = new Vector3(0f, 0f, 0f);
            LeanTween.scale(obj, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
        }
        else
        {
            LeanTween.scale(obj, new Vector3(), 0.6f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
            float[] i = { 1f};
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
