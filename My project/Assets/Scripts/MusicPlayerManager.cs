using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicPlayerManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Songs")]
    public AudioClip tamilSong;
    public AudioClip englishSong;

    [Header("Dropdown")]
    public TMP_Dropdown songDropdown;

    [Header("Texts")]
    public TMP_Text musicTitle;
    public TMP_Text artistName;

    [Header("Time")]
    public TMP_Text currentTime;
    public TMP_Text totalTime;

    [Header("Slider")]
    public Slider progressSlider;

    [Header("Volume")]
    public Slider volumeSlider;
    public TMP_Text volumePercentage;

    bool isPlaying = false;
    bool isMuted = false;

    void Start()
    {
        songDropdown.onValueChanged.AddListener(ChangeSong);

        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        progressSlider.onValueChanged.AddListener(SeekMusic);

        volumeSlider.value = 1f;

        musicTitle.text = "";
        artistName.text = "";
    }

    void Update()
    {
        if (audioSource.clip != null)
        {
            progressSlider.SetValueWithoutNotify(
                audioSource.time /
                audioSource.clip.length);

            currentTime.text =
                FormatTime(audioSource.time);

            totalTime.text =
                FormatTime(audioSource.clip.length);
        }
    }

    void ChangeSong(int index)
    {
        if (index == 0)
        {
            audioSource.Stop();

            musicTitle.text = "";
            artistName.text = "";

            return;
        }

        if (index == 1)
        {
            audioSource.clip = tamilSong;

            musicTitle.text = "Tamil Song";
            artistName.text = "Anirudh";
        }

        if (index == 2)
        {
            audioSource.clip = englishSong;

            musicTitle.text = "English Song";
            artistName.text = "Ed Sheeran";
        }

        audioSource.Play();

        isPlaying = true;
    }

    public void PlayPause()
    {
        if (audioSource.clip == null)
            return;

        if (isPlaying)
            audioSource.Pause();
        else
            audioSource.Play();

        isPlaying = !isPlaying;
    }

    void SeekMusic(float value)
    {
        if (audioSource.clip == null)
            return;

        audioSource.time =
            value *
            audioSource.clip.length;
    }

    void ChangeVolume(float value)
    {
        if (!isMuted)
        {
            audioSource.volume = value;

            volumePercentage.text =
                Mathf.RoundToInt(value * 100) + "%";
        }
    }

    public void Mute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            audioSource.volume = 0;
            volumePercentage.text = "0%";
        }
        else
        {
            audioSource.volume =
                volumeSlider.value;

            volumePercentage.text =
                Mathf.RoundToInt(
                volumeSlider.value * 100)
                + "%";
        }
    }

    string FormatTime(float time)
    {
        int minutes =
            Mathf.FloorToInt(time / 60);

        int seconds =
            Mathf.FloorToInt(time % 60);

        return string.Format(
            "{0:00}:{1:00}",
            minutes,
            seconds);
    }
}