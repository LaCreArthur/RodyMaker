using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RA_FeedbackPanelScript : MonoBehaviour {

	
	public RA_SoundManager sm;

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		sm.OnFeedbackDisabled();
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		sm.OnFeedbackEnabled();
	}

	void Update () {
		
	}
}
