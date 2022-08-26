using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.Analytics;

public class PackagePurching : MonoBehaviour, IStoreListener
{
    static IStoreController storeController = null;
    static string[] sProductIds;

    void Awake()
    {
        if (storeController == null)
        {
            sProductIds = new string[] { "adremoval", "coin_step1", "coin_step2", "coin_step3", "coin_step4", "premierpack", "starterpack"};
            InitStore();
        }
    }

    void InitStore()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(sProductIds[0], ProductType.Consumable, new IDs { { sProductIds[0], GooglePlay.Name } });
        builder.AddProduct(sProductIds[1], ProductType.Consumable, new IDs { { sProductIds[1], GooglePlay.Name } });
        builder.AddProduct(sProductIds[2], ProductType.Consumable, new IDs { { sProductIds[0], GooglePlay.Name } });
        builder.AddProduct(sProductIds[3], ProductType.Consumable, new IDs { { sProductIds[1], GooglePlay.Name } });
        builder.AddProduct(sProductIds[4], ProductType.Consumable, new IDs { { sProductIds[0], GooglePlay.Name } });
        builder.AddProduct(sProductIds[5], ProductType.Consumable, new IDs { { sProductIds[1], GooglePlay.Name } });
        builder.AddProduct(sProductIds[6], ProductType.Consumable, new IDs { { sProductIds[1], GooglePlay.Name } });
        UnityPurchasing.Initialize(this, builder);
    }


    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
    {

    }

    public void OnBtnPurchaseClicked(int index)
    {
        if (storeController == null)
        {
            print("실패");
        }
        else
        {
            storeController.InitiatePurchase(sProductIds[index]);
        }
    }

    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs e)
    {
        bool isSuccess = true;
#if UNITY_ANDROID && !UNITY_EDITOR
		CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
		try
		{
			IPurchaseReceipt[] result = validator.Validate(e.purchasedProduct.receipt);
			for(int i = 0; i < result.Length; i++)
				Analytics.Transaction(result[i].productID, e.purchasedProduct.metadata.localizedPrice, e.purchasedProduct.metadata.isoCurrencyCode, result[i].transactionID, null);
		}
		catch (IAPSecurityException)
		{
			isSuccess = false;
		}
#endif
        if (isSuccess)
        {
            if (e.purchasedProduct.definition.id.Equals(sProductIds[0]))
                AdremovalReward();
            else if (e.purchasedProduct.definition.id.Equals(sProductIds[1]))
                Coinstep1Reward();
            else if (e.purchasedProduct.definition.id.Equals(sProductIds[2]))
                Coinstep2Reward();
            else if (e.purchasedProduct.definition.id.Equals(sProductIds[3]))
                Coinstep3Reward();
            else if (e.purchasedProduct.definition.id.Equals(sProductIds[4]))
                Coinstep4Reward();
            else if (e.purchasedProduct.definition.id.Equals(sProductIds[5]))
                PremierpackReward();
            else if (e.purchasedProduct.definition.id.Equals(sProductIds[6]))
                StarterpackReward();
        }
        else
        {
            //txtLog.text = "구매 실패 : 비정상 결제";
            //Debug.Log(txtLog.text);
        }

        return PurchaseProcessingResult.Complete;
    }

    void IStoreListener.OnPurchaseFailed(Product i, PurchaseFailureReason error)
    {
        if (!error.Equals(PurchaseFailureReason.UserCancelled))
        {

        }
    }
    public void AdremovalReward()
    {
        DataManager.Instance.playerInfo.ADRemove = true;
    }

    //public void AdremovalFailed()
    //{
        
    //}

    public void Coinstep1Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(8000);
    }

    //public void Coinstep1Failed()
    //{
    //    print("문제가 있음");
    //}

    public void Coinstep2Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(13400);
    }

    //public void Coinstep2Failed()
    //{
    //    print("문제가 있음");
    //}

    public void Coinstep3Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(20000);
    }

    //public void Coinstep3Failed()
    //{
    //    print("문제가 있음");
    //}

    public void Coinstep4Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(28500);
    }

    //public void Coinstep4Failed()
    //{
    //    print("문제가 있음");
    //}

    public void PremierpackReward()
    {
        DataManager.Instance.playerInfo.SetShieldItemCount(2);
        DataManager.Instance.playerInfo.SetMagnetItemCount(2);
        DataManager.Instance.playerInfo.SetLifeItemCount(5);
    }

    //public void PremierpackFailed()
    //{
    //    print("문제가 있음");
    //}

    public void StarterpackReward()
    {
        DataManager.Instance.playerInfo.SetShieldItemCount(1);
        DataManager.Instance.playerInfo.SetMagnetItemCount(1);
        DataManager.Instance.playerInfo.SetLifeItemCount(2);
    }

    //public void StarterpackFailed()
    //{
    //    print("문제가 있음");
    //}
}
