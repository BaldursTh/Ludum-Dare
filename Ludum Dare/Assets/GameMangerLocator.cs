using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangerLocator : MonoBehaviour
{
    private GameManager m;
    // Start is called before the first frame update
    void Awake()
    {
        m = GameManager.instance;
    }

    public void ChangeState(int state)
    {
        m.UpdateState(state);
    }
}
