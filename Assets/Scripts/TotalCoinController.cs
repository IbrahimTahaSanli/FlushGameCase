using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalCoinController : MonoBehaviour
{
    [HideInInspector] public static TotalCoinController Instance;

    [SerializeField] private TMPro.TMP_Text text;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else 
            Instance = this;

        SetCoinText();
    }

    public void SetCoinText()
    {
        this.text.text = "Total coin: " + PlayerPrefs.GetFloat( SellPoint.MONEYPREFKEY, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
