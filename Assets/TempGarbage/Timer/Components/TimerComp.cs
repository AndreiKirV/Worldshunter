using TMPro;

namespace Client
{
    struct TimerComp
    {
        public float Speed;
        public float Value;
        public TextMeshProUGUI ViewText;

        public void SetTime()
        {
            ViewText.text = $"{(int)(Value / 60)} : {(int)(Value % 60)}";
        }
    }
}