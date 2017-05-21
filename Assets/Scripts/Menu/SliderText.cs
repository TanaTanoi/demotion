using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{

    Text textComponent;
    public bool zeroIsInfinite;
    public string message;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void SetSliderValue(float sliderValue)
    {

        string value = "";
        if(zeroIsInfinite && sliderValue == 0)
        {
            value = "∞";
        }
        else {
            value = Mathf.Round(sliderValue).ToString();
        }
        textComponent.text = message + value;
    }
}