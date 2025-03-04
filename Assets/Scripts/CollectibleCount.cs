using UnityEngine;

public class CollectibleCount : MonoBehaviour
{

    TMPro.TMP_Text text;
    int count;
    public GameObject WinTrigger;

    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    void Start() => UpdateCount();

    void OnEnable() => Collectible.OnCollected += OnCollectibleCollected;
    void OnDisable() => Collectible.OnCollected -= OnCollectibleCollected;

    void OnCollectibleCollected()
    {
        count++;
        UpdateCount();
    }

    void UpdateCount()
    {
        text.text = $"{count} / {2}";
    }

    private void Update()
    {
        if (count == 2)
        {
            WinTrigger.SetActive(true);
        }
    }

}
