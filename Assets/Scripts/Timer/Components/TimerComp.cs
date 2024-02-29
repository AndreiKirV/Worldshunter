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
            ViewText.text = $"{(Value / 60)} : {(Value % 60)}";
        }
    }
}