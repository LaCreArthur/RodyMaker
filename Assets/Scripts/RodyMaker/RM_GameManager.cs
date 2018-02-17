using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RM_GameManager : MonoBehaviour {

	public GameObject mainLayout;
	public GameObject imagesLayout;
	public GameObject imgAnimLayout;
	public GameObject introLayout;
	public GameObject dialoguesLayout;
	public GameObject dialLayout;
	public GameObject objectsLayout; 
	public GameObject objLayout; 
	public GameObject musicLayout;
	public GameObject warningLayout;
	public GameObject introTextObj;
	public GameObject objTextObj;
	public GameObject ngpTextObj;
	public GameObject fswTextObj;
	public GameObject title;
	public GameObject scenePanel;
	public GameObject objNear, obj, ngpNear, ngp, fswNear, fsw;
	public SoundManager sm;
	public int currentScene = 0;
	[HideInInspector] 
	public float pitch1, pitch2, pitch3;
	[HideInInspector] 
	public bool isMastico1, isMastico2, isMastico3, isZambla, modified = false;
	[HideInInspector] 
	public string currentDial,currentText,introDial1,introDial2,introDial3,objDial,ngpDial,fswDial,titleText,introText,objText,ngpText,fswText, musicIntro, musicLoop;

	void Start() { 
		
		// if gm is load from ingame
		currentScene = PlayerPrefs.GetInt("currentScene");
		
		mainLayout.SetActive(true);
		
		introLayout.SetActive(false);
		warningLayout.SetActive(false);
		dialoguesLayout.SetActive(false);
		dialLayout.SetActive(false);
		objectsLayout.SetActive(false); 
		objLayout.SetActive(false); 
		musicLayout.SetActive(false);
		introTextObj.SetActive(false);
		title.SetActive(false);

		Reset();
	}

	public void Reset() {

		// Variables for the txt file
		introDial1 = ".";
		introDial2 = ".";
		introDial3 = ".";
		objDial    = ".";
		ngpDial    = ".";
		fswDial    = ".";
		titleText  = "glitch title";
		introText  = "glitch intro";
		objText    = ".";
		ngpText    = ".";
		fswText    = ".";
		pitch1 = 1f;
		pitch2 = 1f;
		pitch3 = 1f;
		// Variables for the interface
		introTextObj.GetComponent<Text>().text = "Dialogues de la scène";
		title.GetComponent<Text>().text 	   = "Titre de la scène";
		objTextObj.GetComponent<Text>().text   = "Indice du premier objet";
		ngpTextObj.GetComponent<Text>().text   = "Indice de l'objet New Game Plus";
		fswTextObj.GetComponent<Text>().text   = "Indice de l'objet FromSoftware";

		introLayout.GetComponent<RM_IntroLayout>().titleInputField.text = null;
		objLayout.GetComponent<RM_ObjLayout>().objInputField.text = null;
		
		// Reset miniatures
		mainLayout.GetComponent<RM_MainLayout>().LoadSprites();
		mainLayout.GetComponent<RM_MainLayout>().MiniSceneUpdate();
		mainLayout.GetComponent<RM_MainLayout>().SetActiveBtn();
		// objLayout.GetComponent<RM_ObjLayout>().zoneNear.SetActive(false);

		if (currentScene != 0) {
			ReadSceneStr();
		}
	}

	 public void ReadSceneStr(){
        
        string[] sceneStr = new string[26];

		if (PlayerPrefs.GetInt("scenesCount") + 1 == currentScene) {
			Debug.Log("Adding a new scene to the counter...");
			PlayerPrefs.SetInt("scenesCount", currentScene); //TODO update this only when save or it will crash if adding a second scene, btw activate the next new scene button only when save also 
			Debug.Log("Load default scene text...");
			sceneStr = RM_SaveLoad.LoadSceneTxt(currentScene-1);
			mainLayout.GetComponent<RM_MainLayout>().MoveMini((int)mainLayout.GetComponent<RM_MainLayout>().sliderScenes.value);
		}
		else
			sceneStr = RM_SaveLoad.LoadSceneTxt(currentScene);

		// get music string
		musicIntro = sceneStr[11].Split(',')[0];
		musicLoop  = sceneStr[11].Split(',')[1];			

		string[] pitchs = sceneStr[12].Split(',');
		pitch1     = float.Parse(pitchs[0]);
		pitch2     = float.Parse(pitchs[1]);
		pitch3     = float.Parse(pitchs[2]);

		string[] booleans = sceneStr[13].Split(',');
		isMastico1 = booleans[0] == "1";
		isMastico2 = booleans[1] == "1";
		isMastico3 = booleans[2] == "1";
		isZambla   = booleans[3] == "1";
		
		// wow such refactoring
		introDial1 = sceneStr[0];
		introDial2 = sceneStr[1];
		introDial3 = sceneStr[2];
		objDial    = sceneStr[3];
		ngpDial    = sceneStr[4];
		fswDial    = sceneStr[5];
		titleText  = sceneStr[6];
		title.GetComponent<Text>().text = titleText;
		introText  = sceneStr[7];
		introTextObj.GetComponent<Text>().text = introText;
		objText    = sceneStr[8];
		ngpText    = sceneStr[9];
		fswText    = sceneStr[10];

		objNear.SetActive(true);
		ngpNear.SetActive(true);
		fswNear.SetActive(true);
        RM_SaveLoad.LoadObject("Object" , sceneStr[14], sceneStr[15]);
        RM_SaveLoad.LoadObject("ObjNear", sceneStr[16], sceneStr[17]);
        RM_SaveLoad.LoadObject("Ngp"	, sceneStr[18], sceneStr[19]);
        RM_SaveLoad.LoadObject("NgpNear", sceneStr[20], sceneStr[21]);
        RM_SaveLoad.LoadObject("Fsw"	, sceneStr[22], sceneStr[23]);
        RM_SaveLoad.LoadObject("FswNear", sceneStr[24], sceneStr[25]);
		objNear.SetActive(false);
		ngpNear.SetActive(false);
		fswNear.SetActive(false);
    }

	void Update() {
		if (Input.GetKeyUp(KeyCode.Escape)){
			SceneManager.LoadScene(1);
		}
	}
}
