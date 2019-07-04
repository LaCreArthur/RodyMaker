﻿using SFB;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;

public static class RM_SaveLoad {

    public static bool CustomFolder = false;

	public static void SaveGame (RM_GameManager gm)
    {
        int scene = gm.currentScene;
        
        // increment the counter only if scene is saved, avoid create a second new scene without saving the first one
        if (PlayerPrefs.GetInt("scenesCount") + 1 == scene) {
			Debug.Log("Adding a new scene to the counter...");
			PlayerPrefs.SetInt("scenesCount", scene); 
        }

        string path = PlayerPrefs.GetString("gamePath") + "\\";
        string newPath = "";
        
        /***
        // chose the destination of the game folder in which the scene will be saved
        // removed with rody collection to be more convenient for users
        if (CustomFolder == false) {
            string[] newPaths = StandaloneFileBrowser.OpenFolderPanel("Dossier de ton jeu...", Application.dataPath + "/StreamingAssets", false);
            if (newPaths.Length == 0){
                Debug.Log("save canceled");
                return;
            }
            else {
                newPath = newPaths[0] + "\\";
                CustomFolder = true;
                PlayerPrefs.SetString("gamePath", newPath);
                Debug.Log("SaveGame : new gamePath : " + newPath);
            }
        }
        else {
            newPath = path;
        }
        ***/
        
        newPath = path;

        if (scene == 0) {
            SaveSprite(gm.scenePanel.GetComponent<SpriteRenderer>().sprite.texture, newPath, "0", 320, 240);
            return; // save only the image
        }
        
        
        for (int i = 1; i<5; i++){
            SaveSprite(gm.scenePanel.GetComponent<SpriteRenderer>().sprite.texture, newPath, scene+"."+i);
        }
        
        // save the animation frames if there are set
        int framesCount = RM_ImgAnimLayout.frames.Count;
        if (framesCount > 0) 
            for (int j = 0; j < framesCount; j++)
            {
                Sprite frame = RM_ImgAnimLayout.frames[j];
                if (frame != null)
                    SaveSprite(frame.texture, newPath, scene+"."+(j+2));
            }

        string oldTxtPath = path + "levels.rody";
        string newTxtPath = newPath + "levels.rody";
        
        // cancel chosing file 
        if (newPath == null) 
            return;
        
        string[] lines = File.ReadAllLines(oldTxtPath);
        
        // create the file if it is a new file
        if (!File.Exists(newTxtPath)) {
            File.Copy(oldTxtPath, newTxtPath);
            Debug.Log("creation of " + newTxtPath);
        }
        
        try
        {   // Open the text file using a stream writer.
            using (TextWriter sw = new StreamWriter(newTxtPath))
            {
                WriteToTxt(sw, scene, lines, gm);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be write:");
            Console.WriteLine(e.Message);
        }
        Debug.Log("Save done !");
    }

	private static void SaveSprite(Texture2D tex, string path, string scene, int width = 320, int height = 130) {
        RM_TextureScale.Point(tex, width, height);
        var bytes = tex.EncodeToPNG();
        string spritePath = path + "\\Sprites\\";
        if (!Directory.Exists(spritePath))
            Directory.CreateDirectory(spritePath);
    
        FileStream png = new FileStream(spritePath + scene + ".png", FileMode.Create);
    
        var binary = new BinaryWriter(png);
        binary.Write(bytes);
        png.Close();
    }

    private static void WriteToTxt(TextWriter sw, int scene, string[] lines, RM_GameManager gm){
        int currentScene=0;
        int currentLine=0;

        // preceding scene rewriting
        for (int i = 1; i<scene; i++) {
            for (int j = currentLine; lines[j] != "~"; j++) {
                sw.WriteLine(lines[j]);
                currentLine++;
            }
            sw.WriteLine("~");
            currentLine++;
            currentScene++;
        }

        // current scene writting
        sw.WriteLine("#######################\n###### scene "+scene+" ########\n#######################");
        sw.WriteLine("## phonems\n" + gm.introDial1 + "\n" + gm.introDial2 + "\n" + gm.introDial3 + "\n" + gm.objDial + "\n" + gm.ngpDial + "\n" + gm.fswDial);
        sw.WriteLine("## texts [string]\n" + gm.titleText + "\n" + gm.introText + "\n" + gm.objText + "\n" + gm.ngpText + "\n" + gm.fswText);
        sw.WriteLine("## musics [i1..i3 | l1..l15]\n" + gm.musicIntro + "," + gm.musicLoop);
        sw.WriteLine("## pitch [float]\n" + gm.pitch1 + "," + gm.pitch2 + "," + gm.pitch3);
        sw.WriteLine("## is mastico speaking [booleans] ?\n" + BoolToString(gm.isMastico1) + "," + BoolToString(gm.isMastico2) + "," + BoolToString(gm.isMastico3) + "," + BoolToString(gm.isZambla));
        sw.WriteLine("## obj position\n"     + objListToString(gm.obj));
        sw.WriteLine("## obj size\n"         + objListToString(gm.obj, true));
        sw.WriteLine("## objNear position\n" + objListToString(gm.objNear));
        sw.WriteLine("## objNear size\n"     + objListToString(gm.objNear, true));
        sw.WriteLine("## NGP position\n"     + objListToString(gm.ngp));
        sw.WriteLine("## NGP size\n"         + objListToString(gm.ngp, true));
        sw.WriteLine("## NgpNear position\n" + objListToString(gm.ngpNear));
        sw.WriteLine("## NgpNear size\n"     + objListToString(gm.ngpNear, true));
        sw.WriteLine("## FSW position\n"     + objListToString(gm.fsw));
        sw.WriteLine("## FSW size\n"         + objListToString(gm.fsw, true));
        sw.WriteLine("## fswNear position\n" + objListToString(gm.fswNear));
        sw.WriteLine("## fswNear size\n"     + objListToString(gm.fswNear, true));
        sw.WriteLine("~");
        // increment the scene counter
        currentScene++; 
        currentLine+=47; // one scene is 47 lines
        
        for (int i = currentScene; i < PlayerPrefs.GetInt("scenesCount") + 1; i++) { //TODO verify this condition
            // following scene rewriting
            for (int j=currentLine; lines[j] != "~"; j++) {
                    sw.WriteLine(lines[j]);
                currentLine++;
            }
            sw.WriteLine("~");
            currentLine++;
            currentScene++;
        }
        sw.Close();
    }

    private static string objListToString(List<GameObject> objList, bool size = false) {
        string objStr = "";
        foreach (GameObject obj in objList)
        {
            if (size)
                objStr += (Vector2) obj.GetComponent<RectTransform>().sizeDelta + ";";
            else 
                objStr += (Vector2) obj.GetComponent<RectTransform>().localPosition + ";";
        }
        Debug.Log(objStr);
        return objStr;
    }
    private static string BoolToString(bool b) {
        return (b)?"1":"0";
    }

    public static string[] LoadSceneTxt(int scene)
    {
        string path = PlayerPrefs.GetString("gamePath") + "\\";
        // Debug.Log("LoadSceneTxt gamePath : "+path);
        string[] sceneStr = new string[26];
        try
        {   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(path + "levels.rody"))
            {
                string line;

                for (int j=1; j<scene; j++) {
                   while ((line = sr.ReadLine()) != "~") ; // jump to the selected scene in the txt
                }

                int i = 0;
                while ((line = sr.ReadLine()) != "~") {
                    if (line == "") {
                        sceneStr[i] = "";
                        i++;
                    }
                    else
                        if (line[0] == '#') // does not count as a line
                            continue;
                        else 
                        {
                            if (line[0] == '.') // empty but count as a line
                                sceneStr[i] = "";
                            else
                                sceneStr[i] = readLine(line);
                            //Debug.Log("line "+i+ " : " + sceneStr[i]);
                            i++;
                        }
                }
                sr.Close();
            }
            
			return sceneStr;
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
		return null;
    }

    public static int CountScenesTxt() {
        string path = PlayerPrefs.GetString("gamePath") + "\\";
        int count = 0;
        try
        {   
            using (StreamReader sr = new StreamReader(path + "levels.rody"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    if (line == "~") 
                        count++;
                sr.Close();
            }    
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
		return count;
    }

    private static string readLine(string line) 
    {
        string newLine = (line[0] == '\\')? "" : line[0].ToString();
        for (int i=1; i<line.Length; ++i)
        {
            if (line[i] == 'n' && line[i-1] == '\\')
                newLine = String.Concat(newLine, "\n");
            else if (line[i] != '\\')
                newLine = String.Concat(newLine, line[i]);
        }
        return newLine;
    }

    public static List<Sprite> LoadSceneSprites(int scene) 
    {
        List<Sprite> sceneSprites = new List<Sprite>();

        string path = PlayerPrefs.GetString("gamePath") + "\\";
        string spritesPath = path + "Sprites\\" + scene.ToString();

        Debug.Log("(LoadSceneSprites) path : " + spritesPath);

        // search the number of frames
		DirectoryInfo dir = new DirectoryInfo(path + "Sprites\\");
        var files = dir.GetFiles(scene + ".*.png");

        for (int i = 1; i < files.Length + 1; i++) // frames index start at 1 not 0
        {
            sceneSprites.Add(LoadSprite(spritesPath, i, 320, 130));
        }

        return sceneSprites;
    }

    public static Sprite LoadSprite(string spritePath, int index, int width, int height) {
        Texture2D tex = null;
        byte[] fileData;
        // index == 0 means import a new sprite else load an existing one
        string file = index == 0 ? spritePath : spritePath + "." + index.ToString() + ".png";

        //Debug.Log("[LoadSprite] spritePath : " +  spritePath + ", index : " + index + ", file : " + file);

        if (!File.Exists(file)) // sprite missing or adding a new scene
        {
            if (index > 1 && index < 5) {
                string baseFrame = spritePath + ".1.png";
                // if frame is one of the 3 first & not exist but base do, copy base and load it
                if (File.Exists(baseFrame))
                    File.Copy(baseFrame, file);
                // else load the default sprite
                else {
                    Debug.Log("(LoadSprite) file does not exist : " + file + ", loading base sprite instead");
                    File.Copy(PlayerPrefs.GetString("gamePath") + "\\Sprites\\base.png", file);
                }
            }
            else {
                File.Copy(PlayerPrefs.GetString("gamePath") + "\\Sprites\\base.png", file);
            }
        }

        //Debug.Log("(LoadSprite) load : " + file);
        fileData = File.ReadAllBytes(file);

        tex = new Texture2D(2, 2);
        tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        tex.filterMode = FilterMode.Point;
        RM_TextureScale.Point(tex,width,height); // resize the texture dimensions to 320*130

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1f);
        return sprite;
    }

    public static List<GameObject> ReadObjects(string name, string posStr, string sizeStr, bool isNear = false) {
        
        if (posStr[posStr.Length - 1] == ';')
            posStr = posStr.Substring(0, posStr.Length - 1);

        if (sizeStr[sizeStr.Length - 1] == ';')
            sizeStr = sizeStr.Substring(0, sizeStr.Length - 1);
        
        string[] posList  =  posStr.Split(';');
        string[] sizeList = sizeStr.Split(';');

        int zonesCount = posList.Length;
        Debug.Log("(readObjects) posList length is : " + zonesCount);
        
        if (posList.Length != sizeList.Length) {
            Debug.Log("(readObjects) posList not same length as sizeList !");
            return null;
        }

        // if clone is an object zone parent should be it nearzone or if it is a Near object zone, parent is Objects

        List<GameObject> objects = new List<GameObject>();

        for (int i=0; i < zonesCount; ++i) {
            
            GameObject parent, obj;
            if (isNear) {
                parent = GameObject.Find("Objects");
                obj = GameObject.Instantiate(GameObject.Find("ObjNear"), parent.GetComponent<RectTransform>());
            }
            else {
                parent = GameObject.Find(name + "Near" + i);
                obj = GameObject.Instantiate(GameObject.Find("Obj"), parent.GetComponent<RectTransform>());
            }
            
            obj.name = name+i;
             
            Debug.Log("(readObjects) obj name : " + obj.name);
            Debug.Log("(readObjects) parent name : " + parent.name);

            objects.Add(LoadObject(obj, posList[i], sizeList[i]));
        }
        return objects;
    }
    public static GameObject LoadObject(GameObject obj, string pos, string size) {
		
		//Debug.Log("set position and size for : " + name);
		
        
		RectTransform rectTransform = obj.GetComponent<RectTransform>();

		//Debug.Log("## " + name + " position\n"+rectTransform.localPosition.x+","+rectTransform.localPosition.y);
		//Debug.Log("## " + name + " size\n"+rectTransform.sizeDelta.x+","+rectTransform.sizeDelta.y);

		string[] positions = pos.Split(',');
		string[] sizes     = size.Split(',');
		
		rectTransform.localPosition = new Vector3(
			float.Parse(positions[0].Replace("(","")),
			float.Parse(positions[1].Replace(")","")),0f);

		rectTransform.sizeDelta = new Vector3(
			float.Parse(sizes[0].Replace("(","")),
			float.Parse(sizes[1].Replace(")","")),0f);

		// Debug.Log(name + "pos : " + rectTransform.localPosition + "\n" 
		// 		+ name + "size : " + rectTransform.sizeDelta);
        return obj;
    }

    public static void DeleteScene(int scene) {
        Debug.Log("(DeleteScene) Erase level " + scene + ", scenes count : " + PlayerPrefs.GetInt("scenesCount"));
        
        string path = PlayerPrefs.GetString("gamePath") + "\\";
        // Debug.Log("(DeleteScene) Deleting scene " + scene + " sprites ...");
        // for (int i=0; i < 5; ++i){
        //     File.Delete(path + "Sprites\\" + scene + "." + i + ".png" );
        // }

        Debug.Log("(DeleteScene) Deleting text ...");
        int currentScene=0;
        int currentLine=0;
        string[] lines = File.ReadAllLines(path + "levels.rody");

        try
        {   // Open the text file using a stream writer.
            using (TextWriter sw = new StreamWriter(path + "levels.rody"))
            {
                for (int i = 1; i<scene; i++) {
                    for (int j = currentLine; lines[j] != "~"; j++) {
                        sw.WriteLine(lines[j]);
                        currentLine++;
                    }
                    sw.WriteLine("~");
                    currentLine++;
                    currentScene++;
                }
                // skip current scene
                currentScene++;
                currentLine+=47; // move the lines offset to the next scene
                for (int i = currentScene; i < PlayerPrefs.GetInt("scenesCount") + 1; i++) {
                    // following scene rewriting
                    for (int j=currentLine; lines[j] != "~"; j++) {
                            sw.WriteLine(lines[j]);
                        currentLine++;
                    }
                    sw.WriteLine("~");
                    currentLine++;
                    currentScene++;
                }
                sw.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be write:");
            Console.WriteLine(e.Message);
        }
        PlayerPrefs.SetInt("scenesCount", PlayerPrefs.GetInt("scenesCount") - 1);
        Debug.Log("Erase level done !, scenes count now : " + PlayerPrefs.GetInt("scenesCount"));
    }

    public static void SetActiveZones(List<GameObject> zonesNear, List<GameObject> zones, bool activate = true) {
		foreach (GameObject near in zonesNear)
		{
			near.SetActive(activate);
		}
		foreach (GameObject zone in zones)
		{
			zone.SetActive(activate);
		}
	}

        public static void LoadCredits(Text title, Text credits)
    {
        string path = PlayerPrefs.GetString("gamePath") + "\\credits.txt";
        Debug.Log("Read Credits at " + path);
        // Debug.Log("LoadSceneTxt gamePath : "+path);
        try
        {   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(path))
            {
                title.text = sr.ReadLine();

                string creditsStr = "";
                string line;
                while ((line = sr.ReadLine()) != null) {
                    creditsStr += line + "\n";
                }
                sr.Close();
                credits.text = creditsStr;
                Debug.Log("Read Credits : Done!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}
