using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene : MonoBehaviour {

	public GameManager gm;

	private string txt;
	private List<int> dial;
	private AudioClip[] fx;
	private bool isFounded = false;

	public void Play(int step) {
		initStep(step);
		StartCoroutine(Sequence());
		StartCoroutine(End(step));
	}

	IEnumerator Sequence() {

		yield return new WaitForSeconds(0.2f);
		// remove non unsable buttons
		gm.p_text.SetActive(false);
		gm.b_draw.SetActive(false);
		gm.b_next.SetActive(false);
		gm.b_ngp.SetActive(false);
		gm.b_fsw.SetActive(false);
		gm.b_repeat.SetActive(true);

		// Set the object text
		gm.p_text.GetComponent<UnityEngine.UI.Text>().text = txt;

		yield return new WaitForSeconds(0.2f);
		// display it
		gm.p_text.SetActive(true);
		yield return new WaitForSeconds(0.2f);

		// play a start object fx
		gm.sm.RandomSound(fx);
		
		while(gm.sm.soundSource.isPlaying) 
			yield return null;
		
		StartCoroutine(dialog());

	}

	public IEnumerator dialog() {
		// play the obj dialog and an ending fx
		StartCoroutine(gm.sm.MasticoSpeak(dial, true));

		while(gm.MasticoAnimator.GetBool("isSpeaking"))
			yield return null;

		gm.sm.RandomSound(gm.sm.sounds_fx_fin);
		while(gm.sm.soundSource.isPlaying) 
			yield return null;

		yield return new WaitForSeconds(0.2f);

		// Start object research
		gm.clickObj = true;
		gm.interObj = false;
	}

	IEnumerator End(int step) {
		// wait until object is founded
		while (!isFounded)
			yield return null;
		gm.b_draw.SetActive(true);
		gm.b_next.SetActive(true);
		gm.b_repeat.SetActive(false);
		switch (step) {
			case 1: // obj
				// Debug.Log(step);
				gm.b_ngp.SetActive(true);
				gm.obj.SetActive(false);
				break;
			case 2: // ngp
				gm.b_fsw.SetActive(true);
				gm.ngp.SetActive(false);
				break;
			case 3: // fsw
				gm.fsw.SetActive(false);
				break;
			default: break;
		}
		gm.clickObj = true;
		isFounded = false;
		gm.interObj = true;
	}

// init the scene variables
	public void initStep(int step) {
		switch(step) {
			case 1: 
				txt  = gm.objText;
				dial = gm.getDial(3);
				fx   = gm.sm.sounds_fx_debutObj;
				break;
			case 2:
				txt  = gm.ngpText;
				dial = gm.getDial(4);
				fx   = gm.sm.sounds_fx_debutNgp;
				break;
			case 3:
				txt  = gm.fswText;
				dial = gm.getDial(5);
				fx   = gm.sm.sounds_non;
				break;
			default: 
				txt  = "scene switcher glitch";
				dial = new List<int> { P.g, P.l, P.i, P.t, P.ch};
				fx   = gm.sm.sounds_non;
				break;
		}
	}

	public void Founded() {
		if (gm.clickObj) {
			gm.clickObj = false;
			Debug.Log("BRAVO !");
			StartCoroutine(oui());
		}
	}

	public void Near() {
		if (gm.clickObj)
			gm.clickObj = false;
			Debug.Log("CEST PRESQUE CA !");
			StartCoroutine(presque());
	}

	public void Miss() {
		if (gm.clickObj && !gm.interObj) {
			gm.clickObj = false;
			Debug.Log("NON RECOMMENCE !");
			StartCoroutine(non());
		}
	}

	IEnumerator oui() {
		gm.MasticoAnimator.SetTrigger("Process");
		yield return new WaitForSeconds(gm.MasticoAnimator.GetCurrentAnimatorClipInfo(0).Length);

		gm.MasticoAnimator.SetTrigger("Oui");
		
		gm.sm.RandomSound(gm.sm.sounds_oui);
		while(gm.sm.soundSource.isPlaying) 
			yield return null;
		
		StartCoroutine(gm.sm.MasticoSpeak(gm.sm.RandomOui(), false));
		
		while(gm.MasticoAnimator.GetBool("isSpeaking"))
			yield return null;

		isFounded = true;
	}

	IEnumerator presque() {
		gm.MasticoAnimator.SetTrigger("Process");
		yield return new WaitForSeconds(gm.MasticoAnimator.GetCurrentAnimatorClipInfo(0).Length);
		
		gm.sm.RandomSound(gm.sm.sounds_presque);
		while(gm.sm.soundSource.isPlaying) 
			yield return null;

		StartCoroutine(gm.sm.MasticoSpeak(gm.sm.RandomPresque(), false));
		yield return new WaitForSeconds(1f);

		gm.clickObj = true;
	}

	IEnumerator non() {
		gm.MasticoAnimator.SetTrigger("Process");
		yield return new WaitForSeconds(gm.MasticoAnimator.GetCurrentAnimatorClipInfo(0).Length);

		gm.MasticoAnimator.SetTrigger("Non");
		
		gm.sm.RandomSound(gm.sm.sounds_non);
		while(gm.sm.soundSource.isPlaying) 
			yield return null;
		
		StartCoroutine(gm.sm.MasticoSpeak(gm.sm.RandomNon(), false));
		yield return new WaitForSeconds(1f);
		gm.clickObj = true;
	}


}