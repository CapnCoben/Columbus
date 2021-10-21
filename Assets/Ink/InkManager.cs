using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InkManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    [SerializeField]
    private VerticalLayoutGroup _choiceButtonContainer;

    [SerializeField]
    private Button _choiceButtonPrefab;
    [SerializeField]
    private TextAsset _inkJsonAsset;
    private Story _story;
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private TMP_Text _textField;
    [SerializeField]
    private TMP_Text textPrefab =null;
    [SerializeField]
    private Color _normalTextColor;
    [SerializeField]
    private Color _speechTextColor;

    public Button _closeButton;

    // Start is called before the first frame update
    void Start()
    {
        StartStory();
        _closeButton.enabled = false;
    }

    // Update is called once per frame
    public void StartStory()
    {
        _story = new Story(_inkJsonAsset.text);
        if (OnCreateStory != null) OnCreateStory(_story);
        RefreshView();
 
    }
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (_story.canContinue)
        {
            // Continue gets the next line of the story
            string text = _story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            DisplayNextLine();
        }
        void RemoveChildren()
        {
            int childCount = canvas.transform.childCount;
            for (int i = childCount - 1; i >= 0; --i)
            {
                GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
            }
        }
    }
    private void ApplyStyling()
    {
        if (_story.currentTags.Contains("speech"))
        {
            _textField.color = _speechTextColor;
            _textField.fontStyle = TMPro.FontStyles.Italic;
        }
        else
        {
            _textField.color = _normalTextColor;
            _textField.fontStyle = TMPro.FontStyles.Normal;
        }
    }
    public void DisplayNextLine()
    {
        if (_story.canContinue == false)
        {
            string text = _story.Continue(); // gets next line

            text = text?.Trim(); // removes white space from text
            ApplyStyling();
            _textField.text = text; // displays new text
        }
        if (!_story.canContinue)
        {
            _closeButton.enabled = true;
        }
        else if (_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }
   
    private void DisplayChoices()
    {
        // checks if choices are already being displaye
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _story.currentChoices.Count; i++) // iterates through all choices
        {

            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text); // creates a choice button

            button.onClick.AddListener(delegate
            {
                OnClickChoiceButton(choice);

            });
        }
        void OnClickChoiceButton(Choice choice)
        {
            _story.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
            RefreshChoiceView(); // removes choices from the screen
            DisplayNextLine();

        }
        void RefreshChoiceView()
        {
            if (_choiceButtonContainer != null)
            {
                foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
                {
                    Destroy(button.gameObject);
                }
            }
        }

        Button CreateChoiceButton(string text)
        {
            // creates the button from a prefab
            var choiceButton = Instantiate(_choiceButtonPrefab);
            choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);

            // sets text on the button
            var buttonText = choiceButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = text;
            return choiceButton;
        } 

    }
}
