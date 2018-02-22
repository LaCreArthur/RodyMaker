using SFB;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	public AudioSource titleMusic;
	public GameObject[] blackPanels;
	public Image titleImage;
	private int click = 0;
	private bool isTitle = true; // because same script is use for credits 
	//TODO : improve this
	
	// Use this for initialization
	void Start () {
		Screen.SetResolution(1280, 800, false);
		isTitle = (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)? true:false;
		if (isTitle) {
			int customGame = PlayerPrefs.GetInt("customGame");
			string gamePath = Application.dataPath + "\\RodyAIbiza";
			if (customGame == 1) {
				string customGamePath = PlayerPrefs.GetString("gamePath");
				if (customGamePath != "" || Directory.Exists(customGamePath)){
					gamePath = customGamePath;
					Debug.Log("Launching Custom Game");
				}
				PlayerPrefs.SetInt("customGame",0);
			}

			PlayerPrefs.SetString("gamePath", gamePath);
			Debug.Log("Title set gamePath as : " + gamePath);

			PlayerPrefs.SetInt("scenesCount", RM_SaveLoad.CountScenesTxt());
			Debug.Log("scenes in this game folder : " + PlayerPrefs.GetInt("scenesCount"));
		
			titleImage.sprite = RM_SaveLoad.LoadSprite(gamePath+"\\Sprites\\0.png",320,200);
			Cursor.visible = false;
		}
		else {
			Cursor.visible = true;
			Debug.Log("CREDITS ROLL");
			RM_SaveLoad.LoadCredits(GameObject.Find("Title").GetComponent<Text>(),GameObject.Find("Credits").GetComponent<Text>());
		}
	
		StartCoroutine(music());
		StartCoroutine(appear());
	}

	void Update() {
        if (Input.GetMouseButtonDown(0))
        {
			click++;
			if (click == 10) {
				SceneManager.LoadScene(1);
			}
        }
	}
	IEnumerator music() {
		yield return new WaitForSeconds(0.1f);
		while(titleMusic.isPlaying)
			yield return null;
		Debug.Log("title music ended");
		Cursor.visible = true;
		if (isTitle)
			SceneManager.LoadScene(1);
	}

	IEnumerator appear() {
		if (!isTitle) {
			yield return null;
		}
		else {
			yield return new WaitForSeconds(0.5f);
			foreach (GameObject panel in blackPanels) {
				yield return new WaitForSeconds(0.2f);
				panel.SetActive (false);
			}
		}
	}

	public void skipCredit(){
		SceneManager.LoadScene(1);
	}
}
