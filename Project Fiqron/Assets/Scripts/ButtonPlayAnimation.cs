using UnityEngine;

public class ButtonPlayAnimation : BaseButtonVR
{

    protected override void OnGazeTrigger()
    {
        PlaySelectedAnimation();
        base.OnGazeTrigger();
    }

    void PlaySelectedAnimation()
    {
        if (sharedAnimator != null && selectedAnimation != null)
        {
            sharedAnimator.Play(selectedAnimation.name);
            audioSource.PlayOneShot(selectedAudioClip);
            Debug.Log("Playing animation: " + selectedAnimation.name);
        }
        else
        {
            Debug.LogWarning("No animation selected or Animator is missing!");
        }
    }

    void PlaySound()
    {
        if (audioSource != null && selectedAudioClip != null)
        {
            audioSource.PlayOneShot(selectedAudioClip);
            Debug.Log("Playing sound: " + selectedAudioClip.name);
        }
        else
        {
            Debug.LogWarning("No audio clip selected or AudioSource is missing!");
        }
    }
}
