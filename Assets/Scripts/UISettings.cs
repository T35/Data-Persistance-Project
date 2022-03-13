using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour {
    private GameManager _gm;

    [SerializeField] private GameObject ballAccelerateSlider;
    [SerializeField] private GameObject ballMaxVelocitySlider;
    [SerializeField] private GameObject ballStartForceSlider;
    [SerializeField] private GameObject paddleSpeedSlider;
    
    private void Awake() {
        _gm = GameManager.Instance;
    }

    private void Start() {
        InitMenus();
    }

    private void InitMenus() {
        AdjustSliderMenuFloat(ballAccelerateSlider, _gm.data.settings.ballAccelerate);
        AdjustSliderMenuFloat(ballMaxVelocitySlider, _gm.data.settings.ballMaxVelocity);
        AdjustSliderMenuFloat(ballStartForceSlider, _gm.data.settings.ballStartForce);
        AdjustSliderMenuFloat(paddleSpeedSlider, _gm.data.settings.paddleSpeed);
    }

    public void GoToPreviousScene() {
        _gm.scenes.LoadPreviousScene();
    }
    
    public void ExitGame() {
        _gm.ExitGame();
    }

    public void SaveSettings() {
        _gm.data.SaveData();
    }

    public void SetDefaultSettings() {
        _gm.data.settings = Data.SettingsClass.GetDefault();
        SaveSettings();
        InitMenus();
    }

    private float AdjustSliderMenuFloat(GameObject sliderMenu, float newValueParam = 0) {
        float newValue = newValueParam == 0 ? (float)Math.Round(sliderMenu.GetComponentInChildren<Slider>().value, 2) : newValueParam;
        sliderMenu.GetComponentInChildren<TMP_Text>().text = $"{newValue}";
        sliderMenu.GetComponentInChildren<Slider>().value = newValue;
        return newValue;
    }
    
    public void AdjustBallAccelerate() {
        _gm.data.settings.ballAccelerate = AdjustSliderMenuFloat(ballAccelerateSlider);
    }

    public void AdjustBallMaxVelocity() {
        _gm.data.settings.ballMaxVelocity = AdjustSliderMenuFloat(ballMaxVelocitySlider);
    }
    
    public void AdjustBallStartForce(float newValueParam = 0) {
        _gm.data.settings.ballStartForce = AdjustSliderMenuFloat(ballStartForceSlider);
    }
    
    public void AdjustPaddleSpeed(float newValueParam = 0) {
        _gm.data.settings.paddleSpeed = AdjustSliderMenuFloat(paddleSpeedSlider);
    }
}
