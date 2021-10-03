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
#warning change this to private later
    public UnstableFeatures features;
    public float previousFeatureTime;
    public Slider slider;
    public GameObject hourglass;
    public Animator anim;

    
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

   
    void Update()
    {
        stabilityTimer = Time.time - previousFeatureTime;
        slider.value = stabilityTimer / featureInterval;
        if (stabilityTimer >= featureInterval)
        {
            previousFeatureTime = Time.time;
            features.DoRandomFeature();

        }

    }
    public void AddUnstability(float addition)
    {
        currentUnstability += addition;
        featureInterval -= addition;
        float stabilityPercentage = currentUnstability / maxUnstability;
        CheckHourGlass(stabilityPercentage);
        if (stabilityPercentage >= 1)
        {
            anim.SetInteger("Hourglass", 5);
            print("you lost");
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
