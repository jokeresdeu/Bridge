using System;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class InAppPurchacer : MonoBehaviour, IStoreListener
{
    public static InAppPurchacer instance;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
        if (IsInitialized())
        {
            if(noADSPrice != null)
            {
                noADSPrice.text = m_StoreController.products.WithID(noADS).metadata.localizedPriceString;
                fiveHintsPrice.text = m_StoreController.products.WithID(fiveHints).metadata.localizedPriceString;
                twentyHintsPrice.text = m_StoreController.products.WithID(twentyHints).metadata.localizedPriceString;
                twelwHintsPrice.text = m_StoreController.products.WithID(twelwHints).metadata.localizedPriceString;
            }
            
        }
    }
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string fiveHints = "5hints";
    public static string twelwHints = "12hints";
    public static string twentyHints = "20hints";
    public static string noADS = "no_ads";

    [SerializeField] TMP_Text fiveHintsPrice;
    [SerializeField] TMP_Text twelwHintsPrice;
    [SerializeField] TMP_Text twentyHintsPrice;
    [SerializeField] TMP_Text noADSPrice;

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
       
    }

    public void InitializePurchasing()
    {

        if (IsInitialized())
        {
            return;
        }
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(fiveHints, ProductType.Consumable);
        builder.AddProduct(twelwHints, ProductType.Consumable);
        builder.AddProduct(twentyHints, ProductType.Consumable);
        builder.AddProduct(noADS, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void Buy5Tips()
    {
        BuyProductID(fiveHints);
    }

    public void Buy12Tips()
    {
        BuyProductID(twelwHints);
    }

    public void Buy20Tips()
    {
        BuyProductID(twentyHints);
    }

    public void BuyNoADS()
    {
        BuyProductID(noADS);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");
        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        if (noADSPrice != null)
        {
            noADSPrice.text = m_StoreController.products.WithID(noADS).metadata.localizedPriceString;
            fiveHintsPrice.text = m_StoreController.products.WithID(fiveHints).metadata.localizedPriceString;
            twentyHintsPrice.text = m_StoreController.products.WithID(twentyHints).metadata.localizedPriceString;
            twelwHintsPrice.text = m_StoreController.products.WithID(twelwHints).metadata.localizedPriceString;
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, fiveHints, StringComparison.Ordinal))
        {
            int tips = PlayerPrefs.GetInt("Hints", 0);
            PlayerPrefs.SetInt("Hints", tips + 5);
            Debug.Log("You just bought 5tips");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, twelwHints, StringComparison.Ordinal))
        {
            int tips = PlayerPrefs.GetInt("Hints", 0);
            PlayerPrefs.SetInt("Hints", tips + 12);
            Debug.Log("You just bought 12tips");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, twentyHints, StringComparison.Ordinal))
        {
            int tips = PlayerPrefs.GetInt("Hints", 0);
            PlayerPrefs.SetInt("Hints", tips + 20);
            Debug.Log("You just bought 20tips");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, noADS, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("ADSRemoved", 1);
            Debug.Log("You removed your ads");
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
