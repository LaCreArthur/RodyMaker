using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RM_WarningLayout : RM_Layout {

	public int newScene;
	public bool test = false;
	public Text warningText;

	public void CancelClick(){
		Debug.Log("Cancel button clicked");
		ResetLayout();
	}
	public void AcceptClick(){
		
		Debug.Log("Accept button clicked, load scene : " + newScene);
		if (test){
			 if (newScene == 0) // test of the title screen
            	SceneManager.LoadScene(0);
        	else
            	SceneManager.LoadScene(2);
		} 
		else if (gm.currentScene ==  newScene) { // assuming newScene > 17 because RM_SceneClick checks it
			 // delete current scene (if saved)
			if (newScene <= PlayerPrefs.GetInt("scenesCount"))
				RM_SaveLoad.DeleteScene(newScene);
			else 
				Debug.Log("cancel new scene creation");
			// load the preceding
			newScene--;
		}
		PlayerPrefs.SetInt("currentScene", newScene);
		

		gm.currentScene = newScene;
		gm.Reset();
		ResetLayout();
	}

	public void ResetLayout(){
		SetLayouts(gm.mainLayout);
		UnsetLayouts(gm.warningLayout);
	}
}
