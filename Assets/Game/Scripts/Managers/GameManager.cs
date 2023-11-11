using System;
using Utils.Singleton;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        //Application.targetFrameRate = 60;

        Vibration.Init();
    }

}