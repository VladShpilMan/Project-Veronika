using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class CharacterAudioManager : MonoBehaviour {

	public enum StepsOn { Leaves, Wood }; // List of all types of surfaces

	private string mainFolder = "Footsteps", leavesFolder = "Leaves", woodFolder = "Wood"; // These variables contain the path to the folder
	private AudioClip[] Leaves, Wood, Cut;

	private AudioSource sourceSteps, sourceCut;
	private AudioClip clip, nextClip;

	private void Start() {

		SettingFootstepSounds();		
	}


	private void SettingFootstepSounds() {
		sourceSteps = GetComponent<AudioSource>();

		Leaves = Resources.LoadAll<AudioClip>(mainFolder + "/" + leavesFolder);
		Wood = Resources.LoadAll<AudioClip>(mainFolder + "/" + woodFolder);

	
		sourceSteps.playOnAwake = false;
		sourceSteps.mute = false;
		sourceSteps.loop = false;
	}

	private void CutSound() {

		if (Input.GetButtonDown("Fire1") && AttackCharacter.IsCombatMode)
        {
			
		}
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

		if(clip != nextClip || !(Character.IsMove && Character.IsGround)) sourceSteps.Stop();

		if ((!sourceSteps.isPlaying || clip != nextClip) && Character.IsMove && Character.IsGround) {
			sourceSteps.PlayOneShot(clip, volume);
			nextClip = clip;
		}
	}
}
