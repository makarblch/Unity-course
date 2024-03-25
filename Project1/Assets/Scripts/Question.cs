using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// A class that contains a replica of the dialog, which can be just text or question with answers
    /// </summary>
    [Serializable]
    public class Question
    {
        public string text;
        public QuestionType type;
        public List<String> options;
    }
}
