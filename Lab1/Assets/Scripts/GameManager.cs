using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        private ResourceBank _bank;

        void Start()
        {
            // Start value of game resources
            _bank.ChangeResource(GameResource.Humans, 10);
            _bank.ChangeResource(GameResource.Food, 5);
            _bank.ChangeResource(GameResource.Wood, 5);
        }
    }

}
