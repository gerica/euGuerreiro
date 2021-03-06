﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour {
    [SerializeField] string areaToLoad;
    [SerializeField] string areaTransitionName;
    [SerializeField] AreaEntrace theEntrace;

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start() {
        theEntrace.TrancitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update() {
        if(shouldLoadAfterFade) {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0) {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        if (other.tag == "Player") {
            //SceneManager.LoadScene(areaToLoad);
            
            shouldLoadAfterFade = true;
            UiFade.instance.FadeToBlack();
            PlayerController.Instance.AreaTransitionName = areaTransitionName;
        }

    }
}
