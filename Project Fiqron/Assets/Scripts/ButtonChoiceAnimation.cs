using UnityEngine;

public class ButtonChoiceAnimation : BaseButtonVR
{
    [SerializeField] private AnimationClip defaultAnimation;
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip audioIntruction;
    [SerializeField] private AudioClip audioExplanation;
    [SerializeField] private GameObject coach;
    [SerializeField] private Transform initialTransform;

    private Renderer _myRenderer;
    [SerializeField] private Material InactiveMaterial;
    [SerializeField] private Material GazedAtMaterial;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    protected override void Start()
    {
        base.Start();
        _myRenderer = GetComponent<Renderer>();
        sharedAnimator = animator; // Ensure the same animator is used

        // Store the initial position and rotation of the coach
        if (coach != null)
        {
            initialPosition = coach.transform.position;
            initialRotation = coach.transform.rotation;
        }
    }

    protected override void OnPointerEnter()
    {
        SetMaterial(true);
        base.OnPointerEnter();
    }

    protected override void OnPointerExit()
    {
        SetMaterial(false);
        base.OnPointerExit();
    }

    protected override void OnGazeTrigger()
    {
        SetDefaultAnimation();
        PlaySound();
        base.OnGazeTrigger();
    }

    void PlaySound()
    {
        if (audioSource != null && audioIntruction != null)
        {
            audioSource.PlayOneShot(audioIntruction);
        }
    }

    void SetDefaultAnimation()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        animator.Play(defaultAnimation.name);
        selectedAudioClip = audioExplanation; // Save the selected audio
        selectedAnimation = animationClip; // Save the selected animation
        Debug.Log("Animation selected: " + animationClip.name);

        // Reset the coach's position and rotation to the initial values
        if (coach != null)
        {
            coach.transform.position = initialPosition;
            coach.transform.rotation = initialRotation;
        }
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
