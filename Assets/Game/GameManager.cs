using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public bool recording = true;
	private bool paused = false;
	private float initialFixedDeltaTime;


	void Start(){
		initialFixedDeltaTime = Time.fixedDeltaTime;
	}

	// Update is called once per frame
	void Update ()
	{
		if (CrossPlatformInputManager.GetButton ("Fire1")) {
			recording = false;
		} else {
			recording = true;
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			if (!paused) {
				PauseGame ();
			} else {
				ResumeGame ();
			}
		}
	}

	void PauseGame(){
		Time.timeScale = 0;
		Time.fixedDeltaTime = 0;
		paused = true;
	}

	void ResumeGame(){
		Time.timeScale = 1f;
		Time.fixedDeltaTime = initialFixedDeltaTime;
		paused = false;
	}

}
