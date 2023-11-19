using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsSettings : MonoBehaviour
{
    [Header("Change between panels")]
    [SerializeField] private Button b_settings;
    [SerializeField] private Button b_credits;
    private Outline outline_bSettings;
    private Outline outline_bCredits;
    [SerializeField] private CanvasGroup cg_settings;
    [SerializeField] private CanvasGroup cg_credits;

    [Header("Change audio settings")]
    [SerializeField] Button b_music;
    [SerializeField] Image musicIcon;
    [SerializeField] Sprite sprite_musicOff;
    private Sprite sprite_musicOn;
    bool musicOn;

    [SerializeField] Button b_effects;
    [SerializeField] Image effectsIcon;
    [SerializeField] Sprite sprite_effectsOff;
    private Sprite sprite_effectsOn;
    bool effectsOn;

    private AudioManager _audioManager => AudioManager.I;
    private const string keyMixerEffects = "Sfx";
    private const string keyMixerMusic = "Music";

    private void Awake()
    {
        sprite_musicOn = musicIcon.sprite;
        sprite_effectsOn = effectsIcon.sprite;

        outline_bSettings = b_settings.GetComponent<Outline>();
        outline_bCredits = b_credits.GetComponent<Outline>();

        b_music.onClick.AddListener(ChangeMusicState);
        b_effects.onClick.AddListener(ChangeEffectsState);

        b_settings.onClick.AddListener(OpenSettings);
        b_credits.onClick.AddListener(OpenCredits);
    }

    private void OnEnable()
    {
        InicialPanelSetup();
        CheckInicialAudio();
    }

    #region Credits and Settings Panels

    private void InicialPanelSetup()
    {
        b_settings.interactable = false;
        b_credits.interactable = true;
        outline_bSettings.enabled = true;
        outline_bCredits.enabled = false;
        cg_settings.alpha = 1;
        cg_credits.alpha = 0;
        cg_settings.gameObject.SetActive(true);
        cg_credits.gameObject.SetActive(false);
    }

    private void OpenCredits()
    {
        _audioManager.PlaySfx("ButtonClick");
        outline_bSettings.enabled = false;
        outline_bCredits.enabled = true;
        b_credits.interactable = false;
        cg_settings.DOFade(0, 0.4f).OnComplete(() =>
        {
            b_settings.interactable = true;
            cg_settings.gameObject.SetActive(false);
            cg_credits.gameObject.SetActive(true);
            cg_credits.DOFade(1, 0.4f);
        });
    }

    private void OpenSettings()
    {
        _audioManager.PlaySfx("ButtonClick");
        outline_bSettings.enabled = true;
        outline_bCredits.enabled = false;
        b_settings.interactable = false;
        cg_credits.DOFade(0, 0.4f).OnComplete(() =>
        {
            b_credits.interactable = true;
            cg_credits.gameObject.SetActive(false);
            cg_settings.gameObject.SetActive(true);
            cg_settings.DOFade(1, 0.4f);
        });
    }


    #endregion

    #region Audio Changes

    private void CheckInicialAudio()
    {
        musicOn = (PlayerPrefs.HasKey(keyMixerMusic) ? (PlayerPrefs.GetInt(keyMixerMusic) == 0 ? false : true) : true);
        effectsOn = (PlayerPrefs.HasKey(keyMixerEffects) ? (PlayerPrefs.GetInt(keyMixerEffects) == 0 ? false : true) : true);

        ChangeSpritesMusic();
        ChangeSpritesEffects();
        
    }

    private void ChangeMusicState()
    {
        _audioManager.PlaySfx("ButtonClick");
        musicOn = !musicOn;
        ChangeSpritesMusic();
        _audioManager.ChangeStateMixerMusic(musicOn);
    }
    private void ChangeEffectsState()
    {
        effectsOn = !effectsOn;
        ChangeSpritesEffects();
        _audioManager.ChangeStateMixerSFX(effectsOn);
        if (effectsOn) { _audioManager.PlaySfx("ButtonClick"); }
    }

    private void ChangeSpritesMusic()
    {
        if (musicOn)
        {
            musicIcon.sprite = sprite_musicOn;
        }
        else
        {
            musicIcon.sprite = sprite_musicOff;
        }
    }
    private void ChangeSpritesEffects()
    {
        if (effectsOn)
        {
            effectsIcon.sprite = sprite_effectsOn;
        }
        else
        {
            effectsIcon.sprite = sprite_effectsOff;
        }
    }

    #endregion
}
