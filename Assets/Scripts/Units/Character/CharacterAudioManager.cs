using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class CharacterAudioManager : MonoBehaviour {

	public enum StepsOn { Leaves, Wood }; // List of all types of surfaces

	private string mainFolder = "Footsteps", leavesFolder = "Leaves", woodFolder = "Wood"; // These variables contain the path to the folder
	private AudioClip[] Leaves, Wood;
	private AudioSource source;
	private AudioClip clip, nextClip;

	void Start() {
		source = GetComponent<AudioSource>();
		source.playOnAwake = false;
		source.mute = false;
		source.loop = false;
		

		LoadStepsSounds();		
	}

	void LoadStepsSounds() {
		Leaves = Resources.LoadAll<AudioClip>(mainFolder + "/" + leavesFolder);
		Wood = Resources.LoadAll<AudioClip>(mainFolder + "/" + woodFolder);	
	}

	/*In the class Character, the tag of the surface on which the character is standing is searched 
	  using the GetTag() method and passed using the method GetStep().
	  The method PlayStep(...) takes parameters  "surface's tag" and "volume percentage". 
	  If the enum variable(stepsOn)matches, the corresponding case will be called.*/
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

		if(clip != nextClip || !(Character.IsMove && Character.IsGround)) source.Stop();

		if ((!source.isPlaying || clip != nextClip) && Character.IsMove && Character.IsGround) {
			source.PlayOneShot(clip, volume);
			nextClip = clip;
		}
	}
}
