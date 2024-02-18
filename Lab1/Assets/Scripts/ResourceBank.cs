using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBank : MonoBehaviour
{
    private Dictionary<GameResource, ObservableInt> _resourceBank = new();

    public ResourceBank()
    {
        _resourceBank.Add(GameResource.Humans, new ObservableInt(0));
        _resourceBank.Add(GameResource.Food, new ObservableInt(0));
        _resourceBank.Add(GameResource.Wood, new ObservableInt(0));
        _resourceBank.Add(GameResource.Stone, new ObservableInt(0));
        _resourceBank.Add(GameResource.Gold, new ObservableInt(0));
        _resourceBank.Add(GameResource.HumansProdLvl, new ObservableInt(1));
        _resourceBank.Add(GameResource.FoodProdLvl, new ObservableInt(1));
        _resourceBank.Add(GameResource.WoodProdLvl, new ObservableInt(1));
        _resourceBank.Add(GameResource.StoneProdLvl, new ObservableInt(1));
        _resourceBank.Add(GameResource.GoldProdLvl, new ObservableInt(1));
    }

    public GameResource GetLevelFromResource(GameResource resource)
    {
        switch (resource)
        {
            case GameResource.Food:
                return GameResource.FoodProdLvl;
            case GameResource.Gold:
                return GameResource.GoldProdLvl;
            case GameResource.Humans:
                return GameResource.HumansProdLvl;
            case GameResource.Wood:
                return GameResource.WoodProdLvl;
            case GameResource.Stone:
                return GameResource.StoneProdLvl;
            default:
                return GameResource.HumansProdLvl;
        }
    }

    public void ChangeResource(GameResource resource, int value)
    {
        _resourceBank[resource].Value += value;
    }

    public ObservableInt GetResource(GameResource resource) => _resourceBank[resource];
}