using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    [HideInInspector] private Gem _gemSelf;
    [HideInInspector] public Gem gemSelf {
        get => _gemSelf;
        set { 
            _gemSelf = value;
            renderer.material = value.GemMaterial;
            filter.mesh = value.GemMesh;
        }
    }

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private MeshFilter filter;

    [HideInInspector] private Coroutine growCo = null;

    [HideInInspector] private float growScale = 0.0f;

    [HideInInspector] private Vector3 targetPosition;
    [HideInInspector] private Coroutine moveCo = null;
    [field: SerializeField] public float moveTimeInSecond { get; private set; }
    [SerializeField] private float jumpDistance;


    public void StartGrow()
    {
        if (this.growCo != null)
            StopCoroutine(this.growCo);

        this.growCo = StartCoroutine(growEnumurator());
    }

    public void StopGrow()
    {
        if (this.growCo != null)
            StopCoroutine(this.growCo);
    }

    private IEnumerator growEnumurator()
    {
        this.growScale = 0.0f;
        for(float t = 0; t < 5; t += Time.deltaTime * Time.timeScale)
        {
            this.transform.localScale = Vector3.one * t / 5;
            this.growScale = t / 5;
            yield return new WaitForEndOfFrame();
        }

    }

    public float GetPrice()
    {
        return this.gemSelf.GemBasePrice + 100.0f * this.growScale;
    }

    public bool IsGrown()
    {
        return (this.growScale > 0.25f);
    }


    public void GoToPosition(Vector3 pos)
    {
        if(this.moveCo != null)
        {
            StopCoroutine(this.moveCo);
        }

        this.targetPosition = pos;
        this.moveCo = StartCoroutine(move());

    }

    private IEnumerator move()
    {
        Vector3 startPos = this.transform.position;

        for (float t = 0; t < 1.0f; t += Time.deltaTime * Time.timeScale / moveTimeInSecond)
        {
            Vector3 jump = Vector3.zero;

            jump.y += Mathf.Sin(Mathf.PI * t) * jumpDistance;

            this.transform.position = Vector3.Lerp(startPos, this.targetPosition + jump, t);
            yield return new WaitForEndOfFrame();
        }

        this.moveCo = null;
    }

    public void SetPosition(Vector3 pos)
    {
        this.targetPosition = pos;
    }

    private void Update()
    {
        if (this.moveCo == null) { 
            this.transform.position = this.targetPosition;
        }
    }
}
