using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private List<Gem> gemsToShow;
    [SerializeField] private PopUpRow rowPrefab;
    [HideInInspector] private List<PopUpRow> rows = new List<PopUpRow>();

    [SerializeField] private Transform scrollContent;

    private void OnEnable()
    {
        if(gemsToShow.Count > rows.Count)
            for (int i = rows.Count; i < gemsToShow.Count; i++)
                this.rows.Add(Instantiate(rowPrefab, scrollContent).GetComponent<PopUpRow>());
        else
            this.rows.GetRange(gemsToShow.Count, rows.Count - gemsToShow.Count);

        for(int i = 0; i < rows.Count; i++)
            this.rows[i].SetGem(gemsToShow[i]);
    }
}
