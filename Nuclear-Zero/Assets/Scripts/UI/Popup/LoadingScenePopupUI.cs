using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScenePopupUI : PopupUI
{
    enum Texts
    {
        LoadingText,
    }

    enum Images
    {
        FillAmount,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.FillAmount).fillAmount = 0;
    }

    public void StartLoadSceneAsync(string scene)
    {
        UIManager.Instance.FadeIn();
        StartCoroutine(LoadSceneAsync(scene));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        Image progressBar = GetImage((int)Images.FillAmount);
        Text loadingText = GetText((int)Texts.LoadingText);
        loadingText.text = $"Loading... {0}%";
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
        float timer = 0;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                    timer = 0;
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    loadingText.text = $"Loading... {100}%";
                    ClosePopupUI();
                    yield break;
                }
            }
            loadingText.text = $"Loading... {Mathf.Round(progressBar.fillAmount * 100)}%";
        }
        if (op.isDone)
        {
            ClosePopupUI();
        }
    }

    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
    }
}
