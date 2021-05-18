
using Project.GameSystems;
using TMPro;

namespace Project.Collectables
{
    public class DistanceMilestone : Collectable
    {
        public TextMeshProUGUI text;
        public void Set(float milestone)
        {
            text.text = milestone.ToString();
        }
        public override int OnPlayerCollided()
        {
            if (!destroyed)
            {
                destroyed = true;
                ScoreManager.instance.AchieveMilestone();
            }
            return -1;
        }
    }
}
