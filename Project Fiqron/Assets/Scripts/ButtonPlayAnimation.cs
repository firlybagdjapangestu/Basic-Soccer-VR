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
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(selectedAudioClip);
            sharedAnimator.Play(selectedAnimation.name);
            Debug.Log("Playing animation: " + selectedAnimation.name);
        }
        else
        {
            Debug.LogWarning("No animation selected or Animator is missing!");
        }
    }
}
