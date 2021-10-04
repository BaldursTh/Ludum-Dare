using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections.Specialized;
using UnityEngine.UI;
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

    public Transform vcam;

    [Header("Enemies")]
    public GameObject enemyHandler;
    public List<Vector3> spawnLocs;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> bigEnemies = new List<GameObject>();

    [Header("Overlays")]
    public Image brightness;
    public Kino.DigitalGlitch gli;
    public GameObject crack;
    public GameObject cracks;

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
        //features.Add( GravitySwap);
        features.Add(InvertedControls);
        //features.Add(SwapWaterLava);
        features.Add(SpawnExtraEnemies);
        //features.Add(ObjectTeleportation);
        features.Add(GunShootBackwards);
        features.Add(ScreenFlip);
        features.Add(BlackVoids);
        features.Add(GlitchEffect);
        features.Add(FakeError);
        features.Add(HomingBullets);
        features.Add(ScaleEnemies);
        features.Add(EnemyRandomize);
        features.Add(PlayerScale);
        features.Add(RandomizeBrightness);
        features.Add(Cracks);
        features.Add(ScreenRotate);
        spawnLocs = new List<Vector3>();

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy Spawn"))
        {
            spawnLocs.Add(i.transform.position);
        }

        Reload();
    }

    public void Reload()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMov = player.GetComponent<PlayerMovement>();
        cam = Camera.main;
        vcam = GameObject.FindGameObjectWithTag("vcam").transform;
        enemyHandler = GameObject.FindGameObjectWithTag("Enemy Container");

        brightness = GameObject.FindGameObjectWithTag("Brightness Overlays").GetComponent<Image>();
        gli = cam.GetComponent<Kino.DigitalGlitch>();
        crack = GameObject.FindGameObjectWithTag("Crack");
    }

    public void DoRandomFeature()
    {
        int random = Range(0, features.Count);
        features[random](2, 0, 0.5f, 0, "", "", gameObject);
        print("yes");


    }
    

   

    public void GravitySwap(int x, int y, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Swap Gravity");
        foreach(Transform rb in enemyHandler.transform)
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
        print("Invert Controls");
        playerMov.invertedControls *= -1;
    }

   

    public void SpawnExtraEnemies(int count, int _b, float bigThreshold, float _d, string _e, string _f, GameObject go)
    {
        print("Enemy Spawn");
        count = 15;
        bigThreshold = 0.7f;
        for (int i = 0; i < count; i++)
        {
            GameObject toSpawn;
            if (Range(0f, 1f) >= bigThreshold)
                toSpawn = bigEnemies[Range(0, bigEnemies.Count)];
            else
                toSpawn = enemies[Range(0, enemies.Count)];
            Instantiate(toSpawn,
               spawnLocs[Range(0, spawnLocs.Count)],
               Quaternion.identity,
               enemyHandler.transform);
        }
    }

    

    public void GunShootBackwards(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Gun invert");
        playerMov.invertedGun *= -1;
    }

    public void ScreenFlip(int x, int y, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Screen Flip");
        cam.projectionMatrix *= Matrix4x4.Scale(new Vector3(1, -1, 1));
    }

    public void BlackVoids(int count, int _a, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Void");
        Transform voids = GameObject.FindGameObjectWithTag("Void").transform;
        foreach(Transform voi in voids)
        {
            voi.gameObject.SetActive(true);
        }
    }

    public void GlitchEffect(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Glitch");
        gli.intensity = Mathf.Clamp(gli.intensity + 0.05f, 0, 1);
    }

    public void FakeError(int count, int _b, float _c, float _d, string _e, string message, GameObject go)
    {
        print("Error");
        StartCoroutine(SpawnErrors(count));
    }

    private IEnumerator SpawnErrors(int count) {
        for (int i = 0; i < count; i++)
        {
            Instantiate(error, new Vector3(cam.transform.position.x + Range(-10, 10), cam.transform.position.y + Range(-10, 10), 3), Quaternion.identity).GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(2f);
        }
    }

    public void HomingBullets(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Homing");
        foreach (Transform i in enemyHandler.transform)
        {
            if (i.GetComponent<Enemies.ShootEnemy>() != null)
            {
                i.GetComponent<Enemies.ShootEnemy>().home = true;
            }
        }
    }

    public void ScaleEnemies(int _a, int _b, float scale, float _c, string _e, string _f, GameObject go)
    {
        print("Enemy Scale");
        scale = 0.5f;
        foreach (Transform i in enemyHandler.transform)
        {
            i.localScale += new Vector3(Range(-scale, scale) * Mathf.Sign(i.localScale.x), Range(-scale, scale), 0);
            i.localScale = new Vector3(Mathf.Sign(i.localScale.x) * Mathf.Clamp(Mathf.Abs(i.localScale.x), 0.2f, 5f), Mathf.Clamp(i.localScale.y, 0.2f, 5f));
        }
    }

    public void EnemyRandomize(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Enemy Swap");
        List<Vector3> savedPos = new List<Vector3>();
        foreach (Transform i in enemyHandler.transform)
        {
            savedPos.Add(i.position);
        }
        foreach (Transform i in enemyHandler.transform)
        {
            int ind = Range(0, savedPos.Count);
            i.position = savedPos[ind];
            savedPos.RemoveAt(ind);
        }
    }

    public void PlayerScale(int _a, int _b, float scale, float _c, string _e, string _f, GameObject go)
    {
        print("Player Scale");
        scale = 0.5f;
        player.transform.localScale += new Vector3(Range(-0.5f, 0.25f) * Mathf.Sign(player.transform.localScale.x), Range(-0.5f, 0.25f), 1);
        player.transform.localScale = new Vector3(Mathf.Clamp(Mathf.Abs(player.transform.localScale.x), 0.2f, 1.5f), Mathf.Clamp(player.transform.localScale.y, 0.2f, 1.5f), 1);
    }

    public void RandomizeBrightness(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Brightness");

        float bright = Range(0f, 1f);
        bright -= 0.5f;
        if(bright >= 0)
        {
            brightness.color = Color.white;
        }
        else
        {
            brightness.color = Color.black;
        }
        brightness.color = new Color(brightness.color.r, brightness.color.g, brightness.color.b, Mathf.Abs(bright) * 1.5f);
    }

    public void Cracks(int _a, int _b, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Cracks");
        Instantiate(cracks, new Vector3(Range(-8.5f, 8.5f), Range(-2, 6.5f)), Quaternion.identity, crack.transform);
    }

    public void ScreenRotate(int direction, int _a, float _c, float _d, string _e, string _f, GameObject go)
    {
        print("Rotate");
        vcam.transform.Rotate(0, 0, 90f/* * direction < 0 ? -1 : 1*/);
    }

}