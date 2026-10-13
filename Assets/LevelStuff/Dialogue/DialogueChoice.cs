using UnityEngine;

//One answer the player can pick, e.g. "Who are you?".
//Picking it moves the conversation on to _nextNode

[System.Serializable]
public class DialogueChoice
{
    [SerializeField]
    private string _text;

    //The node to go to when this choice is picked.
    //Drag a DialogueNode asset here. Leave it empty (None) to end the conversation.

    [SerializeField]
    private DialogueNode _nextNode;

    public string Text
    { 
        get { return _text; } 
    }

    public DialogueNode NextNode
    {
        get { return _nextNode; }
    }
}
