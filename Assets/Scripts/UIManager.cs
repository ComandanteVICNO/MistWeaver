using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenu;
    public GameObject healthUI;
    public GameObject energyUI;
    public GameObject dialogUI;

    [Header("Options References")]
    public Slider masterVolumeSlider;
    public Slider musicSlider;
    public Slider soundFxSlider;
    public AudioMixer gameAudioMixer;

    [Header ("Player Prefs Strings")]
    

    [Header("Options Values")]
    public float masterVolume;
    public float musicVolume;
    public float soundfxVolume;

    [Header("PostFx")]
    public FilmGrain filmGrain;
    public Vignette vignette;
    public VolumeProfile gameVolume;


    bool isPaused;

    private void Awake()
    {
        SetUiVisibility();
        GetPostFxFromVolume();


    }
    void Start()
    {
        
        isPaused = false;
        GetVolumePlayerPrefs();
        GetPostFxPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPauseButton();
    }

    void SetUiVisibility()
    {
        pauseMenu.SetActive(false); 
        healthUI.SetActive(true);
        energyUI.SetActive(true);
    }
    #region Audio
    void GetVolumePlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            SetMasterVolume(masterVolume);
            masterVolumeSlider.value = masterVolume;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(musicVolume);
            musicSlider.value = musicVolume;
        }

        if (PlayerPrefs.HasKey("FxVolume"))
        {
            soundfxVolume = PlayerPrefs.GetFloat("FxVolume");
            SetSoundFxVolume(soundfxVolume);
            soundFxSlider.value = soundfxVolume;
        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        gameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", (float)sliderValue);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float sliderValue)
    {
        gameAudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", (float)sliderValue);
        PlayerPrefs.Save();
    }

    public void SetSoundFxVolume(float sliderValue)
    {
        gameAudioMixer.SetFloat("FxVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("FxVolume", (float)sliderValue);
        PlayerPrefs.Save();
    }
    #endregion

    #region PostFX
    public void GetPostFxPlayerPrefs()
    {
        //Check for Filmgrain
        if (PlayerPrefs.HasKey("FilmGrainEnabled"))
        {

            int filmGrainEnabled = PlayerPrefs.GetInt("FilmGrainEnabled");
            if (filmGrainEnabled == 0)
            {
                filmGrain.active = false;
            }
            else if (filmGrainEnabled == 1)
            {
                filmGrain.active = true;
            }
        }

        //check for vignette
        if (PlayerPrefs.HasKey("Vignette"))
        {

            int vignetteEnabled = PlayerPrefs.GetInt("Vignette");
            if (vignetteEnabled == 0)
            {
                vignette.active = false;
            }
            else if (vignetteEnabled == 1)
            {
                vignette.active = true;
            }
        }
    }

    public void GetPostFxFromVolume()
    {
        //filmgrain
        FilmGrain globalFilmGrain;
        if (gameVolume.TryGet<FilmGrain>(out globalFilmGrain))
        {
            filmGrain = globalFilmGrain;
        }
        //vignette
        Vignette globalVignette;
        if(gameVolume.TryGet<Vignette>(out globalVignette))
        {
            vignette = globalVignette;
        }
    }
    #endregion
    void CheckForPauseButton()
    {
        if (UserInput.instance.controls.Player.Pause.WasPressedThisFrame())
        {
            if (!isPaused)
            {
                isPaused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
    public void ResumeButton()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }

}
