using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class CoinPackagePurching : MonoBehaviour
{
    [SerializeField] private IAPButton Coinstep1;
    [SerializeField] private IAPButton Coinstep2;
    [SerializeField] private IAPButton Coinstep3;
    [SerializeField] private IAPButton Coinstep4;
    private ShopPopupUI _shop;
    public void InitCoinPackage()
    {
        this.Coinstep1.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
           Coinstep1Reward()
        ));
        this.Coinstep1.onPurchaseFailed.AddListener(new UnityAction<Product, PurchaseFailureReason>((product, reason) =>
        Debug.LogFormat($"구매 실패 : {product.transactionID},{reason}")
        ));
        this.Coinstep2.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
           Coinstep2Reward()
        ));
        this.Coinstep2.onPurchaseFailed.AddListener(new UnityAction<Product, PurchaseFailureReason>((product, reason) =>
        Debug.LogFormat($"구매 실패 : {product.transactionID},{reason}")
        ));
        this.Coinstep3.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
           Coinstep3Reward()
        ));
        this.Coinstep3.onPurchaseFailed.AddListener(new UnityAction<Product, PurchaseFailureReason>((product, reason) =>
        Debug.LogFormat($"구매 실패 : {product.transactionID},{reason}")
        ));
        this.Coinstep4.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
           Coinstep4Reward()
        ));
        this.Coinstep4.onPurchaseFailed.AddListener(new UnityAction<Product, PurchaseFailureReason>((product, reason) =>
        Debug.LogFormat($"구매 실패 : {product.transactionID},{reason}")
        ));
    }

    public void Coinstep1Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(8000);
        _shop = UIManager.Instance.Get<ShopPopupUI>();
        if (_shop != null)
            _shop.DefaultSet();
    }

    public void Coinstep2Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(13400);
        _shop = UIManager.Instance.Get<ShopPopupUI>();
        if (_shop != null)
            _shop.DefaultSet();
    }

    public void Coinstep3Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(20000);
        _shop = UIManager.Instance.Get<ShopPopupUI>();
        if (_shop != null)
            _shop.DefaultSet();
    }
    public void Coinstep4Reward()
    {
        DataManager.Instance.playerInfo.SetCoin(28500);
        _shop = UIManager.Instance.Get<ShopPopupUI>();
        if (_shop != null)
            _shop.DefaultSet();
    }
}
