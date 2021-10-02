using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections.Specialized;

public class UnstableFeatures : MonoBehaviour {
    public Dictionary<string, Action<int, int, float, float, string, string>> features;

    private void Start() {
        features = new Dictionary<string, Action<int, int, float, float, string, string>>();
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
        features.Add("ScreenRotate", ScreenRotate);
    }

    public void GravitySwap(int x, int y, float _c, float _d, string _e, string _f) {

    }

    public void InvertedControls(int x, int y, float _c, float _d,string _e, string _f) {

    }

    public void SwapWaterLava(int _a, int _b, float _c, float _d,string _e, string _f) {

    }

    public void SpawnExtraEnemies(int count, int size, float _c, float _d,string _e, string _f) {

    }

    public void ObjectTeleportation(int maxSwaps, int _a, float _c, float _d,string item1, string item2) {

    }

    public void GunShootBackwards(int _a, int _b, float _c, float _d,string _e, string _f) {

    }

    public void ScreenFlip(int x, int y, float _c, float _d, string _e, string _f) {

    }

    public void BlackVoids(int count, int _a, float _c, float _d, string _e, string _f) {

    }
    
    public void GlitchEffect(int _a, int _b, float _c, float _d, string _e, string _f) {

    }

    public void InaccurateParticles(int _a, int _b, float _c, float _d, string _e, string _f) {

    }

    public void FakeError(int _a, int _b, float _c, float _d, string _e, string message) {

    }

    public void HomingBullets(int _a, int _b, float _c, float _d, string _e, string _f) {

    }

    public void ScaleEnemies(int _a, int _b, float scale, float _c, string _e, string _f) {

    }

    public void EnemyRandomize(int _a, int _b, float _c, float _d, string _e, string _f) {

    }

    public void PlayerScale(int _a, int _b, float scale, float _c, string _e, string _f) {

    }

    public void RandomizeBrightness(int _a, int _b, float _c, float _d, string _e, string _f) {

    }

    public void Cracks(int _a, int _b, float _c, float _d, string _e, string _f) {

    }
    
    public void ScreenRotate(int direction, int _a, float _c, float _d, string _e, string _f) {
        // direction: => 0 = clockwise, < 0 = counterclockwise
    }

}