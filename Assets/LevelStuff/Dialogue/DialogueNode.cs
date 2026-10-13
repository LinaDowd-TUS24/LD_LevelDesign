using UnityEngine;

//One step of a conversation: some lines are said, then the player may pick a choice.
//It's a ScriptableObject, so it's saved as an asset in the Project window.
[CreateAssetMenu(fileName = "DialogueNode", menuName = "Scriptable Objects/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    //Said one after another. The player presses E to see the next line.
    [SerializeField]
    private DialogueLine[] _lines;

    //Shown after the last line. Leave the list empty to end the conversation.
    [SerializeField]
    private DialogueChoice[] _choices;

    public DialogueLine[] Lines 
    { 
        get { return _lines; } 
    }

    public DialogueChoice[] Choices 
    { 
        get { return _choices; } 
    }

}
