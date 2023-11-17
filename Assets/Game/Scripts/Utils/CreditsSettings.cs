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

        b_music.onClick.AddListener(ChangeMusicState);
        b_effects.onClick.AddListener(ChangeEffectsState);
    }

    private void OnEnable()
    {
        InicialPanelSetup();
        CheckInicialAudio();
    }

    #region Credits and Settings Panels

    private void InicialPanelSetup()
    {
        b_settings.onClick.AddListener(OpenSettings);
        b_credits.onClick.AddListener(OpenCredits);
        cg_settings.alpha = 1;
        cg_credits.alpha = 0;
        cg_credits.gameObject.SetActive(false);
    }

    private void OpenCredits()
    {
        cg_settings.DOFade(0, 0.4f).OnComplete(() => {
            cg_settings.gameObject.SetActive(false);
            cg_credits.DOFade(1, 0.4f);
            });
    }

    private void OpenSettings()
    {
        cg_credits.DOFade(0, 0.4f).OnComplete(() => {
            cg_credits.gameObject.SetActive(false);
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
        musicOn = !musicOn;
        ChangeSpritesMusic();
        _audioManager.ChangeStateMixerMusic(musicOn);
    }
    private void ChangeEffectsState()
    {
        effectsOn = !effectsOn;
        ChangeSpritesEffects();
        _audioManager.ChangeStateMixerSFX(effectsOn);
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
