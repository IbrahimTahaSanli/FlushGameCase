using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public static CharacterController ins { get; private set; }

    [SerializeField] private Animator cont;
    [HideInInspector] private Vector2 contPos;

    [SerializeField] private Camera followCamera;
    [SerializeField] private Vector3 cameraOffset;

    [SerializeField] [Range(0.0f,100.0f)] private float speedMultiplier;

    [HideInInspector] private List<GemScript> collectedGems = new List<GemScript>();
    [SerializeField] private float distanceBetweenGems = 1.0f;
    

    private void Awake()
    {
        if(ins != null)
            DestroyImmediate(ins);
        else 
            ins = this;

    }


    public void SetContPos(Vector2 pos)
    {
        this.contPos = pos;
    }

    private void transformationUpdate()
    {
        this.transform.LookAt(this.transform.position + new Vector3(this.contPos.x, 0, this.contPos.y));
        this.transform.position += (this.transform.forward * this.contPos.magnitude) * speedMultiplier * Time.deltaTime * Time.timeScale;
    
        this.followCamera.transform.position = this.transform.position + this.cameraOffset;
    }

    private void setAnimParam()
    {
        cont.SetFloat("Speed", this.contPos.magnitude);
        cont.SetBool("IsMoving", this.contPos.magnitude > 0);
    }

    public void AddCollectedGem(GemScript script)
    {
        collectedGems.Add(script);
        script.GoToPosition((this.transform.position - this.transform.forward) + (this.transform.up * (collectedGems.Count - 1)));
    }

    public int CollectedGemCount() => collectedGems.Count;

    public GemScript GetCollectedGem()
    {
        GemScript scr = collectedGems[collectedGems.Count - 1];
        collectedGems.RemoveAt(collectedGems.Count - 1);
        return scr;
    }

    // Update is called once per frame
    void Update()
    {
        this.setAnimParam();
        this.transformationUpdate();

        for(int i = 0; i < this.collectedGems.Count; i++)
        {
            this.collectedGems[i].SetPosition((this.transform.position - this.transform.forward) + (this.transform.up * i));
        }

    }
}
