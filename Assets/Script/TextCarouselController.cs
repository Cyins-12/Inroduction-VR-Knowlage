using UnityEngine;
using UnityEngine.UI;

public class TextCarouselController : MonoBehaviour
{
    public Text displayText;
    public Button prevButton;
    public Button nextButton;

    private int currentIndex = 0;

    private string[] words = { "Pedang Kayu", "Pistol"};

    void Start()
    {
        UpdateText();

        // Tambahkan listener untuk tombol
        prevButton.onClick.AddListener(PrevWord);
        nextButton.onClick.AddListener(NextWord);
    }

    public void NextWord()
    {
        if (currentIndex < words.Length - 1)
        {
            currentIndex++;
            UpdateText();
        }
    }

    public void PrevWord()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateText();
        }
    }

    void UpdateText()
    {
        displayText.text = words[currentIndex];

        // Update status tombol
        nextButton.interactable = currentIndex < words.Length - 1;
        prevButton.interactable = currentIndex > 0;
    }
}
