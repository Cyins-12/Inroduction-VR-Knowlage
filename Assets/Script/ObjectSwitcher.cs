using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotSwitcher : MonoBehaviour
{
    public GameObject[] slots; // Array untuk slot
    public GameObject[] objects; // Array untuk objek di kotak besar
    public string[] objectNames; // Array untuk nama objek
    public string[] objectShortDescriptions; // Array untuk deskripsi singkat objek
    public string[] objectDetails; // Array untuk detail objek
    public TextMeshProUGUI nameText; // Text untuk menampilkan nama objek
    public TextMeshProUGUI shortDescriptionText; // Text untuk menampilkan deskripsi singkat objek
    public TextMeshProUGUI detailText; // Text untuk menampilkan detail objek
    public TextMeshProUGUI detailNameText; // Text untuk menampilkan nama objek pada panel detail
    public GameObject detailPanel; // Panel untuk menampilkan detail objek
    public Button detailButton; // Tombol untuk menampilkan detail
    public Button backButton; // Tombol untuk kembali ke panel sebelumnya
    public Image[] indicators; // Array untuk menyimpan referensi ke titik-titik
    public Color activeColor = new Color(0, 0, 0, 0.5f); // Warna hitam transparan untuk slot aktif
    public Color inactiveColor = Color.white; // Warna tidak aktif untuk slot
    public Color indicatorActiveColor = Color.green; // Warna aktif untuk indikator bulat
    public Color indicatorInactiveColor = Color.white; // Warna tidak aktif untuk indikator bulat
    private int currentIndex = 0; // Indeks slot saat ini
    public Vector2 activeSlotScale = new Vector2(1.2f, 1.2f); // Skala slot aktif
    public Vector2 inactiveSlotScale = new Vector2(1.0f, 1.0f); // Skala slot tidak aktif
    public Vector2 activeIndicatorSize = new Vector2(20, 10); // Ukuran indikator aktif
    public Vector2 inactiveIndicatorSize = new Vector2(10, 10); // Ukuran indikator tidak aktif

    void Start()
    {
        // Pastikan panjang array sesuai
        if (slots.Length != objects.Length || objects.Length != objectNames.Length || objectNames.Length != objectDetails.Length || objectNames.Length != objectShortDescriptions.Length)
        {
            Debug.LogError("Array lengths are not equal. Please ensure all arrays have the same length.");
            Debug.LogError($"Slots: {slots.Length}, Objects: {objects.Length}, ObjectNames: {objectNames.Length}, ObjectDetails: {objectDetails.Length}, ObjectShortDescriptions: {objectShortDescriptions.Length}");
            return;
        }

        UpdateSlots();
        UpdateIndicators();
        ShowObject(currentIndex);
        UpdateObjectName(currentIndex);
        UpdateObjectShortDescription(currentIndex);

        // Tambahkan listener untuk tombol detail
        detailButton.onClick.AddListener(ShowDetailPanel);
        detailPanel.SetActive(false); // Panel tidak aktif secara default

        // Tambahkan listener untuk tombol kembali
        backButton.onClick.AddListener(HideDetailPanel);
    }

    // Fungsi untuk memperbarui warna dan ukuran slot
    void UpdateSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Image img = slots[i].GetComponent<Image>();
            if (img != null)
            {
                img.color = (i == currentIndex) ? activeColor : inactiveColor;
                slots[i].transform.localScale = (i == currentIndex) ? activeSlotScale : inactiveSlotScale;
            }
        }
    }

    // Fungsi untuk memperbarui warna dan ukuran indikator
    void UpdateIndicators()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].color = (i == currentIndex) ? indicatorActiveColor : indicatorInactiveColor;
            RectTransform rt = indicators[i].GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.sizeDelta = (i == currentIndex) ? activeIndicatorSize : inactiveIndicatorSize;
            }
        }
    }

    // Fungsi untuk menampilkan objek di kotak besar
    void ShowObject(int index)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == index);
        }
    }

    // Fungsi untuk memperbarui nama objek
    void UpdateObjectName(int index)
    {
        if (index >= 0 && index < objectNames.Length)
        {
            nameText.text = objectNames[index];
        }
        else
        {
            Debug.LogError($"Index out of bounds for objectNames array. index is {index}");
        }
    }

    // Fungsi untuk memperbarui deskripsi singkat objek
    void UpdateObjectShortDescription(int index)
    {
        if (index >= 0 && index < objectShortDescriptions.Length)
        {
            shortDescriptionText.text = objectShortDescriptions[index];
        }
        else
        {
            Debug.LogError($"Index out of bounds for objectShortDescriptions array. index is {index}");
        }
    }

    // Fungsi untuk menampilkan panel detail objek
    void ShowDetailPanel()
    {
        if (currentIndex >= 0 && currentIndex < objectDetails.Length)
        {
            detailText.text = objectDetails[currentIndex];
            detailNameText.text = objectNames[currentIndex]; // Update nama karakter pada panel detail
            detailPanel.SetActive(true);
        }
        else
        {
            Debug.LogError($"Index out of bounds for objectDetails array.");
        }
    }

    // Fungsi untuk menyembunyikan panel detail objek
    void HideDetailPanel()
    {
        detailPanel.SetActive(false);
    }

    // Fungsi untuk pindah ke slot sebelumnya
    public void PreviousSlot()
    {
        currentIndex = (currentIndex - 1 + slots.Length) % slots.Length;
        Debug.Log($"PreviousSlot: {currentIndex}");
        UpdateSlots();
        UpdateIndicators();
        ShowObject(currentIndex);
        UpdateObjectName(currentIndex);
        UpdateObjectShortDescription(currentIndex);
    }

    // Fungsi untuk pindah ke slot berikutnya
    public void NextSlot()
    {
        currentIndex = (currentIndex + 1) % slots.Length;
        Debug.Log($"NextSlot: {currentIndex}");
        UpdateSlots();
        UpdateIndicators();
        ShowObject(currentIndex);
        UpdateObjectName(currentIndex);
        UpdateObjectShortDescription(currentIndex);
    }

    // Fungsi untuk mengubah kode warna hex menjadi Color
    Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError($"Invalid hex color code: {hex}");
            return Color.white;
        }
    }
}
