using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour
{
    [SerializeField] ResourceBank _resourceBank;
    [SerializeField] private GameResource _resource;

    private float _productionTime = 1.0f;
    
    public float ProductionTime
    {
        get => GetSpeedOfProduction();
        set => _productionTime = value;
    }
    
    public int ToAdd
    {
        get => _resourceBank.GetResource(_resourceBank.GetLevelFromResource(_resource)).Value;
    }
    private float GetSpeedOfProduction()
    {
        GameResource level = _resourceBank.GetLevelFromResource(_resource);
        return Math.Max(_productionTime * (1.01f - _resourceBank.GetResource(level).Value / 100.0f), 0.1f);
    }
    
    
    public void Increase()
    {
        // Debug.Log("checking work...");
        StartCoroutine(IncreaseCoroutine());
    }
    
    private IEnumerator IncreaseCoroutine()
    {
        yield return new WaitForSeconds(ProductionTime);
        _resourceBank.ChangeResource(_resource, ToAdd);
    }
    
}
