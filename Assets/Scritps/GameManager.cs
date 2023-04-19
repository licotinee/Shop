using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();
        Pref.Coins = 1000;
    }
}
