using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuation = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>(){ '.','!','?'},0.6f),
        new Punctuation(new HashSet<char>(){ ',',';',':'},0.3f),
    };

    private Coroutine typingCoroutine;

    public void Run(string textToType,Text textLable)
    {
        typingCoroutine = StartCoroutine(routine:TypeText(textToType,textLable));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, Text textLable)
    {
        IsRunning = true;
        textLable.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typeWriterSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value: charIndex, min: 0, max: textToType.Length);

            for(int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                textLable.text = textToType.Substring(startIndex: 0, length: i+1);

                if(IsPunctuation(textToType[i],out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1],out _))
                {
                    yield return YieldInstructionCache.WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }
        IsRunning = false;
    }

    private bool IsPunctuation(char character,out float waitTime)
    {
        foreach(Punctuation punctuationCategory in punctuation)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations,float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
