using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpRow : MonoBehaviour
{
    [HideInInspector] private Gem self;

    [SerializeField] private Image gemImage;
    [SerializeField] private TMPro.TMP_Text gemName;
    [SerializeField] private TMPro.TMP_Text gemCount;

    public void SetGem(Gem gem)
    {
        self = gem;

        this.gemImage.sprite = self.GemSprite;
        this.gemName.text = self.GemName;
        this.gemCount.text = "Collected Gem Count: " + PlayerPrefs.GetInt(self.GemName, 0);
    }
}
