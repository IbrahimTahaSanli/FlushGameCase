using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [HideInInspector] private Gem _self;
    [HideInInspector] public Gem self { get => _self; set {
            _self = value;
        }
    }

    [SerializeField] private GameObject gemPrefab;
    [HideInInspector] private GemScript currentObject;
    [HideInInspector] public GridController masterGrid;

    public void StartWithGem()
    {
        if(currentObject == null)
            currentObject = Instantiate(gemPrefab).GetComponent<GemScript>();

        self = this.masterGrid.gemList[Random.Range(0, this.masterGrid.gemList.Length)];

        currentObject.gemSelf = self;

        currentObject.SetPosition(this.transform.position);
        currentObject.StartGrow();
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                if (this.currentObject.IsGrown())
                {
                    this.currentObject.StopGrow();
                    CharacterController.ins.AddCollectedGem(this.currentObject);
                    this.currentObject = null;
                    StartWithGem();
                }
                break;
        }
    }
}
