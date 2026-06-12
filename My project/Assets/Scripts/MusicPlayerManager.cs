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

    [Header("Play/Pause Button")]
    public Button playPauseButton;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private bool isMuted = false;

    [Header("Mute Button")]
    public Button muteButton;

    public Sprite muteSprite;      
    public Sprite unmuteSprite;

    void Start()
    {
        songDropdown.onValueChanged.AddListener(ChangeSong);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        progressSlider.onValueChanged.AddListener(SeekMusic);

        volumeSlider.value = 1f;
        audioSource.volume = 1f;

        musicTitle.text = "";
        artistName.text = "";
        currentTime.text = "00:00";
        totalTime.text = "00:00";
        volumePercentage.text = "100%";

        playPauseButton.image.sprite = playSprite;
        muteButton.image.sprite = muteSprite;
    }

    void Update()
    {
        if (audioSource.clip != null)
        {
            progressSlider.SetValueWithoutNotify(
                audioSource.time / audioSource.clip.length
            );

            currentTime.text = FormatTime(audioSource.time);
            totalTime.text = FormatTime(audioSource.clip.length);

           
            if (!audioSource.isPlaying &&
                audioSource.time >= audioSource.clip.length)
            {
                playPauseButton.image.sprite = playSprite;
            }
        }
    }

    void ChangeSong(int index)
    {
        if (index == 0)
        {
            audioSource.Stop();

            musicTitle.text = "";
            artistName.text = "";

            progressSlider.value = 0;
            currentTime.text = "00:00";
            totalTime.text = "00:00";

            playPauseButton.image.sprite = playSprite;

            return;
        }

        if (index == 1)
        {
            audioSource.clip = tamilSong;
            musicTitle.text = "Tamil Song";
            artistName.text = "Anirudh";
        }
        else if (index == 2)
        {
            audioSource.clip = englishSong;
            musicTitle.text = "English Song";
            artistName.text = "Ed Sheeran";
        }

        audioSource.Play();

        playPauseButton.image.sprite = pauseSprite;
    }

    public void PlayPause()
    {
        if (audioSource.clip == null)
            return;

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            playPauseButton.image.sprite = playSprite;
        }
        else
        {
            audioSource.Play();
            playPauseButton.image.sprite = pauseSprite;
        }
    }

    void SeekMusic(float value)
    {
        if (audioSource.clip == null)
            return;

        audioSource.time = value * audioSource.clip.length;
    }

    void ChangeVolume(float value)
    {
        if (!isMuted)
        {
            audioSource.volume = value;
            volumePercentage.text = Mathf.RoundToInt(value * 100) + "%";
        }
    }

    public void Mute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            audioSource.volume = 0f;

            volumePercentage.text = "0%";

            muteButton.image.sprite = unmuteSprite;
        }
        else
        {
            audioSource.volume = volumeSlider.value;

            volumePercentage.text =
                Mathf.RoundToInt(volumeSlider.value * 100) + "%";

            muteButton.image.sprite = muteSprite;
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


