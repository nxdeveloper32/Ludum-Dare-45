using UnityEngine;
using UnityEngine.UI;
public class ColorSetter : MonoBehaviour
{
    public Color ColorForChild;
    public Image child;
    Image myImage;
    CannonManager Cannon;
    GameObject Temp;
    public string MyText;
    public Image LoadingBar;
    public bool isLoading;
    private void Start()
    {
        myImage = GetComponent<Image>();
        Cannon = CannonManager.instance;
        if (!isLoading)
        {
            LoadingBar = transform.GetChild(0).GetComponent<Image>();
        }
    }
    public void SetColor()
    {
        child.color = ColorForChild;
    }

    public void SelectMe()
    {
        SoundManager.Instance.PlaySFX(GameManager.Instance.buttonClick);
        if (Cannon.MyWeapon != null)
        {
            Temp = Cannon.MyWeapon;
            Temp.GetComponent<Image>().color = Color.white;
        }
        myImage.color = Color.yellow;
        Cannon.MyWeapon = transform.gameObject;
    }
}
