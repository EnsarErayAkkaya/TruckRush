using Project.Collectables;
using Project.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameSystems
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private SlotMachineUI slotMachineUI;
        [SerializeField] private EntranceUI entranceUI;
        [SerializeField] private SlotType[] slots;
        [SerializeField] private SlotUI[] slotUIs;
        [SerializeField] private float slotTurnDuration;
        [SerializeField] private float turningSoundPlayInterval;

        private int stoppedSlotCount;
        [SerializeField] private int spinToken;
        private int turningSoundIndex;
        private float lastTurningSoundPlayTime;
        private float currentTurningSoundPlayInterval;
        private bool slotsStarted;

        public int SpinToken => spinToken;

        public static SlotManager instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            spinToken = DataManager.instance.savedData.spinTokenCount;
            turningSoundIndex = AudioManager.instance.GetAudioIndex("slotTurning");
        }

        public void OnSlotStopped(int slotIndex, SlotType slotType )
        {
            slots[slotIndex] = slotType;
            stoppedSlotCount += 1;

            if (stoppedSlotCount == 3)
            {
                if (slots[0] == slots[1] && slots[1] == slots[2])
                {
                    AudioManager.instance.Play("slotCelebration");
                    if (slots[0] == SlotType.Coin1)
                    {
                        slotMachineUI.SetSlotTitle("10000 Coins");
                        CreditManager.instance.GainCredit(10000);
                    }
                    else if (slots[0] == SlotType.Coin2)
                    {
                        slotMachineUI.SetSlotTitle("20000 Coins");
                        CreditManager.instance.GainCredit(20000);
                    }
                    else if (slots[0] == SlotType.FlyStart)
                    {
                        slotMachineUI.SetSlotTitle("Head Start");
                        TruckManager.instance.AddHeadStart();
                        entranceUI.UpdateHeadStartText();
                    }
                }
                else
                    slotMachineUI.SetSlotTitle("Final Spin");
                
                stoppedSlotCount = 0;
                slotsStarted = false;
            }
        }
        public void RunSlots()
        {
            if (stoppedSlotCount != 0 || slotsStarted || spinToken < 1)
                return;

            spinToken--;
            DataManager.instance.savedData.spinTokenCount = spinToken;
            DataManager.instance.Save();
            slotsStarted = true;
            slotMachineUI.SetTokenCount(spinToken);
            StartCoroutine(PlaySlotAudio());

            foreach (SlotUI item in slotUIs)
            {
                item.CallRunSlot();
            }
        }
        private IEnumerator PlaySlotAudio()
        {
            lastTurningSoundPlayTime = 0;
            currentTurningSoundPlayInterval = turningSoundPlayInterval;
            float t = slotTurnDuration;
            while (t > 0)
            {
                t -= Time.deltaTime;
                if (lastTurningSoundPlayTime + currentTurningSoundPlayInterval <= Time.time)
                {
                    lastTurningSoundPlayTime = Time.time;
                    AudioManager.instance.Play(turningSoundIndex);
                    currentTurningSoundPlayInterval += 0.01f;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        public enum SlotType
        {
            Coin1, Coin2, FlyStart
        }
    }
}