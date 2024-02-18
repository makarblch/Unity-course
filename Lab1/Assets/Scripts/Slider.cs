using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ProductionBuilding _productionBuilder;
    [SerializeField] private Button _button;

    private float _start = 0.0f;
    
    public void Fill()
    {
        _button.interactable = false;
        // number of steps that will be shown on the screen
        int steps = Convert.ToInt32(_productionBuilder.ProductionTime * 100);
        // lenght of every step
        float dist = 1.0f / steps;
        StartCoroutine(FillCoroutine(steps, dist));
    }

    IEnumerator FillCoroutine(int steps, float dist)
    {
        // filling the slider
        for (int i = 0; i < steps; ++i)
        {
            _image.fillAmount = _start;
            _start += dist;
            yield return new WaitForEndOfFrame();
        }
        // restoring the primary data
        _start = 0;
        _image.fillAmount = dist;
        _button.interactable = true;
        yield return new WaitForEndOfFrame();
    }
}
