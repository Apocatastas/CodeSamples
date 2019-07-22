using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using com.adjust.sdk;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;




public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string WEEK_SUB = "week_subscription";
    public GameObject subInterface;
    public GameObject noSubInterface;
    public GameObject parentalInterface;
    public GameObject mainScreen;
    public GameObject uiCanvas;
    public Text debugInfo;
    public Text price;
    public Text currency;
    public string priceString;
    public bool itsTime = false;
    public Assets.SimpleLocalization.MenuController m_menuController;
    private static Dictionary<string, string> introductory_info_dict;
    public bool isSubscriptionExpired;
    public bool isSubscribed;
    public bool isInited;

    public System.Collections.IEnumerator SubChecker() //IF WE GOT CONTROLLA WE SET PREFS
    {
        debugInfo.text += " SCH ";
        PlayerPrefs.SetInt("isSubscriber", 0);
        if (m_StoreController != null && m_StoreExtensionProvider != null)
        {
            if (m_StoreController.products.WithID("week_subscription").receipt != null)
            {
                if (m_StoreController.products.WithID("week_subscription").definition.type == ProductType.Subscription)
                {

                    if (checkIfProductIsAvailableForSubscriptionManager(m_StoreController.products.WithID("week_subscription").receipt))
                    {
                        string intro_json =
                         (introductory_info_dict == null
                          || !introductory_info_dict.ContainsKey(m_StoreController.products.WithID("week_subscription").definition.storeSpecificId))
                          ? null
                          : introductory_info_dict[m_StoreController.products.WithID("week_subscription").definition.storeSpecificId];
                        SubscriptionManager p = new SubscriptionManager(m_StoreController.products.WithID("week_subscription"), intro_json);
                        SubscriptionInfo info = p.getSubscriptionInfo();
                        Broadcaster.Week = m_StoreController.products.WithID(WEEK_SUB).metadata.localizedPriceString;
                        if (info.isSubscribed() == Result.True)
                        {
                            PlayerPrefs.SetInt("isSubscriber", 1);
                            debugInfo.text += " SCH1 ";
                        }
                        else
                        {
                            debugInfo.text += " SCH0 ";
                        }
                    }
                }
            }
        }
        yield return 0;
    }

    public System.Collections.IEnumerator SubSwitcher() //PREFS SWITCHER
    {
        debugInfo.text += " SSW ";
        uiCanvas.SetActive(true);
        switch (PlayerPrefs.GetInt("isSubscriber"))
        {
            case 1:
                subInterface.SetActive(true);
                noSubInterface.SetActive(false);
                debugInfo.text += " SSW1 ";
                break;
            case 0:
                subInterface.SetActive(false);
                noSubInterface.SetActive(true);
                debugInfo.text += " SSW0 ";
                break;
        }
        mainScreen.SetActive(false);
        yield return 0;
    }

    public void SendSuccessEvent()
    {
        debugInfo.text += " goAmp ";
        Amplitude.Instance.logEvent("SuccessBuy");
        Amplitude.Instance.setUserProperty("EP", "Subscr");
        if (PlayerPrefs.GetInt("FirstEP") == 1) { SendAdjustEvent("po56v3"); PlayerPrefs.SetInt("FirstEP", 0); };
        if (PlayerPrefs.GetInt("AnimationPushed") == 1) { SendAdjustEvent("lbdcaq"); PlayerPrefs.SetInt("AnimationPushed", 0); };
        if (PlayerPrefs.GetInt("CartPushed") == 1) { SendAdjustEvent("lkg0ns"); PlayerPrefs.SetInt("CartPushed", 0); };

    }

    public System.Collections.IEnumerator GetScreen()
    {
        debugInfo.text += " GS ";
        StartCoroutine("SubChecker");

        yield return new WaitForSeconds(2f);
        StartCoroutine("SubSwitcher");

        yield return 0;
    }
       public void BuySubscription(string productId)
    {
        BuyProductID(productId);
        InitSubscription(m_StoreController.products.WithID(productId));
    }
    private void InitSubscription(Product item) //THIS IS FOR DYNAMIC SWITCHING AFTER PURCHASE
    {
        if (item.receipt != null)
        {
            if (item.definition.type == ProductType.Subscription)
            {
                if (checkIfProductIsAvailableForSubscriptionManager(item.receipt))
                {
                    string intro_json =
                     (introductory_info_dict == null
                      || !introductory_info_dict.ContainsKey(item.definition.storeSpecificId))
                      ? null
                      : introductory_info_dict[item.definition.storeSpecificId];
                    SubscriptionManager p = new SubscriptionManager(item, intro_json);
                    SubscriptionInfo info = p.getSubscriptionInfo();

                    if (info.isSubscribed() == Result.True)
                    {
                        SendSuccessEvent();
                        PlayerPrefs.SetInt("isSubscriber", 1);
                        StartCoroutine("GetScreen");
                    }
                    else
                    {
                        if (info.isExpired() == Result.True)
                        {
                            PlayerPrefs.SetInt("isSubscriber", 0);
                            StartCoroutine("GetScreen");
                        }
                    }
                }
            }
        }
    }
    #region STANDART FUNC
    public bool WeHaveStore()
    {
        if (m_StoreController != null && m_StoreExtensionProvider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
   //         StartCoroutine("Pricer");
        }
    }

 //   public System.Collections.IEnumerator Pricer()
 //   {
 //       yield return new WaitForSeconds(3f);
        //price.text = m_StoreController.products.WithID(WEEK_SUB).metadata.localizedPriceString;
//        price.text = m_StoreController.products.WithID(WEEK_SUB).metadata.localizedPrice.ToString();
//        currency.text = m_StoreController.products.WithID(WEEK_SUB).metadata.isoCurrencyCode;
//        yield return 0;
//    }

    public void InitializePurchasing() //builder add products
    {
        if (m_StoreController != null && m_StoreExtensionProvider != null)
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(WEEK_SUB, ProductType.Subscription, new IDs(){

            { WEEK_SUB, GooglePlay.Name },
        });

        UnityPurchasing.Initialize(this, builder);
        //  Debug.Log("BUILDER " + Broadcaster.Week);
        


    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        isInited = true;
    }
    private void OnDeferred(Product item)
    {
    }
    public void SendAdjustEvent(string what)
    {
        AdjustEvent adjustEvent = new AdjustEvent(what);
        Adjust.trackEvent(adjustEvent);
    }
    public void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (m_StoreController != null && m_StoreExtensionProvider != null)
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null)
            {// Debug.Log("buy prod is not null" + product.definition.id); 
            }

            if (product.availableToPurchase)
            { //Debug.Log("buy prod is ready to purchase"); 
            }

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                //Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                // Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");

            }
        }
        else
        {
            // Debug.Log("BuyProductID FAIL. Not initialized.");

        }
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, WEEK_SUB, StringComparison.Ordinal))
        {
            InitSubscription(args.purchasedProduct);
        }
        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        //   Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));

    }
    private static bool checkIfProductIsAvailableForSubscriptionManager(string receipt)
    {
        var receipt_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
        if (!receipt_wrapper.ContainsKey("Store") || !receipt_wrapper.ContainsKey("Payload"))
        {
            //     Debug.Log("The product receipt does not contain enough information");

            return false;
        }

        var store = (string)receipt_wrapper["Store"];
        var payload = (string)receipt_wrapper["Payload"];

        if (payload != null)
        {
            switch (store)
            {
                case GooglePlay.Name:
                    {
                        var payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
                        if (!payload_wrapper.ContainsKey("json"))
                        {
                            //   Debug.Log(
                            //    "The product receipt does not contain enough information, the 'json' field is missing");

                            return false;
                        }

                        var original_json_payload_wrapper =
                         (Dictionary<string, object>)MiniJson.JsonDecode((string)payload_wrapper["json"]);
                        if (original_json_payload_wrapper == null
                            || !original_json_payload_wrapper.ContainsKey("developerPayload"))
                        {
                            //  Debug.Log(
                            //  "The product receipt does not contain enough information, the 'developerPayload' field is missing");

                            return false;
                        }

                        var developerPayloadJSON = (string)original_json_payload_wrapper["developerPayload"];
                        var developerPayload_wrapper =
                         (Dictionary<string, object>)MiniJson.JsonDecode(developerPayloadJSON);
                        if (developerPayload_wrapper == null || !developerPayload_wrapper.ContainsKey("is_free_trial")
                                                             || !developerPayload_wrapper.ContainsKey(
                                                              "has_introductory_price_trial"))
                        {
                            //  Debug.Log(
                            //  "The product receipt does not contain enough information, the product is not purchased using 1.19 or later");

                            return false;
                        }


                        return true;
                    }
                case AppleAppStore.Name:
                case AmazonApps.Name:
                case MacAppStore.Name:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        return false;
    }
    public void CheckAllProducts(Product[] products, Dictionary<string, string> dict)
    {
        introductory_info_dict = dict;
        foreach (var item in products)
        {
            if (item.availableToPurchase)
            {
                InitSubscription(item);
            }
        }
    }
    #endregion STANDART FUNC
}

