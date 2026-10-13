using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

// Plays a conversation, one step at a time.
//
// How a conversation runs:
//   1. StartConversation(firstNode) pauses the player and shows the node's first line.
//   2. Each time the player presses E, Advance() shows the next line.
//   3. After the last line, the node's choices are shown (if it has any).
//   4. Clicking a choice calls Choose(0), Choose(1) or Choose(2), which moves to that
//      choice's next node, and we're back at step 2 with the new node.
//   5. A node with no choices, or a choice with no next node, ends the conversation.
//
// This script never knows WHAT is being said. All the words live in DialogueNode assets,
// so the same runner can play every conversation in the game.
public class DialogueRunner : MonoBehaviour
{
    [SerializeField] private DialogueUI _dialogueUI;

    // The player's movement script. It's turned off during a conversation
    // so the player can't walk away or look around while talking.
    [SerializeField] private FirstPersonController _playerController;

    [SerializeField]
    private PlayerAnimControl _playerAnimator;

    // For testing: if a node is dragged in here, it starts as soon as you press Play.
    // Leave it empty (None) in a finished game.
    [SerializeField] private DialogueNode _startOnPlay;

    private DialogueNode _currentNode;
    private int _currentLineIndex;
    private bool _isTalking;

    // The node the most recent conversation ended on. It keeps its value after the
    // conversation is over, so other scripts can ask "how did that conversation end?"
    private DialogueNode _lastNode;

    // Other scripts (like PlayerTalk) can ask "is a conversation running?"
    public bool IsTalking
    {
        get { return _isTalking; }
    }

    public DialogueNode LastNode
    {
        get { return _lastNode; }
    }

    private void Start()
    {
        _dialogueUI.Hide();

        if (_startOnPlay != null)
        {
            StartConversation(_startOnPlay);
        }
    }

    public void StartConversation(DialogueNode firstNode)
    {
        _isTalking = true;
        PausePlayer();
        ShowNode(firstNode);
        Debug.Log("Conversation started: " + firstNode.name);
    }

    // Called when the player presses E during a conversation.
    public void Advance()
    {
        // Several checks in a row, so each one stops early if we can't advance.
        if (_isTalking == false)
        {
            return;
        }

        // Past the last line means the choices are showing.
        // The player has to click one, so pressing E does nothing.
        if (_currentLineIndex >= _currentNode.Lines.Length)
        {
            return;
        }

        _currentLineIndex = _currentLineIndex + 1;
        ShowCurrentStep();
    }

    // Called by the choice buttons' On Click () event, with 0, 1 or 2.
    public void Choose(int choiceIndex)
    {
        if (_isTalking == false)
        {
            return;
        }

        if (choiceIndex >= _currentNode.Choices.Length)
        {
            return;
        }

        DialogueChoice chosen = _currentNode.Choices[choiceIndex];
        Debug.Log("Player chose: " + chosen.Text);

        // An empty Next Node means "this choice ends the conversation".
        if (chosen.NextNode != null)
        {
            ShowNode(chosen.NextNode);
        }
        else
        {
            EndConversation();
        }
    }

    public void EndConversation()
    {
        _isTalking = false;
        _lastNode = _currentNode;
        _currentNode = null;
        _dialogueUI.Hide();
        ResumePlayer();
        Debug.Log("Conversation ended");
    }

    // Starts a node from its first line.
    private void ShowNode(DialogueNode node)
    {
        _currentNode = node;
        _currentLineIndex = 0;
        ShowCurrentStep();
    }

    // Shows whatever comes next in the current node: a line, the choices, or the end.
    private void ShowCurrentStep()
    {
        if (_currentLineIndex < _currentNode.Lines.Length)
        {
            DialogueLine line = _currentNode.Lines[_currentLineIndex];
            _dialogueUI.ShowLine(line);
        }
        else if (_currentNode.Choices.Length > 0)
        {
            _dialogueUI.ShowChoices(_currentNode.Choices);
        }
        else
        {
            EndConversation();
        }
    }

    private void PausePlayer()
    {
        // TODO (Part 4): at the moment the player can still walk and look around while
        // talking, and the mouse is locked, so the choice buttons can't be clicked.
        //   1. Turn off the player's movement script, _playerController.
        //      Hint: every component has an "enabled" bool, the tick box in the Inspector.
        //   2. Unlock the mouse and make it visible.
        //      Hint: look up Cursor.lockState and Cursor.visible.

        _playerController.enabled = false;
        _playerAnimator.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumePlayer()
    {
        // TODO (Part 4): undo everything PausePlayer() did, so the player can move,
        // look around and has a locked, hidden mouse again.
        _playerController.enabled = true;
        _playerAnimator.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
