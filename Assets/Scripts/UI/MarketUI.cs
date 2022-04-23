using DanielLochner.Assets.SimpleScrollSnap;
using Project.GameSystems;
using Project.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class MarketUI : MonoBehaviour
    {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color notSelectedColor;

        [SerializeField] private Button selectBuyButton;
        [SerializeField] private Text selectBuyButtonText;
        [SerializeField] private Image selectBuyButtonImage;

        [SerializeField] private GameObject priceTextParent;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image selectedImage;
        [SerializeField] private SimpleScrollSnap sss;

        [SerializeField] private MarketObjectSetting[] trucks;
        [SerializeField] private MarketObjectSetting[] wings;
        [SerializeField] private Vector3 selectedRenderCameraOffset;
        [SerializeField] private Transform selectedRenderCamera;
        [SerializeField] private Transform worldCanvas;

        private int activeContent;
        private int selectedIndex;
        private int index;
        private int contentLength;

        private void Start()
        {
            activeContent = -1;

            SetMarketContent(0);
        }

        public void SetMarketContent(int i)
        {
            if (i != activeContent)
            {
                selectedRenderCamera.SetParent(worldCanvas);
                if (sss.Panels != null)
                {
                    int count = sss.Panels.Length;
                    for (int j = 0; j < count; j++)
                    {
                        sss.RemoveFromBack();
                    }
                }
                

                activeContent = i;
                index = 0;

                if (activeContent == 0) // truck
                {
                    selectedIndex = TruckManager.instance.TruckIndex;
                    foreach (MarketObjectSetting item in trucks)
                    {
                        sss.AddToBack(item.MarketObject);
                    }
                }
                else if (activeContent == 1) // wings
                {
                    selectedIndex = TruckManager.instance.WingIndex;
                    foreach (MarketObjectSetting item in wings)
                    {
                        sss.AddToBack(item.MarketObject);
                    }
                }
                else if(activeContent == 2) // customization
                {
                    
                }

                if(activeContent != 2)
                {
                    index = selectedIndex;
                    sss.startingPanel = selectedIndex;
                    sss.GoToPanel(selectedIndex);
                }
            }

            SetContentLength();
            SetMarketObjectInfo();
            SetSelectedRenderCameraPos();
        }
        public void OnNextButtonClicked()
        {
            index++;

            if (index >= contentLength)
                index = 0;

            SetMarketObjectInfo();
        }
        public void OnPreviousButtonClicked()
        {
            index--;

            if (index < 0)
                index = contentLength - 1;

            SetMarketObjectInfo();
        }

        private void SetMarketObjectInfo()
        {
            MarketObjectSetting mos = ScriptableObject.CreateInstance<MarketObjectSetting>();
            if (activeContent == 0) // truck
            {
                mos = trucks[index];
            }
            else if (activeContent == 1) // wings
            {
                mos = wings[index];
            }

            titleText.text = mos.title;

            if (activeContent == 0 && !TruckManager.instance.IsTruckOwned(mos.name)) // truck
            {
                if (!priceTextParent.activeSelf)
                    priceTextParent.SetActive(true);

                priceText.text = mos.price.ToString();
                selectBuyButtonText.text = "Buy";
            }
            else if (activeContent == 1 && !TruckManager.instance.IsWingOwned(mos.name)) // wing
            {
                if (!priceTextParent.activeSelf)
                    priceTextParent.SetActive(true);

                priceText.text = mos.price.ToString();
                selectBuyButtonText.text = "Buy";
            }
            else if (priceTextParent.activeSelf)
            {
                priceTextParent.SetActive(false);
                selectBuyButtonText.text = "Select";
            }

            // set selected 

            selectedImage.enabled = index == selectedIndex;
            selectBuyButton.gameObject.SetActive(index != selectedIndex);

            SetButtonColor();
        }
        private void SetSelectedRenderCameraPos()
        {
            Transform t = sss.Content.GetChild(selectedIndex);
            selectedRenderCamera.SetParent(t);
            selectedRenderCamera.LookAt(t);
            selectedRenderCamera.rotation = Quaternion.Euler(15, 0, 0);
            selectedRenderCamera.position = t.position + selectedRenderCameraOffset;
        }
        
        private void SetContentLength() => contentLength = sss.Panels != null? sss.Panels.Length:0;

        public void OnClickSelectBuyButton()
        {
            MarketObjectSetting[] content = activeContent == 0 ? trucks : wings;
            
            selectedIndex = index;
            if (activeContent == 0 && TruckManager.instance.IsTruckOwned(content[index].name)) // truck
            {
                TruckManager.instance.SelectTruck(index);
                SetSelectedRenderCameraPos();

                selectedImage.enabled = true;
                SetButtonColor();
            }
            else if (activeContent == 1 && TruckManager.instance.IsWingOwned(content[index].name)) // wing
            {
                TruckManager.instance.SelectWing(index);
                SetSelectedRenderCameraPos();

                selectedImage.enabled = true;
                SetButtonColor();
            }
            else if(CreditManager.instance.IsCreditSufficient(content[index].price))
            {
                if (activeContent == 0) // truck
                {
                    TruckManager.instance.BuyTruck(content[index].name);
                }
                else if (activeContent == 1) // wing
                {
                    TruckManager.instance.BuyWing(content[index].name);
                }
                CreditManager.instance.LoseCredit(content[index].price);
                selectBuyButtonText.text = "Select";
            }
        }
        public void SetButtonColor()
        {
            if(index == selectedIndex)
                selectBuyButtonImage.color = selectedColor;
            else
                selectBuyButtonImage.color = notSelectedColor;
        }
    }
}