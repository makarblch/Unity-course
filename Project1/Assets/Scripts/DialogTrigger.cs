using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Class with method TriggerDialog that starts when the start button is pushed
    /// </summary>
    public class DialogTrigger : MonoBehaviour
    {
        public Dialog dialog;

        public void TriggerDialog()
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);
        }
    }
}

