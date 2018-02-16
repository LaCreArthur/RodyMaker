using SFB;
using UnityEngine;
using UnityEngine.UI;

public class RM_ImgAnimLayout : RM_Layout {

	public static Sprite[] frames = new Sprite[3];

	public void ReturnClick(){
		Debug.Log("Images return button clicked");
		SetLayouts(gm.imagesLayout);
		UnsetLayouts(gm.imgAnimLayout);
	}

	public void ImportClick(int i)
    {
        Debug.Log("Import button clicked");
        var extensions = new[] {new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),};
        string path = null;
        string[] files = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        if (files.Length != 0)
            path = files[0];
        else
            return;

		frames[i] = RM_SaveLoad.LoadSprite(path,320,130);
    }
}
