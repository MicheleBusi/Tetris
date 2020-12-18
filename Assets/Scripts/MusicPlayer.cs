using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] GameSettings settings = default;
    [SerializeField] List<AudioClip> playlist = default;

    AudioSource audioSource = null;

    private void Awake()
    {
        // If a music player was already in the scene, destroy this.
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        QueueNextSong();

        if (!settings.MusicOn)
        {
            audioSource.Pause();
        }
    }

    void QueueNextSong()
    {
        AudioClip song = playlist[Random.Range(0, playlist.Count - 1)];
        audioSource.clip = song;
        audioSource.Play();
        QueueNextSong(song.length - 0.1f);
    }

    IEnumerator QueueNextSong(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        AudioClip song = playlist[Random.Range(0, playlist.Count - 1)];
        audioSource.clip = song;
        audioSource.Play();
        QueueNextSong(song.length - 0.1f);
    }

    public void SoftPause()
    {
        audioSource.volumeTransition(0f, 0.5f);
        StartCoroutine(DelaySoftPause(0.5f));
    }

    public void SoftUnpause()
    {
        audioSource.volumeTransition(0.45f, 0.5f);
        audioSource.UnPause();
    }

    IEnumerator DelaySoftPause(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        audioSource.Pause();
    }
}