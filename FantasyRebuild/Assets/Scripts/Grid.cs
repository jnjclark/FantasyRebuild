using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width;
    public int height;
    public int cellSize;
    public Transform origin;

    public List<Vector2> vertices = new List<Vector2>();
    public char[,] buildingPlacement;       //cells marked o for occupied or e for empty

    #region Singleton
    public static Grid instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start()
    {
        //create array and fill with 'e'
        buildingPlacement = new char[width, height];

        for (int i = width - 1; i >= 0; i--)
        {
            for (int j = height - 1; j >= 0; j--)
            {
                buildingPlacement[i, j] = 'e';
            }
        }
    }

    public void SetOccupied(Vector2 transform, bool occupied)
    {
        Vector2 place = TransformToGrid(transform);
        int x = (int)place.x;
        int y = (int)place.y;

        //if the cell is occupied and we want to set it as empty
        if (GetOccupied(x, y) && !occupied)
        {
            buildingPlacement[x, y] = 'e';

            Debug.Log(buildingPlacement[x, y]);
        }

        //if the cell is empty and we want to set it as occupied
        else if (!GetOccupied(x, y) && occupied)
        {
            buildingPlacement[x, y] = 'o';
        }

        //if cell empty & set empty or cell occupied & set occupied, nothing happens
    }

    //check if a cell is occupied
    public bool GetOccupied(int x, int y)
    {
        if (buildingPlacement[x, y] == 'o') return true;            //occupied
        else if (buildingPlacement[x, y] == 'e') return false;      //empty
        else    
            Debug.LogError("buildingPlacement " + x.ToString() + ", " + y.ToString() + " not marked empty or occupied");    //should not happen
        return false;
    }

    public bool GetOccupied(Vector3 position)
    {
        Vector2 gridPos = TransformToGrid(position);

        return GetOccupied((int)gridPos.x, (int)gridPos.y);
    }

    //returns a random unoccupied cell
    public Vector2 RandomFreePlace()
    {
        bool found = false;
        int x = 0;
        int y = 0;

        while (found == false)
        {
            x = Random.Range(0, width + 1);     //+1 because max range is not inclusive
            y = Random.Range(0, height + 1);

            if (GetOccupied(x, y) == false)
                found = true;
        }

        return new Vector2(x, y);
    }

    public Vector2 RandomFreeTransform()
    {
        Vector2 place = RandomFreePlace();  //get a free cell
        place = GridToTransform(place);     //transform it to world space

        return place;
    }

    public Vector2 TransformToGrid(Vector2 transform)
    {
        int x = (int)((transform.x - origin.position.x) / cellSize);
        int y = (int)((transform.y - origin.position.y) / cellSize);
        
        //int x = Mathf.FloorToInt((transform.x - origin.position.x) / cellSize);
        //int y = Mathf.FloorToInt((transform.y - origin.position.y) / cellSize);
        return new Vector2(x, y);
    }

    public Vector2 GridToTransform(Vector2 grid)
    {
        float x = grid.x * cellSize + origin.position.x;
        float y = grid.y * cellSize + origin.position.y;
        return new Vector2(x, y);
    }

    //shows the grid in the editor while playing
    private void OnDrawGizmos()
    {
        foreach (Vector3 vertex in vertices)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(vertex, .1f);
        }
    }
    
    //make buildings sit on the grid when given a mouse click position
    public Vector2 GetGridSnapLocation(Vector2 origin)
    {
        return GetGridSnapLocation(origin, 1, 1);
    }
    public Vector2 GetGridSnapLocation(Vector2 origin, int width, int height)
    {
        Vector2 center = new Vector2(width / 2, height / 2);        //find center of placed object
        center.x -= 1;                                              //helps keep the actual center on the mouse
        center.y -= 1;
        origin -= center;                                           //clicked location (origin) is desired center of object, move origin to corner transform

        if (origin.x >= 0)                                           //doing math like this helps keep the building centered on the mouse in negative x or y position
            origin.x -= (origin.x % cellSize);
        else
            origin.x -= cellSize - (Mathf.Abs(origin.x) % cellSize);

        if (origin.y >= 0)
            origin.y -= (origin.y % cellSize);
        else
            origin.y -= cellSize - (Mathf.Abs(origin.y) % cellSize);

        origin -= new Vector2(1, 1);        //its doing something odd so just correct it

        return origin;
    }
}
