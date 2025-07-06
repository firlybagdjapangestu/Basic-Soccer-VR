using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip audioInstruction;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetAudioInstruction(AudioClip selectedAudioClip)
    {
        audioInstruction = selectedAudioClip;
    }
    public void Intruction()
    {
        audioSource.PlayOneShot(audioInstruction);
    }
}
