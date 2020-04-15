using UnityEngine;
using Assets.Pixelation.Scripts;

public class RA_Menu : MonoBehaviour {

	public int pixAcceleration = 1;
	public Pixelation pix;
	bool loading = true;
	// Use this for initialization
	void Start () {
		pix = GetComponent<Pixelation>();

	}
	
	// Update is called once per frame
	void Update () {
		if(loading && pix.BlockCount < 800)
			pix.BlockCount += (pixAcceleration * pix.BlockCount / 100);
		if (loading && pix.BlockCount >= 800) {
			loading = false;
			pix.enabled = false;

		}
	}
}
