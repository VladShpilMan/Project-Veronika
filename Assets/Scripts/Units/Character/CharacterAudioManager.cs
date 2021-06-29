using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class CharacterAudioManager : MonoBehaviour {

	public enum StepsOn { Leaves, Wood }; // List of all types of surfaces

	// These variables contain the path to the folder
	private string mainFolder = "Footsteps", leavesFolder = "Leaves", woodFolder = "Wood", cutFoldet = "Weapons and hand tools"; 

	private AudioClip[] Leaves, Wood, Cut;

	private AudioSource sourceSteps, sourceCut;
	private AudioClip stepClip, nextStepClip, cut;
	//private AudioClip cutClip;

	private void Start() {
		Cut = Resources.LoadAll<AudioClip>(mainFolder + "/" + cutFoldet);
		sourceCut = GetComponent<AudioSource>();
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

	public void CutSound() {
	
	}

	/*In the class Character, the tag of the surface on which the character is standing is searched 
	  using the GetTag() method and passed using the method GetStep().
	  The method PlayStep(...) takes parameters  "surface's tag" and "volume percentage". 
	  If the enum variable(stepsOn)matches, the corresponding case will be called.*/
	public void PlayStep(StepsOn stepsOn, float volume) {
        switch (stepsOn)
        {
            case StepsOn.Leaves:
                stepClip = Leaves[0];
                break;
            case StepsOn.Wood:
                stepClip = Wood[0];
                break;
        }

        if (stepClip != nextStepClip || !(Character.IsMove && Character.IsGround)) sourceSteps.Stop();

        if ((!sourceSteps.isPlaying || stepClip != nextStepClip) && Character.IsMove && Character.IsGround)
        {
            //stepClip = Cut[0];
            sourceSteps.PlayOneShot(stepClip, volume);
            nextStepClip = stepClip;
        }
    }
}
