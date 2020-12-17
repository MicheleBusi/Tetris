using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Invoke("PlayNextSong", song.length);
    }
}