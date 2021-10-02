using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections.Specialized;
using UnityEngine.UIElements;
using static UnityEngine.Random;
using Player;
public class UnstableFeatures : MonoBehaviour
{
    //public Dictionary<string, Action<int, int, float, float, string, string, GameObject>> features;
    public List< Action<int, int, float, float, string, string, GameObject>> features;
    public Camera cam;
    public GameObject error;
    public GameObject player;
    public PlayerMovement playerMov;
    

    
    
    private void Start()
    {
        /* features = new Dictionary<string, Action<int, int, float, float, string, string, GameObject>>();
        features.Add("GravitySwap", GravitySwap);
        features.Add("InvertedControls", InvertedControls);
        features.Add("SwapWaterLava", SwapWaterLava);
        features.Add("SpawnExtraEnemies", SpawnExtraEnemies);
        features.Add("ObjectTeleportation", ObjectTeleportation);
        features.Add("GunShootBackwards", GunShootBackwards);
        features.Add("ScreenFlip", ScreenFlip);
        features.Add("BlackVoids", BlackVoids);
        features.Add("GlitchEffect", GlitchEffect);
        features.Add("InaccurateParticles", InaccurateParticles);
        features.Add("FakeError", FakeError);
        features.Add("HomingBullets", HomingBullets);
        features.Add("ScaleEnemies", ScaleEnemies);
        features.Add("EnemyRandomize", EnemyRandomize);
        features.Add("PlayerScale", PlayerScale);
        features.Add("RandomizeBrightness", RandomizeBrightness);
        features.Add("Cracks", Cracks);
        features.Add("ScreenRotate", ScreenRotate);*/
        features = new List<Action<int, int, float, float, string, string, GameObject>>();
        features.Add( GravitySwap);
        features.Add(InvertedControls);
        features.Add(SwapWaterLava);
        features.Add(SpawnExtraEnemies);
        features.Add(ObjectTeleportation);
        features.Add(GunShootBackwards);
        features.Add(ScreenFlip);
        features.Add(BlackVoids);
        features.Add(GlitchEffect);
        features.Add(InaccurateParticles);
        features.Add(FakeError);
        features.Add(HomingBullets);
        features.Add(ScaleEnemies);
        features.Add(EnemyRandomize);
        features.Add(PlayerScale);
        features.Add(RandomizeBrightness);
        features.Add(Cracks);
        features.Add(ScreenRotate); 
         player = GameObject.FindGameObjectWithTag("Player");
        playerMov = player.GetComponent<PlayerMovement>();
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GravitySwap(0, 0, 0, 0, "", "", gameObject);
            UnstabilityManager.instance.AddUnstability(10);
        }
    }
    public void DoRandomFeature()
    {
        int random = Range(0, features.Count);
        features[random](2, 0, 0, 0, "", "", gameObject);
        print("yes");


    }
    

   

    public void GravitySwap(int x, int y, float _c, float _d, string _e, string _f, GameObject go)
    {
        GameObject[] rbs = GameObject.FindGameObjectsWithTag("Enemy");
         
        foreach(GameObject rb in rbs)
        {
            rb.GetComponent<Rigidbody2D>().gravityScale *= -1;
            rb.transform.localScale = new Vector2(rb.transform.localScale.x, rb.transform.localScale.y * -1);
        }
        player.GetComponent<Rigidbody2D>().gravityScale *= -1;
        player.transform.localScale = new Vector2(player.transform.localScale.x, player.transform.localScale.y * -1);
        playerMov.point *= -1;

    }

    public void InvertedControls(int x, int _a, float _c, float _d, string _e, string _f, GameObject go)
    {
        playerMov.invertedControls *= -1;
    }

    public void SwapWaterLava(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {

    }

    public void SpawnExtraEnemies(int count, int size, float _c, float _d, string _e, string _f, GameObject go)
    {

    }

    public void ObjectTeleportation(int maxSwaps, int _a, float _c, float _d, string item1, string item2, GameObject go)
    {

    }

    public void GunShootBackwards(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        playerMov.invertedGun *= -1;
    }

    public void ScreenFlip(int x, int y, float _c, float _d, string _e, string _f, GameObject go)
    {
        cam.transform.Rotate(0, 0, 180);
    }

    public void BlackVoids(int count, int _a, float _c, float _d, string _e, string _f, GameObject go)
    {
        //dont nothing
    }

    public void GlitchEffect(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {

    }

    public void InaccurateParticles(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {

    }

    public void FakeError(int count, int _b, float _c, float _d, string _e, string message, GameObject go)
    {
        StartCoroutine(SpawnErrors(count));
    }

    private IEnumerator SpawnErrors(int count) {
        for (int i = 0; i < count; i++)
        {
            Instantiate(error, new Vector3(Range(-10, 10), Range(-10, 10), 3), Quaternion.identity).GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(2f);
        }
    }

    public void HomingBullets(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        GameObject[] rbs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject rb in rbs)
        {
            if (rb.GetComponent<Enemies.ShootEnemy>() != null)
            {
                rb.GetComponent<Enemies.ShootEnemy>().home = true;
            }
            
        }
    }

    public void ScaleEnemies(int _a, int _b, float scale, float _c, string _e, string _f, GameObject go)
    {
        GameObject[] rbs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject rb in rbs)
        {
            float[] multis = { 0.5f, 2 };
            int multi = Range(0, multis.Length);
            rb.transform.localScale *= multis[multi];

        }
    }

    public void EnemyRandomize(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {

    }

    public void PlayerScale(int _a, int _b, float scale, float _c, string _e, string _f, GameObject go)
    {

    }

    public void RandomizeBrightness(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        // var tempColor = brightnessBootleg.GetComponent<Image>().tintColor;
        // tempColor.a = Range(0, 100);
        // brightnessBootleg.GetComponent<Image>().tintColor = tempColor;
    }

    public void Cracks(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {

    }

    public void ScreenRotate(int direction, int _a, float _c, float _d, string _e, string _f, GameObject go)
    {
        cam.transform.Rotate(0, 0, 90f/* * direction < 0 ? -1 : 1*/);
    }

}