using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour {

	public GameManager gm;
	public void NextClick() {
		if (gm.clickIntro) {
			Debug.Log ("next clicked in intro");
			gm.intro.sceneMusic.Stop();
			gm.introOver = true;
			gm.clickIntro = false;
		}
		else if (gm.clickObj) {
			Debug.Log ("next clicked in obj/ngp/fsw, next = " + PlayerPrefs.GetInt("currentScene") + 1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("currentScene", PlayerPrefs.GetInt("currentScene")+1);
			SceneManager.LoadScene(2);
        }
	}

	public void LaunchCredit() {
		SceneManager.LoadScene(4);
	}

	public void RepeatClick() {
		if (gm.clickIntro) {
			gm.clickIntro = false;
			Debug.Log("repeat intro dialog");
			gm.intro.sceneMusic.Stop();
			StartCoroutine(gm.intro.Dialog());
		}
		else if (gm.clickObj) {
			gm.clickObj = false;
			Debug.Log("repeat obj/ngp/fsw dialog");
			StartCoroutine(gm.scene.dialog());
		}
	}

	public void DrawClick() {
		//PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);
		SceneManager.LoadScene(5);
	}

	public void ngpClick() {
		gm.objOver = true;
		gm.clickObj = false;
	}
	public void fswClick() {
		gm.ngpOver = true;
		gm.clickObj = false;
	}
}
