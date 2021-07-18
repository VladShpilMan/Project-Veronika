using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSound : MonoBehaviour
{
    private AudioClip _stepClip, _nextStepClip;
    private AudioClip[] _wood, _leaves;
    private AudioSource _sourceSteps;

    #region MONO
    public void StepsStart(InputComponent input)
    {
        input.stepSound += StepSound;

        GetReference();
        SettingSounds();
    }

    private void GetReference()
    {
        _sourceSteps = GetComponent<AudioSource>();

        _leaves = Resources.LoadAll<AudioClip>("Footsteps/Leaves");
        _wood = Resources.LoadAll<AudioClip>("Footsteps/Wood");
    }

    private void SettingSounds()
    {
        _sourceSteps.playOnAwake = false;
        _sourceSteps.mute = false;
        _sourceSteps.loop = false;
    }
    #endregion


    /*In the class Character, the tag of the surface on which the character is standing is searched 
	  using the GetTag() method and passed using the method GetStep().
	  The method PlayStep(...) takes parameters  "surface's tag" and "volume percentage". 
	  If the enum variable(stepsOn)matches, the corresponding case will be called.*/
    private void StepSound(string kind, InputComponent input)
    {
        bool noMove = false;
        if (kind != null)
        {
            switch (kind)
            {
                case "Leaves":
                    _stepClip = _leaves[0];
                    break;
                case "Wood":
                    _stepClip = _wood[0];
                    break;
            }


            if (_stepClip != _nextStepClip || noMove)
            {
                _sourceSteps.Stop();

            }

            if ((!_sourceSteps.isPlaying || _stepClip != _nextStepClip) && input.IsMove)
            {

                _sourceSteps.PlayOneShot(_stepClip, 0.8f);
                _nextStepClip = _stepClip;
                noMove = false;
            }
        }
        else _sourceSteps.Stop();
    }
}
