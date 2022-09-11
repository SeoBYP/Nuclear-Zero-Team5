using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SHARE_INFO
{
	public static string SUBJECT = "Nuclear-Zero";
	public static string TEXT = "Nuclear-Zero에 대해 알아보세요";
	public static string TARGETURL = "https://play.google.com/store/apps/details?id=com.EvenI.NuclearZero";
}

public class ClickHandler : MonoBehaviour
{ 

    public void ShareSomething()
    {
		StartCoroutine(TakeScreenshotAndShare());
	}

	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		new NativeShare().SetSubject(SHARE_INFO.SUBJECT).SetText(SHARE_INFO.TEXT).SetUrl($"{SHARE_INFO.TARGETURL}")
			.SetCallback((result, shareTarget) => ShareCallBack(result, shareTarget))
			.Share();
	}

	private void ShareCallBack(NativeShare.ShareResult result, string shareTarget)
    {
		SharePopupUI share = UIManager.Instance.Get<SharePopupUI>();
		if(share != null)
        {
			share.Shared();
        }
    }
}
