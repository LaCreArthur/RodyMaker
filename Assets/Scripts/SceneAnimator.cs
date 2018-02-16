using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimator : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	public Sprite baseFrame;
	public Sprite[] frames;
	public bool isSpeaking;

	private int fps = 5;
	int currentFrame = 0;
	bool increasing = true;

	float frameTime;
	float frameTimer = 0;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		isSpeaking = false;
		frameTime = 1 / (float)fps;
		//Debug.Log(gameObject.name + GetComponent<Transform>().position.z);
		spriteRenderer.sprite = baseFrame;
	}

	void Update()
	{
		if (isSpeaking) {
			if (frameTimer < frameTime)
			{
				frameTimer += Time.deltaTime;
			}
			else
			{
				spriteRenderer.sprite = frames[currentFrame];
				// sprite cycle = 0 1 2 1 0 ...
				currentFrame =  (currentFrame == 0)? 1 : 				// 0->1
								(currentFrame == 1 && increasing)? 2: 	// 1->2
								(currentFrame == 1 && !increasing)? 0 : // 1->0
								1; 										// 2->1
				
				if (currentFrame == 2)
					increasing = false;
				if (currentFrame == 0)
					increasing = true;

				frameTimer = 0;
			}
		}
		else {
			spriteRenderer.sprite = baseFrame;
		}
	}
}
