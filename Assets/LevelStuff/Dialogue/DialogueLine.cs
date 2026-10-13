using UnityEngine;

//One line of dialogue: Who says it, and what they say
[System.Serializable]
public class DialogueLine
{
    [SerializeField]
    private string _speaker;

    //[TextArea] gives abigger, multi-line text box in the Inspector
    [TextArea(2, 5)]
    [SerializeField]
    private string _text;

    //Other scripts can read the speaker and text, but can't change them.
    public string Speaker
    { 
        get { return _speaker; }
    }

    public string Text
    { 
        get { return _text; } 
    }

}
