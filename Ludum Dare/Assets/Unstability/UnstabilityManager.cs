using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnstabilityManager : MonoBehaviour
{
    public static UnstabilityManager instance = null;

    public float maxUnstability;
    public float currentUnstability;

    public float stabilityTimer;
    public float featureInterval;
    private UnstableFeatures features;
    public float previousFeatureTime;
    public Slider slider;
    public GameObject hourglass;
    public Animator anim;

    public int currentGlitches;

    void Start()
    {

        #region Singelton
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            print("wtf");
            print(instance);
            Destroy(gameObject);
        }
    #endregion

        features = GetComponent<UnstableFeatures>();
        currentUnstability = 0;
        previousFeatureTime = Time.time;
        anim = hourglass.GetComponent<Animator>();
    }

    public void Reload()
    {
        hourglass = GameObject.FindGameObjectWithTag("Hourglass");
        slider = GameObject.FindGameObjectWithTag("USlider").GetComponent<Slider>();
        features = GetComponent<UnstableFeatures>();
        anim = hourglass.GetComponent<Animator>();

        for(int i = 0; i < currentGlitches; i++)
        {
            features.DoRandomFeature();
        }

        CheckHourGlass(currentUnstability / maxUnstability);
    }
   
    void Update()
    {
        stabilityTimer = Time.time - previousFeatureTime;
        slider.value = stabilityTimer / featureInterval;
        if (stabilityTimer >= featureInterval)
        {
            previousFeatureTime = Time.time;
            currentGlitches++;
            features.DoRandomFeature();

        }

    }
    public void AddUnstability(float addition)
    {
        currentUnstability += addition;
        featureInterval -= addition;
        featureInterval = Mathf.Clamp(featureInterval, 0.5f, 300f);
        float stabilityPercentage = currentUnstability / maxUnstability;
        CheckHourGlass(stabilityPercentage);
        if (stabilityPercentage >= 1)
        {
            anim.SetInteger("Hourglass", 5);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Death();
        }
        
    }
    public void CheckHourGlass(float st)
    {
        if (st < 0.25)
        {
            anim.SetInteger("Hourglass", 1);
        }
        else if (st < 0.5)
        {
            anim.SetInteger("Hourglass", 2);
        }
        else if (st < 0.75)
        {
            anim.SetInteger("Hourglass", 3);
        }
        else  if (st < 1)
        {
            anim.SetInteger("Hourglass", 4);
        }
        
    }
}
