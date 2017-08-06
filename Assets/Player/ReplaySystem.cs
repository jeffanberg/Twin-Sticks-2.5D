using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 500;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
	private GameManager gameManager;

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameManager.recording) {
			Record ();
		} else {
			PlayBack ();
		}
	}

	void Record ()
	{
		rigidBody.isKinematic = false;
		int frame = Time.frameCount % bufferFrames;
		float time = Time.time;
		print ("Writing frame " + frame);
		keyFrames [frame] = new MyKeyFrame (time, transform.position, transform.rotation);
	}

	void PlayBack ()
	{
		rigidBody.isKinematic = true;
		//int frame = Time.frameCount % bufferFrames;
		int frameCount = 0;
		foreach (MyKeyFrame keyFrame in keyFrames) {
			if (keyFrame.frameTime > 0) {
				frameCount++;
			}
		}
		int frame = Time.frameCount % frameCount;
		print ("Reading frame " + frame);
		transform.position = keyFrames [frame].position;
		transform.rotation = keyFrames [frame].rotation;

		//TODO Return to end state before play back instead of current recorded frame at button release.
	}

}

/// <summary>
/// A structure for storing time, rotation, and position.
/// </summary>
public struct MyKeyFrame {

	public float frameTime;
	public Vector3 position;
	public Quaternion rotation;

	public MyKeyFrame (float time, Vector3 pos, Quaternion rot)
	{
		frameTime = time;
		position = pos;
		rotation = rot;
	} 

}