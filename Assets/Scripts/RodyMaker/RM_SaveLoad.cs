using SFB;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public static class RM_SaveLoad {

    public static bool CustomFolder = false;

	public static void SaveGame (RM_GameManager gm)
    {
        int scene = gm.activeScene-1;
        string path = PlayerPrefs.GetString("gamePath") + "\\";
        string newPath = "";
        
        if (CustomFolder == false) {
            string[] newPaths = StandaloneFileBrowser.OpenFolderPanel("Dossier de ton jeu...", "", false);
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


        if (scene == 0) {
            SaveSprite(gm.scenePanel.GetComponent<SpriteRenderer>().sprite.texture, newPath, "0", 320, 240);
            return; // save only the image
        }
        
        
        for (int i = 1; i<5; i++){
            SaveSprite(gm.scenePanel.GetComponent<SpriteRenderer>().sprite.texture, newPath, scene+"."+i);
        }
        
        // save the animation frames if there are set
        Sprite[] frames = RM_ImgAnimLayout.frames;
        if (frames.Length > 0) 
            for (int j = 0; j<3; j++)
            {
                Sprite frame = frames[j];
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
        TextureScale.Point(tex, width, height);
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
        int currentScene=1;
        int currentLine=0;

        for (int i = 1; i<scene; i++) {
            // preceding scene rewriting
            for (int j = currentLine; lines[j] != "~"; j++) {
                sw.WriteLine(lines[j]);
                currentLine++;
            }
            sw.WriteLine("~");
            currentLine++;
            currentScene++;
        }

        sw.WriteLine("#######################\n###### scene "+scene+" ########\n#######################");
        sw.WriteLine("## phonems\n" + gm.introDial1 + "\n" + gm.introDial2 + "\n" + gm.introDial3 + "\n" + gm.objDial + "\n" + gm.ngpDial + "\n" + gm.fswDial);
        sw.WriteLine("## texts [string]\n" + gm.titleText + "\n" + gm.introText + "\n" + gm.objText + "\n" + gm.ngpText + "\n" + gm.fswText);
        sw.WriteLine("## musics [i1..i3 | l1..l15]\n" + gm.musicIntro + "," + gm.musicLoop);
        sw.WriteLine("## pitch [float]\n" + gm.pitch1 + "," + gm.pitch2 + "," + gm.pitch3);
        sw.WriteLine("## is mastico speaking [booleans] ?\n" + BoolToString(gm.isMastico1) + "," + BoolToString(gm.isMastico2) + "," + BoolToString(gm.isMastico3) + "," + BoolToString(gm.isZambla));
        sw.WriteLine("## obj position\n"     + (Vector2) gm.obj.GetComponent<RectTransform>().localPosition);
        sw.WriteLine("## obj size\n"         + gm.obj.GetComponent<RectTransform>().sizeDelta);
        sw.WriteLine("## objNear position\n" + (Vector2) gm.objNear.GetComponent<RectTransform>().localPosition);
        sw.WriteLine("## objNear size\n"     + gm.objNear.GetComponent<RectTransform>().sizeDelta);
        sw.WriteLine("## NGP position\n"     + (Vector2) gm.ngp.GetComponent<RectTransform>().localPosition);
        sw.WriteLine("## NGP size\n"         + gm.ngp.GetComponent<RectTransform>().sizeDelta);
        sw.WriteLine("## NgpNear position\n" + (Vector2) gm.ngpNear.GetComponent<RectTransform>().localPosition);
        sw.WriteLine("## NgpNear size\n"     + gm.ngpNear.GetComponent<RectTransform>().sizeDelta);
        sw.WriteLine("## FSW position\n"     + (Vector2) gm.fsw.GetComponent<RectTransform>().localPosition);
        sw.WriteLine("## FSW size\n"         + gm.fsw.GetComponent<RectTransform>().sizeDelta);
        sw.WriteLine("## fswNear position\n" + (Vector2) gm.fswNear.GetComponent<RectTransform>().localPosition);
        sw.WriteLine("## fswNear size\n"     + gm.fswNear.GetComponent<RectTransform>().sizeDelta);
        sw.WriteLine("~");
        // increment the scene counter
        currentScene++; 
        currentLine+=47; // one scene is 47 lines
        
        for (int i = currentScene; i<19; i++) {
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

    private static string BoolToString(bool b) {
        return (b)?"1":"0";
    }

    public static string[] LoadSceneTxt(int scene)
    {
        string path = PlayerPrefs.GetString("gamePath") + "\\";
        Debug.Log("LoadSceneTxt gamePath : "+path);
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

    public static Sprite[] LoadSceneSprites(int scene) 
    {
        Sprite[] sceneSprites = new Sprite[4];

        string path = PlayerPrefs.GetString("gamePath") + "\\";
        string spritesPath = path + "Sprites\\" + scene.ToString();

        Debug.Log("LoadSceneSprites : path : " + spritesPath);

        for (int i=1; i<5; i++)
        {
            sceneSprites[(i-1)] = LoadSprite(spritesPath + "." + i.ToString() + ".png",320,130);
        }

        return sceneSprites;
    }

    public static Sprite LoadSprite(string spritePath, int width, int height) {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(spritePath))
        {
            fileData = File.ReadAllBytes(spritePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-re the texture dimensions.
			tex.filterMode = FilterMode.Point;
			RM_TextureScale.Point(tex,width,height); // re the texture dimensions to 320*130
        }
        else {
            Debug.Log("file does not exist : " + spritePath);
        }

        Sprite sprite = new Sprite();
        sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1f);
        return sprite;
    }

    public static void LoadObject(string name, string pos, string size) {
		
		//Debug.Log("set position and size for : " + name);
		GameObject obj = GameObject.Find(name);
		RectTransform rectTransform = obj.GetComponent<RectTransform>();

		Debug.Log("## " + name + " position\n"+rectTransform.localPosition.x+","+rectTransform.localPosition.y);
		Debug.Log("## " + name + " size\n"+rectTransform.sizeDelta.x+","+rectTransform.sizeDelta.y);

		string[] positions = pos.Split(',');
		string[] sizes     = size.Split(',');
		
		rectTransform.localPosition = new Vector3(
			float.Parse(positions[0].Replace("(","")),
			float.Parse(positions[1].Replace(")","")),0f);

		rectTransform.sizeDelta = new Vector3(
			float.Parse(sizes[0].Replace("(","")),
			float.Parse(sizes[1].Replace(")","")),0f);

		Debug.Log(name + "pos : " + rectTransform.localPosition + "\n" 
				+ name + "size : " + rectTransform.sizeDelta);
	}

}
