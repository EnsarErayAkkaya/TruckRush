using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class BaseNotificationUIObject : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public void SetNotificationColorAlpha(float t)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, t);
        }
    }
}