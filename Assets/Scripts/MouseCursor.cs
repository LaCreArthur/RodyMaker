using UnityEngine;


using System.Collections;

public class MouseCursor : MonoBehaviour
{
	public GameManager gm;
	public RM_GameManager rmgm;
    public Texture2D cursorTexture1_16;
    public Texture2D cursorTexture2_16;
    public Texture2D cursorTexture3_16;
    public Texture2D cursorTexture1_32;
    public Texture2D cursorTexture2_32;
    public Texture2D cursorTexture3_32;
    public Texture2D cursorTexture1_64;
    public Texture2D cursorTexture2_64;
    public Texture2D cursorTexture3_64;
    public CursorMode cursorMode = CursorMode.Auto;
    public bool onScene;
    private bool present;
    private Vector2 hotSpot;

    IEnumerator cursorAnim()
    {
        hotSpot = new Vector2(0, 0);
		while (present)
        {
            if (Screen.width < 480) // 320*200
            {
                if (onScene) hotSpot = new Vector2(0, 15);
                Cursor.SetCursor(cursorTexture1_16, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
                if (present) Cursor.SetCursor(cursorTexture2_16, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
                if (present) Cursor.SetCursor(cursorTexture3_16, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
            }
            if (Screen.width >= 480 && Screen.width < 960) // 640.400
            {
                if (onScene) hotSpot = new Vector2(0, 30);
                Cursor.SetCursor(cursorTexture1_32, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
                if (present) Cursor.SetCursor(cursorTexture2_32, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
                if (present) Cursor.SetCursor(cursorTexture3_32, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
            }
            if (Screen.width >= 960) // 1280*800
            {
                if (onScene) hotSpot = new Vector2(0, 60);
                Cursor.SetCursor(cursorTexture1_64, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
                if (present) Cursor.SetCursor(cursorTexture2_64, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
                if (present) Cursor.SetCursor(cursorTexture3_64, hotSpot, cursorMode);
                if (present) yield return new WaitForSeconds(0.3F);
            }
        }
    }

    void Update() {
        if (gm != null) {
            if (!(gm.clickIntro || gm.clickObj )) {
                Cursor.visible = false;
            }
		    else if (!Cursor.visible) Cursor.visible = true;
        }
        else if(rmgm != null) {
            Cursor.visible = true;
        }
    }

    public void OnMouseEnter()
    {
        present = true;
        StartCoroutine(cursorAnim());
        //Debug.Log ("Enter: "+gameObject.name);
    }

    public void OnMouseExit()
    {
        //Debug.Log ("Exit: "+gameObject.name);
        present = false;
    }

}
