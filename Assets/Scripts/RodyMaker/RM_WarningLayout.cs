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
		Debug.Log("Accept button clicked");
		ResetLayout();
		gm.activeScene = newScene;
		gm.Reset();
		if (test){
			 if (gm.activeScene == 1)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(gm.activeScene);
		}
	}

	public void ResetLayout(){
		SetLayouts(gm.mainLayout);
		UnsetLayouts(gm.warningLayout);
	}
}
