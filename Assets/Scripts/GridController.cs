using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField][Range(1, 100)] private int gridX;
    [SerializeField][Range(1, 100)] private int gridY;

    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] private Vector2 spawnerOffset;

    [SerializeField] public Gem[] gemList;

    [HideInInspector] private List<List<GemSpawner>> grid;

    private void Awake()
    {
        startGrid();
    }

    private void createGrid()
    {
        if(grid == null)
            grid = new List<List<GemSpawner>>();

        if(this.grid.Count > gridX)
        {
            for(int x = 0; x < gridX; x++){ 
                if(this.grid[x].Count < gridY)
                {
                    this.grid[x].GetRange(gridY, this.grid[x].Count - gridY).ForEach((go) => {
                        if (Application.isEditor)
                            DestroyImmediate(go.gameObject);
                        else
                            Destroy(go.gameObject);
                    });

                    this.grid[x] = this.grid[x].GetRange(0, gridY);
                }
                else
                {
                    for(int i = this.grid[x].Count; i < gridY; i++)
                        this.grid[x].Add(Instantiate(spawnerPrefab, this.transform).GetComponent<GemSpawner>());
                }
                    
            }
            
            for(int x = gridX; x < this.grid.Count; x++)
                for(int y = 0; y < this.grid[x].Count; y++)
                {
                    if (Application.isEditor)
                        DestroyImmediate(this.grid[x][y].gameObject);
                    else
                        Destroy(this.grid[x][y].gameObject);
                }

            this.grid = this.grid.GetRange(0, gridX);
        }
        else
        {
            for (int x = 0; x < this.grid.Count; x++)
            {
                if (this.grid[x].Count < gridY)
                {
                    this.grid[x].GetRange(gridY, this.grid[x].Count - gridY).ForEach((go) => {
                        if (Application.isEditor)
                            DestroyImmediate(go.gameObject);
                        else
                            Destroy(go.gameObject);
                    });

                    this.grid[x] = this.grid[x].GetRange(0, gridY);
                }
                else
                {
                    for (int i = this.grid[x].Count; i < gridY; i++)
                        this.grid[x].Add(Instantiate(spawnerPrefab, this.transform).GetComponent<GemSpawner>());
                }

            }

            for(int x = this.grid.Count; x < gridX; x++)
            {
                List<GemSpawner> list = new List<GemSpawner>();
                for (int y = 0; y < gridY; y++)
                    list.Add(Instantiate(spawnerPrefab, this.transform).GetComponent<GemSpawner>());
                this.grid.Add(list);
            }
                
        }
    }

    private void placeGrid()
    {
        for (int x = 0; x < this.grid.Count; x++)
            for (int y = 0; y < this.grid[x].Count; y++)
                this.grid[x][y].transform.localPosition = new Vector3(this.spawnerOffset.x * (x - (this.grid.Count / 2)), 0, this.spawnerOffset.y * (y - (this.grid[x].Count / 2)));
    }

    private void startGrid()
    {
        for (int x = 0; x < this.grid.Count; x++)
            for (int y = 0; y < this.grid[x].Count; y++)
            {
                this.grid[x][y].masterGrid = this;
                this.grid[x][y].StartWithGem();
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnValidate()
    {
        foreach (Transform tra in this.transform)
                DestroyGO(tra.gameObject);
        

        grid = new List<List<GemSpawner>>();

        createGrid();
        placeGrid();
    }

    private void DestroyGO(GameObject gameObject)
    {
        IEnumerator _destroy(GameObject gameObject)
        {
            yield return new WaitForEndOfFrame();
            DestroyImmediate(gameObject);
        }
        StartCoroutine(_destroy(gameObject));
    }
}
