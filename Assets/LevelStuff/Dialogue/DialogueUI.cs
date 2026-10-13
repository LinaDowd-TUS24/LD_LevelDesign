using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Shows dialogue on screen. It only displays things: the DialogueRunner decides
// WHAT to show and calls these methods.
//
// The choice buttons are made in the editor, not in code. Each button's On Click ()
// calls DialogueRunner.Choose with its own number: 0, 1 or 2.
// Buttons that aren't needed are hidden, so a node can have up to 3 choices.
public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue Box")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMP_Text _speakerText;
    [SerializeField] private TMP_Text _lineText;

    // The "Press E to continue" hint. Hidden while choices are showing,
    // because then the player has to click a choice instead.
    [SerializeField] private GameObject _continuePrompt;

    [Header("Choices")]
    [SerializeField] private Button[] _choiceButtons;

    public void ShowLine(DialogueLine line)
    {
        _dialoguePanel.SetActive(true);
        _speakerText.text = line.Speaker;
        _lineText.text = line.Text;

        _continuePrompt.SetActive(true);
        HideChoices();
    }

    // The last line stays on screen while the choices are showing,
    // so the player can still read the question they are answering.
    public void ShowChoices(DialogueChoice[] choices)
    {
        _dialoguePanel.SetActive(true);
        _continuePrompt.SetActive(false);

        if (choices.Length > _choiceButtons.Length)
        {
            Debug.LogWarning("This node has " + choices.Length + " choices, but there are only "
                + _choiceButtons.Length + " buttons. The extra choices won't be shown.");
        }

        for (int i = 0; i < _choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                // The button's label is a TMP_Text on a child of the button.
                TMP_Text buttonLabel = _choiceButtons[i].GetComponentInChildren<TMP_Text>();
                buttonLabel.text = choices[i].Text;
                _choiceButtons[i].gameObject.SetActive(true);
            }
            else
            {
                _choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void Hide()
    {
        _dialoguePanel.SetActive(false);
        HideChoices();
    }

    private void HideChoices()
    {
        foreach (Button button in _choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
