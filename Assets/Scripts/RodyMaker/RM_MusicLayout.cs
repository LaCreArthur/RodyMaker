using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RM_MusicLayout : RM_Layout {

	public Dropdown introDropDrown; 
	public Dropdown loopDropDown; 
	public AudioSource audioSource;
	public void RM_ReturnClick(){
		Debug.Log("MusicReturn button clicked");
		SetLayouts(gm.introLayout);
		UnsetLayouts(gm.musicLayout, gm.title);
	}

	public void ListenClick(){
		Debug.Log("Listen button clicked");
		StartCoroutine(PlayMusics());
	}

	IEnumerator PlayMusics() {
		audioSource.clip = gm.sm.getMusic(gm.musicIntro);
		audioSource.Play();
		while(audioSource.isPlaying)
			yield return null;
		audioSource.clip = gm.sm.getMusic(gm.musicLoop);
		audioSource.Play();
	}

	// play and save the selected music
	public void SaveMusic(int dropDown) {
		Dropdown currentDropDown = (dropDown == 0)? introDropDrown:loopDropDown;
		string musicStr = null;
		switch (currentDropDown.value) {
			case 0: musicStr = "i1"; break;
			case 1: musicStr = "i2"; break;
			case 2: musicStr = "i3"; break;
			case 3: musicStr = "l1"; break;
			case 4: musicStr = "l2"; break;
			case 5: musicStr = "l3"; break;
			case 6: musicStr = "l4"; break;
			case 7: musicStr = "l5"; break;
			case 8: musicStr = "l6"; break;
			case 9: musicStr = "l7"; break;
			case 10: musicStr = "l8"; break;
			case 11: musicStr = "l9"; break;
			case 12: musicStr = "l10"; break;
			case 13: musicStr = "l11"; break;
			case 14: musicStr = "l12"; break;
			case 15: musicStr = "l13"; break;
			case 16: musicStr = "l14"; break;
			case 17: musicStr = "l15"; break;
			case 18: musicStr = "l2oiseaux"; break;
			case 19: musicStr = "torrent"; break;
			case 20: musicStr = "bim"; break;
			default: break;
		}

		if (dropDown == 0) {
			gm.musicIntro = musicStr;
			Debug.Log ("new intro music:  "+ gm.musicIntro);
		} 
		else {
			gm.musicLoop = musicStr;
			Debug.Log ("new loop music:  "+gm.musicLoop);
		}
		
		audioSource.clip = gm.sm.getMusic(musicStr);
		audioSource.Play();

	}

	public void SetMusic() {
		
		SetDropDown(0);
		SetDropDown(1);
	}

	private void SetDropDown(int dropDown){

		Dropdown currentDropDown = (dropDown == 0)? introDropDrown:loopDropDown;
		string musicStr = (dropDown == 0)? gm.musicIntro:gm.musicLoop;

		switch (musicStr) {
			case "i1"  : currentDropDown.value = 0; break;
			case "i2"  : currentDropDown.value = 1; break;
			case "i3"  : currentDropDown.value = 2; break;
			case "l1"  : currentDropDown.value = 3; break;
			case "l2"  : currentDropDown.value = 4; break;
			case "l3"  : currentDropDown.value = 5; break;
			case "l4"  : currentDropDown.value = 6; break;
			case "l5"  : currentDropDown.value = 7; break;
			case "l6"  : currentDropDown.value = 8; break;
			case "l7"  : currentDropDown.value = 9; break;
			case "l8"  : currentDropDown.value = 10; break;
			case "l9"  : currentDropDown.value = 11; break;
			case "l10" : currentDropDown.value = 12; break;
			case "l11" : currentDropDown.value = 13; break;
			case "l12" : currentDropDown.value = 14; break;
			case "l13" : currentDropDown.value = 15; break;
			case "l14" : currentDropDown.value = 16; break;
			case "l15" : currentDropDown.value = 17; break;
			case "l2oiseaux" : currentDropDown.value = 18; break;
			case "torrent" 	 : currentDropDown.value = 19; break;
			case "bim" 	 	 : currentDropDown.value = 20; break;
			default: break;
		}
	}
}
