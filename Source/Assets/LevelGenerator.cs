using System.Collections;
using System.Collections.Generic;
using MKK.DoodleJumpe.Player;
using MKK.DoodleJumpe.Platform;
using UnityEngine;
using System;
public class LevelGenerator : MonoBehaviour, IDisposable, IUpdateReciever
{
    [SerializeField] private PlatformBase[] _platformPrefabs;
    [SerializeField] PlayerController _playerController;
    private Vector2 _screenBounds;

    private void Awake()
    {
        _screenBounds = Utils.GetScreenXYBoundsInWorldSpace();
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach(PlatformBase platform in _platformPrefabs)
        {
            platform.Init(platform.transform.position, _playerController);
        }
    }

    public void Dispose()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}
