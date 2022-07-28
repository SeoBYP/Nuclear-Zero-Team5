using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : SubUI
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text textLabel;
    [SerializeField] private string dialogueObjectName;
    [SerializeField] private Text _characterName1;
    [SerializeField] private Text _characterName2;
    [SerializeField] private Image _characterIcon1;
    [SerializeField] private Image _characterIcon2;

    private DialogueObject dialogueObject;
    private TypeWriterEffect typeWriterEffect;
    private ResponseHandler responseHandler;
    private PopupUI popupUI;

    public override void Init()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        popupUI = GetComponent<PopupUI>();

        if (_characterName1 != null)
            _characterName1.gameObject.SetActive(false);

        if (_characterIcon1 != null)
            _characterIcon1.gameObject.SetActive(false);

        if (_characterName2 != null)
            _characterName2.gameObject.SetActive(false);

        if (_characterIcon2 != null)
            _characterIcon2.gameObject.SetActive(false);

        CloseDialogueBox();
        if(dialogueObjectName == string.Empty)
        {
            dialogueObjectName = GameData.dialogueObjectName;
        }
        dialogueObject = ResourcesManager.Instance.Load<DialogueObject>("DialogueObject/" + dialogueObjectName);
        ShowDialogue(dialogueObject);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(routine: StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Data.Length; i++)
        {
            string dialogue = dialogueObject.Data[i].Dialogue;

            if (dialogueObject.Data[i].CharacterName == "Prologue")
                SetPrologueSprite(dialogueObject.Data[i].Sprite);
            else
            {
                SetCharacterNameText(dialogueObject.Data[i].CharacterName);
                SetCharacterSprite(dialogueObject.Data[i].Sprite);
            }

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Data.Length - 1 && dialogueObject.Responses != null && dialogueObject.Responses.Length > 0)
            {
                print(i);
                break;
            }
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        }
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
            if(popupUI != null)
                popupUI.ClosePopupUI();
        }
        yield return null;
    }

    private void SetCharacterNameText(string charactername)
    {
        if (_characterName1 == null && _characterName1 == null)
        {
            return;
        }
        if (charactername == string.Empty)
        {
            _characterName1.gameObject.SetActive(false);
            _characterName2.gameObject.SetActive(false);
        }
        else if(charactername == "딜런")
        {
            _characterName2.gameObject.SetActive(false);
            _characterName1.gameObject.SetActive(true);
            _characterName1.text = charactername;
        }
        else
        {
            _characterName1.gameObject.SetActive(false);
            _characterName2.gameObject.SetActive(true);
            _characterName2.text = charactername;
        }
    }

    private void SetCharacterSprite(Sprite sprite)
    {
        if (_characterIcon1 == null && _characterIcon2 == null)
            return;
        if (sprite == null)
        {
            _characterIcon1.gameObject.SetActive(false);
            _characterIcon2.gameObject.SetActive(false);
        }
        else if(_characterName1.gameObject.activeSelf == true)
        {
            _characterIcon2.gameObject.SetActive(false);
            _characterIcon1.gameObject.SetActive(true);
            _characterIcon1.sprite = sprite;
        }
        else if (_characterName2.gameObject.activeSelf == true)
        {
            _characterIcon1.gameObject.SetActive(false);
            _characterIcon2.gameObject.SetActive(true);
            _characterIcon2.sprite = sprite;
        }
    }

    private void SetPrologueSprite(Sprite sprite)
    {
        if (_characterIcon1 == null && _characterIcon2 == null)
            return;
        if (sprite == null)
        {
            _characterIcon1.gameObject.SetActive(false);
            //_characterIcon2.gameObject.SetActive(false);
        }
        else
        {
            _characterIcon1.gameObject.SetActive(true);
            _characterIcon1.sprite = sprite;
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);

        while (typeWriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                typeWriterEffect.Stop();
            }
        }

    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
