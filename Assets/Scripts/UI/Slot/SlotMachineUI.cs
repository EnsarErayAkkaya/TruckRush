using Project.GameSystems;
using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class SlotMachineUI : MonoBehaviour
    {
        [SerializeField] private GameObject slotCanvas;
        [SerializeField] private TextMeshProUGUI slotTitle;
        [SerializeField] private TextMeshProUGUI tokenCount;
        private void Start()
        {
            SetTokenCount(SlotManager.instance.SpinToken);
        }

        public void SetSlotTitle(string title)
        {
            slotTitle.text = title;
        }
        public void SetTokenCount(int count)
        {
            tokenCount.text = count.ToString();
        }
    }
}
