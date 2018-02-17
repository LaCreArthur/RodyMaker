using UnityEngine;
using UnityEngine.SceneManagement;

public class RM_WarningLayout : RM_Layout {

	public int newScene;
	public bool test = false;

	public void CancelClick(){
		Debug.Log("Cancel button clicked");
		ResetLayout();
	}
	public void AcceptClick(){
		Debug.Log("Accept button clicked, load scene : " + newScene);
		
		if (PlayerPrefs.GetInt("scenesCount") < newScene)
			PlayerPrefs.SetInt("scenesCount", newScene);

		PlayerPrefs.SetInt("currentScene", newScene);
		
		if (test){
			 if (newScene == 0) // test of the title screen
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(2);
		}

		gm.currentScene = newScene;
		gm.Reset();
		ResetLayout();
	}

	public void ResetLayout(){
		SetLayouts(gm.mainLayout);
		UnsetLayouts(gm.warningLayout);
	}
}
