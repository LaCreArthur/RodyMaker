using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BtnConfirm : MonoBehaviour
{

    public SynthManager synth;
    public void OnClick()
    {
		if (synth.dialLayoutScript.isDial) {
            synth.dialLayoutScript.pitch   = (float) Math.Round(synth.pitchSlider.value,1);
            synth.dialLayoutScript.phonems = synth.input.text;
        }
		if (synth.objLayoutScript.isObj) {
            synth.objLayoutScript.phonems = synth.input.text;
        }
        
        SceneManager.UnloadSceneAsync(7);
    }
}
