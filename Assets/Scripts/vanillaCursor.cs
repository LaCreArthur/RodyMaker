using UnityEngine;
using System.Collections;

public class vanillaCursor : MonoBehaviour {

	public Texture2D cursorTexture_16;
    public Texture2D cursorTexture_32;
    public Texture2D cursorTexture_64;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot;

	void Start () {
		hotSpot = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Screen.width < 480) // 320*200
		{
			Cursor.SetCursor(cursorTexture_16, hotSpot, cursorMode);
		}
		if (Screen.width >= 480 && Screen.width < 960) // 640.400
		{
			Cursor.SetCursor(cursorTexture_32, hotSpot, cursorMode);
		}
		if (Screen.width >= 960) // 1280*800
		{
			Cursor.SetCursor(cursorTexture_64, hotSpot, cursorMode);
		}
	}
}
