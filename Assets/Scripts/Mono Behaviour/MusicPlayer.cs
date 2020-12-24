using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] GameEvent musicPaused = default;
    [SerializeField] GameEvent musicUnpaused = default;
    [SerializeField] BoolVariable musicToggle = default;

    AudioSource audioSource = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        musicPaused.RegisterListener(SoftPause);
        musicUnpaused.RegisterListener(SoftUnpause);
    }

    private void Start()
    {
        audioSource.Play();
        if (!musicToggle.Value)
        {
            audioSource.Pause();
        }
    }

    public void SoftPause()
    {
        audioSource.volumeTransition(0f, 0.5f);
        this.Wait(0.5f, audioSource.Pause);
    }

    public void SoftUnpause()
    {
        audioSource.UnPause();
        audioSource.volumeTransition(1f, 0.5f);
    }
}