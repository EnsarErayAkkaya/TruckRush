using Project.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Project.GameSystems.SlotManager;

namespace Project.UI
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private float minStartingSpeed;
        [SerializeField] private float maxStartingSpeed;
        [SerializeField] private float snapSpeed;
        [SerializeField] private float padding;
        [SerializeField] private float childHeight;

        [SerializeField] private List<SlotType> slots; 
        [SerializeField] private int slotIndex; 

        private RectTransform rectTransform;
        private float speedSlowingDownFactor;
        private float currentSpeed;

        private int index;

        private List<RectTransform> childs;
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            
            childs = new List<RectTransform>();
            foreach (Transform t in transform)
            {
                childs.Add(t.GetComponent<RectTransform>());
            }
            index = childs.Count;
            

            SetChildStartingPoses();
        }
        public void CallRunSlot()
        {
            currentSpeed = Random.Range(minStartingSpeed, maxStartingSpeed); 
            speedSlowingDownFactor = currentSpeed / duration;
            StartCoroutine(RunSlot());
        }
        private IEnumerator RunSlot()
        {
            float t = duration;
            
            while (t > 0)
            {
                t -= Time.deltaTime;
                currentSpeed -= speedSlowingDownFactor * Time.deltaTime;
                if (currentSpeed < 0.1f)
                {
                    currentSpeed = 0;
                    break;
                }
                MoveChilds();
                yield return new WaitForEndOfFrame();
            }
            // move closest child to center
            SetClosestChild();
            Vector2 pos;
            int multiply = 1;

            if (childs[index].anchoredPosition.y > 0)
                multiply = -1;

            while (childs[index].anchoredPosition != Vector2.zero)
            {
                childs[index].anchoredPosition = Vector2.MoveTowards(childs[index].anchoredPosition, Vector2.zero, Time.deltaTime * snapSpeed);
                for (int i = 0; i < childs.Count; i++)
                {
                    if (i != index)
                    {
                        pos = childs[i].anchoredPosition;

                        pos.y += (snapSpeed * Time.deltaTime) * multiply;
                        childs[i].anchoredPosition = pos;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            instance.OnSlotStopped(slotIndex, slots[index]);
        }
        private void SetChildStartingPoses()
        {
            Vector2 pos = Vector2.zero;
            foreach (RectTransform c in childs)
            {
                c.anchoredPosition = pos;
                pos.y += childHeight + padding;
            }
        }
        private void MoveChilds()
        {
            Vector2 pos;
            for (int i = 0; i < childs.Count; i++)
            {
                pos = childs[i].anchoredPosition;
                if (pos.y < -this.rectTransform.sizeDelta.y - padding)
                {
                    pos = childs[childs.Count - 1].anchoredPosition;
                    pos.y += childHeight + padding;
                    childs[i].anchoredPosition = pos;

                    RectTransform temp = childs[i];
                    childs.RemoveAt(i);
                    childs.Add(temp);

                    SlotType type = slots[i];
                    slots.RemoveAt(i);
                    slots.Add(type);
                }
                else
                {
                    pos.y -= currentSpeed * Time.deltaTime;
                    childs[i].anchoredPosition = pos;
                }
            }
        }
        private void SetClosestChild()
        {
            float closestDist = int.MaxValue;
            int i = -1;
            foreach (RectTransform c in childs)
            {
                i++;
                float dist = Vector2.Distance(c.anchoredPosition, Vector2.zero);
                if(dist < closestDist)
                {
                    index = i;
                    closestDist = dist;
                }
            }
        }
    }
}
