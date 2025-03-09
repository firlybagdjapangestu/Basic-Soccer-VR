using System.Collections;
using UnityEngine;

public class BaseButtonVR : MonoBehaviour
{
    public AudioSource audioSource;    
    [SerializeField] private float gazeDuration = 2.0f;

    private Coroutine gazeCoroutine;

    protected static AnimationClip selectedAnimation; // Menyimpan animasi yang dipilih
    protected static Animator sharedAnimator; // Animator yang dipakai oleh semua button
    protected static AudioClip selectedAudioClip; // Menyimpan audio yang dipilih

    protected virtual void Start()
    {
        
    }

    protected virtual void OnPointerEnter()
    {
        gazeCoroutine = StartCoroutine(StartGazeTimer());
    }

    protected virtual void OnPointerExit()
    {
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine);
            gazeCoroutine = null;
        }
    }

    private IEnumerator StartGazeTimer()
    {
        yield return new WaitForSeconds(gazeDuration);
        OnGazeTrigger();
        OnPointerExit();
    }

    protected virtual void OnGazeTrigger()
    {
        // Fungsi ini akan di-override oleh button spesifik
    }

    
}
