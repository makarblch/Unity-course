using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// A class with NPC name and sentences that are given in inspector
    /// </summary>
    [Serializable]
    public class Dialog
    {
        public string name;
        [SerializeField] public List<Question> sentences;
    }
}

