using SFB;
using UnityEngine;
using UnityEngine.UI;

public class RM_ImagesLayout : RM_Layout {

 	public Button imgAnimBtn;
	void Start(){
        SetActiveBtn();
	}
	
	public void SetActiveBtn(){
		imgAnimBtn.interactable = (gm.currentScene == 0)?false:true;
	}
	public void ReturnClick(){
		Debug.Log("Images return button clicked");
		SetLayouts(gm.mainLayout);
		UnsetLayouts(gm.imagesLayout);
	}
	public void ImgAnimClick(){
		Debug.Log("Img Animes button clicked");
		SetLayouts(gm.imgAnimLayout);
		UnsetLayouts(gm.imagesLayout);
	}
    public void ImportClick()
    {
        Debug.Log("Import button clicked");
        var extensions = new[] {new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),};
        string path = null;
        string[] files = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        if (files.Length != 0)
            path = files[0];
        else
            return;

		gm.scenePanel.GetComponent<Transform>().localPosition = new Vector3(0,-35,0);
        gm.scenePanel.GetComponent<SpriteRenderer>().sprite = RM_SaveLoad.LoadSprite(path,320,130);
        // update the miniature
        gm.mainLayout.GetComponent<RM_MainLayout>().miniScenes[gm.currentScene].GetComponent<Image>().sprite = RM_SaveLoad.LoadSprite(path,36,21);
    }
}
