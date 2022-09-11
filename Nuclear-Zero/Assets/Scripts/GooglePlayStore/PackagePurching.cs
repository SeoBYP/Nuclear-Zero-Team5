using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class PackagePurching : MonoBehaviour
{
    [SerializeField] private IAPButton Adremoval;
    [SerializeField] private IAPButton Premierpack;
    [SerializeField] private IAPButton Starterpack;
    private ShopPopupUI _shop;
    public void InitPackage()
    {
        this.Adremoval.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
           AdremovalReward()
        ));
        this.Adremoval.onPurchaseFailed.AddListener(new UnityAction<Product, PurchaseFailureReason>((product, reason) =>
        Debug.LogFormat($"구매 실패 : {product.transactionID},{reason}")
        ));
        this.Premierpack.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
           PremierpackReward()
        ));
        this.Premierpack.onPurchaseFailed.AddListener(new UnityAction<Product, PurchaseFailureReason>((product, reason) =>
        Debug.LogFormat($"구매 실패 : {product.transactionID},{reason}")
        ));
        this.Starterpack.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
           StarterpackReward()
        ));
        this.Starterpack.onPurchaseFailed.AddListener(new UnityAction<Product, PurchaseFailureReason>((product, reason) =>
        Debug.LogFormat($"구매 실패 : {product.transactionID},{reason}")
        ));
    }

    public void AdremovalReward()
    {
        DataManager.Instance.playerInfo.ADRemove = true;
        _shop = UIManager.Instance.Get<ShopPopupUI>();
        if (_shop != null)
            _shop.DefaultSet();
    }

    public void PremierpackReward()
    {
        DataManager.Instance.playerInfo.SetShieldItemCount(2);
        DataManager.Instance.playerInfo.SetMagnetItemCount(2);
        DataManager.Instance.playerInfo.SetLifeItemCount(5);
        _shop = UIManager.Instance.Get<ShopPopupUI>();
        if (_shop != null)
            _shop.DefaultSet();
    }

    public void StarterpackReward()
    {
        DataManager.Instance.playerInfo.SetShieldItemCount(1);
        DataManager.Instance.playerInfo.SetMagnetItemCount(1);
        DataManager.Instance.playerInfo.SetLifeItemCount(2);
        _shop = UIManager.Instance.Get<ShopPopupUI>();
        if (_shop != null)
            _shop.DefaultSet();
    }
}
