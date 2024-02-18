using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameResource _resource;
    [SerializeField] private ResourceBank _bank;
    [SerializeField] private Button _button;

    private int _price = 10;

    public void Upgrade()
    {
        var value = _bank.GetResource(_resource).Value;
        if (value >= _price)
        {
            _bank.GetResource(_resource).Value -= _price;
            _bank.GetResource(_bank.GetLevelFromResource(_resource)).Value += 1;
            Fill(Color.green);
        }
        else
        {
            Fill(Color.red);
        }
    }

    private void Fill(Color color)
    {
        _button.interactable = false;
        // number of steps that will be shown on the screen
        int steps = Convert.ToInt32(100);
        // lenght of every step
        float dist = 1.0f / steps;
        StartCoroutine(FillCoroutine(steps, dist, color));
    }

    private IEnumerator FillCoroutine(int steps, float dist, Color color)
    {
        float start = 0.0f;
        // filling the slider
        _button.image.color = color;

        for (int i = 0; i < steps; ++i)
        {
            _button.image.fillAmount = start;
            start += dist;
            yield return new WaitForEndOfFrame();
        }
        
        _button.interactable = true;
        _button.image.color = Color.white;
        _button.image.fillAmount = 1;
        yield return new WaitForEndOfFrame();
    }
}