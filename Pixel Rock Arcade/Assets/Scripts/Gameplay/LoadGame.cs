﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour {

    [SerializeField]
    private GameObject mKoreoObjectsPrefab;

    [SerializeField]
    private GameObject mGameCanvasPrefab;

    [SerializeField]
    private GameObject mGameManagersPrefab;

    [SerializeField]
    private GameObject mGameObjectsPrefab;


    private void Awake()
    {
        Instantiate(mKoreoObjectsPrefab);
        Instantiate(mGameCanvasPrefab);
        Instantiate(mGameObjectsPrefab);
        Instantiate(mGameManagersPrefab);

        
    }

    private void Start()
    {
        GameplayCanvasScript.instance.Initialize();
        RhythmManager.instance.Initialize();
        GameInput.instance.Initialize();
        GameplayManager.instance.Initialize();

        //Screen.autorotateToLandscapeLeft = false;
        //Screen.autorotateToLandscapeRight = false;
    }



}
