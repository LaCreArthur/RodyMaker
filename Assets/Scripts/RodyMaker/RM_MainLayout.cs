using UnityEngine;
using UnityEngine.UI;

public class RM_MainLayout : RM_Layout
{

    public Color notActiveColor ;
    public Color activeColor;
    public GameObject[] miniScenes;
    public Slider sliderScenes;

    public Button objBtn, IntroBtn;

    void Start() {
        SetActiveBtn();
    }

    public void SetActiveBtn(){
        if (gm.activeScene == 1)
            objBtn.interactable = IntroBtn.interactable = false;
        else
            objBtn.interactable = IntroBtn.interactable = true;
    }

    public void LoadSprites() {

        string path = PlayerPrefs.GetString("gamePath") + "\\";
        string spritePath = path + "Sprites\\";
        Debug.Log("LoadSprites spritePath : " + spritePath);

        // Load miniscenes
        miniScenes[0].GetComponent<Image>().sprite = 
            RM_SaveLoad.LoadSprite(spritePath+0+".png",36,21);
        for (int i = 1; i<18; i++) {
            GameObject miniScene = miniScenes[i];
            miniScene.GetComponent<Image>().sprite = 
                RM_SaveLoad.LoadSprite(spritePath+(i)+".1.png",36,21);
            miniScene.GetComponent<Image>().color = notActiveColor;
            //Debug.Log(spritePath+i+".1.png");
        }

        // Load Scene sprite
        if (gm.activeScene == 1)
            gm.scenePanel.GetComponent<SpriteRenderer>().sprite = 
                RM_SaveLoad.LoadSprite(spritePath+0+".png",320,240);
        else {
            gm.scenePanel.GetComponent<SpriteRenderer>().sprite = 
                RM_SaveLoad.LoadSprite(spritePath+(gm.activeScene-1)+".1.png",320,130);
            // Load animations frames
            for (int i=0; i<3; ++i)
			    RM_ImgAnimLayout.frames[i] = 
                    RM_SaveLoad.LoadSprite(spritePath+(gm.activeScene-1)+"."+ (i+2) +".png",320,130);
        }
    }

    public void RM_IntroClick()
    {
        Debug.Log("Intro button clicked");
        SetLayouts(gm.introLayout,gm.introTextObj);
        UnsetLayouts(gm.mainLayout);
        gm.introLayout.GetComponent<RM_IntroLayout>().titleInputField.text = gm.titleText;
    }
    public void ImagesClick()
    {
        Debug.Log("Images button clicked");
        SetLayouts(gm.imagesLayout);
        UnsetLayouts(gm.mainLayout);
        gm.imagesLayout.GetComponent<RM_ImagesLayout>().SetActiveBtn();
    }
    public void RM_ObjectsClick()
    {
        Debug.Log("Objects button clicked");
        SetLayouts(gm.introTextObj, gm.title, gm.objectsLayout);
        UnsetLayouts(gm.mainLayout);
    }

    public void TestClick()
    {
        Debug.Log("Test button clicked");
        gm.warningLayout.GetComponent<RM_WarningLayout>().test = true;
        gm.warningLayout.GetComponent<RM_WarningLayout>().newScene = gm.activeScene;
        UnsetLayouts(gm.mainLayout);
        SetLayouts(gm.warningLayout);
    }
    public void SaveClick()
    {
        Debug.Log("Save button clicked");
        RM_SaveLoad.SaveGame(gm);
    }

    public void RM_ResetClick()
    {
        Debug.Log("Reset button clicked");
        gm.warningLayout.GetComponent<RM_WarningLayout>().newScene = gm.activeScene;
        UnsetLayouts(gm.mainLayout);
        SetLayouts(gm.warningLayout);
    }


    public void RM_SceneClick(int scene)
    {
        Debug.Log("Scene " + scene + " button clicked");
        if (scene == gm.activeScene)
            return;
        gm.warningLayout.GetComponent<RM_WarningLayout>().newScene = scene;
        UnsetLayouts(gm.mainLayout);
        SetLayouts(gm.warningLayout);
    }

    public void MiniSceneUpdate() {
        for (int i = 0; i<17; i++) {
            GameObject miniScene = miniScenes[i];
            miniScene.GetComponent<Image>().color = notActiveColor;
            //Debug.Log(spritePath+i+".1.png");
        }
        miniScenes[gm.activeScene-1].GetComponent<Image>().color = activeColor;
    }

    public void SliderHandler ()
    {
        MoveMini((int)sliderScenes.value);
    }

    public void MoveMini(int sliderValue)
    {
        for (int i=0; i<4; ++i)
        {
            for (int j = 6*i; j < 6*i+6; ++j)
            {
                if (j < 6 * sliderValue) // mini over the top
                    miniScenes[j].SetActive(false);

                else if (j > 6 * sliderValue + 17) // mini under the bottom
                {
                    //Debug.Log("slider value : " + sliderValue + ", j value : " + j);
                    miniScenes[j].SetActive(false);
                }
                else
                {
                    miniScenes[j].SetActive(true);
                    Vector3 minipos = miniScenes[j].GetComponent<Transform>().localPosition;
                    //Debug.Log("before " + minipos);
                    miniScenes[j].GetComponent<Transform>().localPosition = new Vector3(minipos.x, 22.5f - ((i-sliderValue) * 22.0f), minipos.z);
                    //Debug.Log("after " + miniScenes[j].GetComponent<Transform>().localPosition);
                }
            }

        }
    }
}
