using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class FootstepsCharacter : MonoBehaviour {

	public enum StepsOn { Leaves, Wood };

	private string mainFolder = "Footsteps", leavesFolder = "Leaves", woodFolder = "Wood";
	private AudioClip[] Leaves, Wood;
	private AudioSource source;
	private AudioClip clip;

	void Start() {
		source = GetComponent<AudioSource>();
		source.playOnAwake = false;
		source.mute = false;
		source.loop = false;
		

		LoadSounds();
	}

	private void Update()
    {
		Debug.Log(Resources.LoadAll<AudioClip>(mainFolder + "/" + leavesFolder));
		
	}

	void LoadSounds() {
		Leaves = Resources.LoadAll<AudioClip>(mainFolder + "/" + leavesFolder);
		Wood = Resources.LoadAll<AudioClip>(mainFolder + "/" + woodFolder);	
	}

	public void PlayStep(StepsOn stepsOn, float volume) {
		switch (stepsOn) {
			case StepsOn.Leaves:
				clip = Leaves[1];
				break;
			case StepsOn.Wood:
				clip = Wood[Random.Range(0, Wood.Length)];
				break;
		}

		if(clip != null)
			source.PlayOneShot(clip, volume);
	}
}
