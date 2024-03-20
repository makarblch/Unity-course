using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardGame
    {
        private static CardGame _instance;
        private static int _number = 0;
        
        public static CardGame Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CardGame();
                }

                return _instance;
            }
        }

        public Dictionary<CardInstance, CardView> CardsDictionary = new Dictionary<CardInstance, CardView>();
        private List<CardAsset> _cardAssets = new List<CardAsset>();
        public List<CardLayout> Layouts = new();
        private int _handCapacity;
        private CardLayout _discardLayout;
        public CardLayout CenterLayout;
        
        public void Init(List<CardLayout> layouts, List<CardAsset> assets, int capacity, CardLayout center, CardLayout discard)
        {
            Layouts = layouts;
            _cardAssets = assets;
            _handCapacity = capacity;
            CenterLayout = center;
            _discardLayout = discard;
            StartGame();
        }


        public void StartGame()
        {
            foreach (var asset in _cardAssets)
            {
                foreach (var layout in Layouts)
                {
                    CreateCard(asset, layout.LayoutId);
                }
            }
        }

        public void ShuffleLayout(int layoutId)
        {
            var cards = GetInstancesInLayout(layoutId);
            System.Random rnd = new System.Random();
            for (int i = 0; i < Layouts[layoutId].CardsNumber; ++i)
            {
                var rand = rnd.Next(0, cards.Count-1);
                (cards[i], cards[rand]) = (cards[rand], cards[i]);
                CardsDictionary[cards[i]].transform.SetSiblingIndex(rand);
            }
        }
        

        public void StartTurn()
        {
            foreach (var layout in Layouts)
            {
                ShuffleLayout(layout.LayoutId);
                layout.FaceUp = true;
                
                var cards = GetCardsInLayout(layout.LayoutId);
                System.Console.WriteLine(cards.Count);
                for (int i = 0; i < _handCapacity; ++i)
                {
                    // Turning card face up
                    cards[i].Status = 1;
                }
            }
        }

        private void CreateCardView(CardInstance cardInstance)
        {
            GameObject newCardInstance = new GameObject($"Card {++_number}");
            
            CardView view = newCardInstance.AddComponent<CardView>();
            Image image = newCardInstance.AddComponent<Image>();
            
            view.Init(cardInstance, image);

            Button button = newCardInstance.AddComponent<Button>();
            button.onClick.AddListener(view.PlayCard);
            newCardInstance.transform.SetParent(Layouts[cardInstance.LayoutId].transform);

            CardsDictionary[cardInstance] = view;
        } 
        private void CreateCard(CardAsset asset, int layoutNumber)
        {
            var instance = new CardInstance(asset);
            instance.LayoutId = layoutNumber;
            Layouts[layoutNumber].CardsNumber++;
            instance.CardPosition = Layouts[layoutNumber].CardsNumber;
            CreateCardView(instance);
        }

        private List<CardView> GetCardsInLayout(int layoutId)
        {
            return _instance.CardsDictionary.Where(x => x.Key.LayoutId == layoutId).Select(x => x.Value).ToList();
        }
        
        private List<CardInstance> GetInstancesInLayout(int layoutId)
        {
            return _instance.CardsDictionary.Where(x => x.Key.LayoutId == layoutId).Select(x => x.Key).ToList();
        }

        public void RecalculateLayout(int layoutId)
        {
            var cards = GetInstancesInLayout(layoutId);
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].CardPosition = i;
            }
        }
        
        public void MoveToCenter(CardInstance card)
        {
            int temp = card.LayoutId;
            card.LayoutId = CenterLayout.LayoutId;
            CardsDictionary[card].transform.SetParent(CenterLayout.transform);
            RecalculateLayout(CenterLayout.LayoutId);
            RecalculateLayout(temp);
        }
        
        public void MoveToDiscard(CardInstance card)
        {
            int temp = card.LayoutId;
            card.LayoutId = _discardLayout.LayoutId;
            // Set new parent to the discarding card
            CardsDictionary[card].transform.SetParent(_discardLayout.transform);
            // Pushing the card is no longer available
            try
            {
                Button button = CardsDictionary[card].GetComponent<Button>();
                button.enabled = false;
                button.onClick.RemoveAllListeners();
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
            RecalculateLayout(_discardLayout.LayoutId);
            RecalculateLayout(temp);
        }
    }
}

