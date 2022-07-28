using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] TalkData[] talkData;
    [SerializeField] private Response[] responses;

    public TalkData[] Data => talkData;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

    
}
[System.Serializable]
public struct TalkData
{
    [SerializeField] [TextArea] private string dialogues;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string charactername;


    public Sprite Sprite => sprite;

    public string CharacterName => charactername;

    public string Dialogue => dialogues;
}