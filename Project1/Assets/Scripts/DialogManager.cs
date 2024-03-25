using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Scripts
{
    /// <summary>
    /// MonoBehaviour class that rules the dialog structure
    /// </summary>
    public class DialogManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogText;

    public Button next;
    public Button prev;

    [SerializeField] public GameObject prefab;

    public Animator animator;


    private List<Question> _sentences;
    private List<GameObject> _displayedButtons = new List<GameObject>();
    private int _index = 0;

    /// <summary>
    /// Starts dialog with first sentence in the list and shows dialog box. Starts with TriggerDialog method
    /// </summary>
    /// <param name="dialog">dialog that is filled in inspector</param>
    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("isOpen", true);
        nameText.text = dialog.name;

        foreach (var sentence in dialog.sentences)
        {
            _sentences.Add(sentence);
        }

        DisplayNewSentence();
    }

    void Start()
    {
        _sentences = new List<Question>();
    }

    /// <summary>
    /// Displays the next sentence in the list
    /// </summary>
    public void DisplayNewSentence()
    {
        try
        {
            DestroyAllButtons();
            if (_index == _sentences.Count)
            {
                EndDialog();
                return;
            }

            StopAllCoroutines();
            StartCoroutine(CreateSentence(_sentences[_index]));
            if (_sentences[_index].type == QuestionType.Question)
            {
                DisplayChoices(_sentences[_index]);
            }

            ++_index;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    /// <summary>
    /// Displays the previous sentence in the list
    /// </summary>
    public void DisplayPreviousSentence()
    {
        try
        {
            DestroyAllButtons();
            if (_index == 0)
            {
                EndDialog();
                return;
            }

            _index -= 2;
            if (_index < 0)
            {
                _index = 0;
            }

            StopAllCoroutines();
            StartCoroutine(CreateSentence(_sentences[_index]));
            if (_sentences[_index].type == QuestionType.Question)
            {
                DisplayChoices(_sentences[_index]);
            }

            ++_index;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    /// <summary>
    ///  If the type of the sentence is question (with answers), shows the buttons with answers
    /// </summary>
    /// <param name="q">sentence</param>
    public void DisplayChoices(Question q)
    {
        next.interactable = false;
        prev.interactable = false;
        _displayedButtons.Clear();
        for (int i = 0; i < q.options.Count; ++i)
        {
            try
            {
                GameObject button = Instantiate(prefab, transform, true);
                button.transform.localPosition = new Vector2(130, 50 * ((q.options.Count / 2) - i) + 90);
                _displayedButtons.Add(button);
                button.GetComponentInChildren<TMP_Text>().text = q.options[i];
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    next.interactable = true;
                    prev.interactable = true;
                    DisplayNewSentence();
                });
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
    
    /// <summary>
    /// Destroys choice buttons on the screen
    /// </summary>
    public void DestroyAllButtons()
    {
        foreach (GameObject button in _displayedButtons)
        {
            Destroy(button);
        }
    } 


    /// <summary>
    /// Types the sentence with animation
    /// </summary>
    /// <param name="sentence">current sentence</param>
    /// <returns></returns>
    public IEnumerator CreateSentence(Question sentence)
    {
        dialogText.text = "";
        foreach (var letter in sentence.text.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    /// <summary>
    /// When there are no more sentences, ends the dialog and hides the dialog box
    /// </summary>
    public void EndDialog()
    {
        Debug.Log("End dialog");
        animator.SetBool("isOpen", false);
    }
}
}
