using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Enum of different dialog types (just phrase or question)
    /// </summary>
    [Serializable]
    public enum QuestionType
    {
        [SerializeField] Text,
        [SerializeField] Question
    }
}

