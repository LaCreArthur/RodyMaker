﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO; // DirectoryInfo

public class GameManager : MonoBehaviour {

	public SceneLoading loading;
	public Intro intro;
	public Scene scene;
	public AudioSource source;
	public SoundManager sm;
	public Animator MasticoAnimator;
	public GameObject p_layout;
	public GameObject p_title;
	public GameObject p_text;
	public GameObject b_next;
	public GameObject b_draw;
	public GameObject b_repeat;
	public GameObject b_ngp;
	public GameObject b_fsw;
	public GameObject b_mastico;
	public GameObject objNearTemplate, objTemplate;
	public SceneAnimator sceneAnimator;
	[HideInInspector]
	public List<GameObject> obj, objNear, ngp, ngpNear, fsw, fswNear;
	[HideInInspector] 
	public bool introOver,objOver,ngpOver,fswOver,interObj,clickIntro = false,clickObj = false;
	[HideInInspector] 
	public AudioClip[] currentFx;
	[HideInInspector] 
	public List<Sprite> sceneSprites;
	[HideInInspector] 
	public string currentDial,currentText,introDial1,introDial2,introDial3,objDial,ngpDial,fswDial,titleText,introText,objText,ngpText,fswText;
	[HideInInspector]
	public int currentScene;

	// Use this for initialization
	void Start () {
        // init the good dial for the current scene
        currentScene = PlayerPrefs.GetInt("currentScene");
		
		if (currentScene <= PlayerPrefs.GetInt("scenesCount")) {
			InitScene (currentScene);

			// init variables
			introOver = false;
			objOver = false;
			
			// start the sequence of events
			StartCoroutine (Play());
		}
		else { // try to load a non existing scene
			Debug.Log("How dare you do that !");
			SceneManager.LoadScene(5);
		}
	}
	
	IEnumerator Play() {
		// load the scene assets with lag
		loading.Play();
		//Debug.Log(loading.isPlaying);
		while (loading.isPlaying) {
			yield return null;
		}
		Debug.Log("loading done!");
		//Debug.Log(loading.isPlaying);
		// play intro music and dials
		intro.Play ();
		while (intro.isPlaying) {
			yield return null;
		}
		Debug.Log("intro done!");
		// wait for click in intro

		while (!introOver) {
			yield return null;
		}
		// active the object interactable zone
		// obj.SetActive(true);
		objNear[0].SetActive(true);
		obj[0].SetActive(true);
		Debug.Log("isInit set to false");
		scene.isInit = false;
		scene.Play(1);
		// wait for click on ngp or next
		while (!objOver) {
			scene.initStep(1);
			yield return null;
		}
		// ngp.SetActive(true);
		ngpNear[0].SetActive(true);
		ngp[0].SetActive(true);
		Debug.Log("isInit set to false");
		scene.isInit = false;
		scene.Play(2);
		while (!ngpOver) {
			scene.initStep(2);
			yield return null;
		}
		// fsw.SetActive(true);
		fswNear[0].SetActive(true);
		fsw[0].SetActive(true);
		Debug.Log("isInit set to false");
		scene.isInit = false;
		scene.Play(3);
		while (!fswOver) {
			scene.initStep(3);
			yield return null;
		}
	}

	void InitScene(int scene) {

		introDial1 = "g_l_i_t_ch";
		introDial2 = "g_l_i_t_ch";
		objDial    = "g_l_i_t_ch";
		ngpDial    = "g_l_i_t_ch";
		fswDial    = "g_l_i_t_ch";
		introText  = "intro glitch";
		objText    = "object glitch";
		ngpText    = "ngp object glitch";
		fswText    = "fsw object glitch";

		ReadSceneStr();

		// Load Sprites
		sceneSprites = new List<Sprite>();
		sceneSprites = RM_SaveLoad.LoadSceneSprites(scene);
		Debug.Log(sceneSprites.ToString());
		sceneAnimator.baseFrame = sceneSprites[0];
		// copie sceneSprite to scene animator frame
		sceneAnimator.frames = new List<Sprite>(sceneSprites);
		sceneAnimator.frames.RemoveAt(0); // remove the base image, not part of the animation
		Debug.Log("GM : There are " + sceneAnimator.frames.Count + " animation frames in this scene");
		// sum of the non-mastico dials
		if (!sm.isMastico1) sceneAnimator.sumDial++;
		if (getDial(2).Count > 0 && !sm.isMastico2) sceneAnimator.sumDial++;
		if (getDial(6).Count > 0 && !sm.isMastico3) sceneAnimator.sumDial++;
		sceneAnimator.firstDial = (!sm.isMastico1) ? 1 : (!sm.isMastico2) ? 2 : (!sm.isMastico3) ? 3 : -1; // index of first non mastico dial
		Debug.Log("GM : There are " + sceneAnimator.sumDial + " people speaking, the first is : " + sceneAnimator.firstDial);
	}

	public List<int> getDial(int dial) {
		switch(dial) {
			case 1: return sm.StringToPhonemes(introDial1);
			case 2: return sm.StringToPhonemes(introDial2);
			case 3: return sm.StringToPhonemes(objDial);
			case 4: return sm.StringToPhonemes(ngpDial);
			case 5: return sm.StringToPhonemes(fswDial);
			case 6: return sm.StringToPhonemes(introDial3);
			default: return sm.StringToPhonemes("g_et_t_d_i_a_l g_l_i_t_ch");
		}
	}

	  public void ReadSceneStr(){
        
        string[] sceneStr = new string[26];
        
        Debug.Log("loaded scene : '" + currentScene + "' from " + PlayerPrefs.GetString("gamePath"));
        sceneStr = RM_SaveLoad.LoadSceneTxt(currentScene);
        
        // Load musics from .txt
        source.clip = sm.getMusic(sceneStr[11].Split(',')[0]);
        intro.sceneMusic.clip = sm.getMusic(sceneStr[11].Split(',')[1]);

        // Load pitchs  
        string[] pitchs = sceneStr[12].Split(',');
        sm.pitch1     = float.Parse(pitchs[0]);
        sm.pitch2     = float.Parse(pitchs[1]);
        sm.pitch3     = float.Parse(pitchs[2]);
        
        // Load mastico/zambla isSpeaking
        string[] booleans = sceneStr[13].Split(',');
        sm.isMastico1 = booleans[0] == "1";
        sm.isMastico2 = booleans[1] == "1";
        sm.isMastico3 = booleans[2] == "1";
        sm.isZambla   = booleans[3] == "1";

        introDial1 = sceneStr[0];
        introDial2 = sceneStr[1];
        introDial3 = sceneStr[2];
        objDial    = sceneStr[3];
        ngpDial    = sceneStr[4];
        fswDial    = sceneStr[5];
        titleText  = sceneStr[6];
        introText  = sceneStr[7];
        objText    = sceneStr[8];
        ngpText    = sceneStr[9];
        fswText    = sceneStr[10];

        objNear = RM_SaveLoad.ReadObjects("objNear", sceneStr[16], sceneStr[17], true);
        ngpNear = RM_SaveLoad.ReadObjects("ngpNear", sceneStr[20], sceneStr[21], true);
        fswNear = RM_SaveLoad.ReadObjects("fswNear", sceneStr[24], sceneStr[25], true);
        
        obj = RM_SaveLoad.ReadObjects("obj" , sceneStr[14], sceneStr[15]);
		ngp = RM_SaveLoad.ReadObjects("ngp" , sceneStr[18], sceneStr[19]);
		fsw = RM_SaveLoad.ReadObjects("fsw" , sceneStr[22], sceneStr[23]);

		List<List<GameObject>> zonesList = new List<List<GameObject>>{objNear, ngpNear, fswNear};
		foreach(List<GameObject> zones in zonesList)
			foreach (GameObject zone in zones)
				zone.SetActive(false);
		
		objNearTemplate.SetActive(false);
		objTemplate.SetActive(false);
    }

	void Update() {
		if (Input.GetKeyUp(KeyCode.Escape)){
			SceneManager.LoadScene(2);
		}
	}

}
