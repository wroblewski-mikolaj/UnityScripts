using UnityEngine;
using UnityEngine.UI;

public class SpriteRandomizer : MonoBehaviour
{
    private float timePassed;
    private Image image;

    public Sprite[] spriteArray;

    private void Start()
    {
        image = this.GetComponent<Image>(); 
    }

    private void OnEnable()
    {
        timePassed = 0;
    }

    private void ChangeSprite()
    {
        image.sprite = spriteArray[Random.Range(0, spriteArray.Length)];
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 5f)
        {
            ChangeSprite();
            timePassed = 0;
        }
    }
}
