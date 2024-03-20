using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField] private List<CardLayout> layouts;
        [SerializeField] private List<CardAsset> assets;
        [SerializeField] private int handCapacity;
        [SerializeField] private CardLayout centerLayout;
        [SerializeField] private CardLayout discardLayout;

        private void Start()
        {
            // Possible to add more players
            for (int i = 0; i < layouts.Count; ++i)
            {
                layouts[i].LayoutId = i;
            }

            int count = layouts.Count;
            
            discardLayout.LayoutId = count;
            centerLayout.LayoutId = count + 1;


            CardGame.Instance.Init(layouts, assets, handCapacity, centerLayout, discardLayout);
        }

        // Начинает ход
        public void StartTurn()
        {
            CardGame.Instance.StartTurn();
        }
    }
}