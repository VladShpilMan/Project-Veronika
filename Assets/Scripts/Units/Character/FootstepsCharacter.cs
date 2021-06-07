using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class FootstepsCharacter : MonoBehaviour {

	public enum StepsOn { Leaves, Wood };

	private string mainFolder = "Footsteps", leavesFolder = "Leaves", woodFolder = "Wood";
	private AudioClip[] Leaves, Wood;
	private AudioSource source;
	private AudioClip clip, nextClip;

	void Start() {
		source = GetComponent<AudioSource>();
		source.playOnAwake = false;
		source.mute = false;
		source.loop = true;
		

		LoadSounds();
		
	}

	void LoadSounds() {
		Leaves = Resources.LoadAll<AudioClip>(mainFolder + "/" + leavesFolder);
		Wood = Resources.LoadAll<AudioClip>(mainFolder + "/" + woodFolder);	
	}

	public void PlayStep(StepsOn stepsOn, float volume) {
        switch (stepsOn)
        {
            case StepsOn.Leaves:
                clip = Leaves[0];
                break;
            case StepsOn.Wood:
                clip = Wood[0];
                break;
        }

		//Debug.Log(nextClip == clip);
		if (!source.isPlaying || clip != nextClip)
		{
			source.PlayOneShot(clip, volume);
			nextClip = clip;
		}
	}
}
