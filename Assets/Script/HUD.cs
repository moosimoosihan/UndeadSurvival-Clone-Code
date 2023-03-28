using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoYType { Exp, Level, Kill, Time, Health }
    public InfoYType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch(type){
            case InfoYType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level,GameManager.instance.nextExp.Length-1)];
                mySlider.value = curExp/maxExp;
                break;
            case InfoYType.Level:
                myText.text = string.Format("Lv.{0:F0}",GameManager.instance.level);
                break;
            case InfoYType.Kill:
                myText.text = string.Format("{0:F0}",GameManager.instance.kill);
                break;
            case InfoYType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}",min, sec);
                break;
            case InfoYType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth/maxHealth;
                break;
        }
    }
}
