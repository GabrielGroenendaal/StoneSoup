using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class gfg_RevisedRoomGenerator : MonoBehaviour

{
    // Start is called before the first frame update

    public int numOfRooms = 100;
    public int numSteps = 5;

    // CA Variables
    public int starvationLimit = 4; public int DirectstarvationLimit = 1;
    public int overpopLimit = 5; public int DirectoverpopLimit = 3;
    public int birthLimit = 3; public int DirectbirthLimit = 3;
    public float chanceToStartAlive_body = 0.35f;
    public float chanceToStartAlive_walls = 0.85f;
    //public float chanceToGetTiles_X = .1f;
    //public float chanceToGetTiles_Y = .1f;

    public int[,] grid = new int[8,10];
    public int[,] newGrid = new int[8,10];

    void Start()
    {
        for (int i = 0; i < numOfRooms; i++){
            overpopLimit = Random.Range(4,5);
            createTextFile(GenerateRoom(), (i+1).ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space)){
            //createTextFile(GenerateRoom(),numSteps.ToString());
            //numSteps++;
       // }
    }

    public int[,] GenerateRoom() {

        ClearGrid(grid); ClearGrid(newGrid);

        for (int x = 0; x < grid.GetLength(0); x++){
            for (int y = 0; y < grid.GetLength(1); y++){

                if (x <= 1 || y <= 1 || x >= 8 || y >= 6){
                    if (Random.Range(0f,1f) < chanceToStartAlive_walls){
                        grid[x,y] = 1;
                    }
                    else {
                        grid[x,y] = 0;
                    }
                }
                else {
                    if (Random.Range(0f,1f) < chanceToStartAlive_body){
                        grid[x,y] = 1;
                    }
                    else {
                        grid[x,y] = 0;
                    }
                }
            }
        }

        for (int m = 0; m < numSteps; m++){
            PerformCAPass();
        }

        addTiles();
 
        return grid;
    }


    public void PerformCAPass (){
        for (int x = 0; x < grid.GetLength(0); x++){
            for (int y = 0; y < grid.GetLength(1); y++){
                newGrid[x,y] = nextCAValue(x,y);
            }
        }
        copyGrid(newGrid,grid);
    }

    public int nextCAValue(int x, int y){

        int aliveNeighbors = CountAliveNeighbors(x,y);

         if (grid[x, y] == 1) //if wall is alive/a solid wall
            {
                if (aliveNeighbors < starvationLimit) //death limit is 3
                {
                    return 0;
                }
                else if (aliveNeighbors > overpopLimit)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        else //if wall is dead/empty
            {
                if (aliveNeighbors > birthLimit) //birth limit is 4
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
    }

    public int CountAliveNeighbors(int x, int y){
        int count = 0;
        for (int i = -1; i < 2; i++){
            for (int j = -1; j < 2; j++){
                int neighbor_x = x + i;
                int neighbor_y = y + j;

                if (i == 0 && j == 0){

                }
                else if (neighbor_x < 0 || neighbor_y < 0 || neighbor_x >= grid.GetLength(0) || neighbor_y >= grid.GetLength(1)){
                    count++;
                }
                else if (grid[neighbor_x,neighbor_y] == 1) {
                    count++;
                }
            }
        }
        return count;
    }

    public void addTiles(){
        //Debug.Log("Adding Tiles");
        float chanceToGetTiles_Direct = Random.Range(.1f,.2f);
        for (int x = 1; x < grid.GetLength(0) - 1; x++){
            for (int y = 1; y < grid.GetLength(1) - 1; y++){
                if (grid[x,y] == 0) {
                    newGrid[x,y] = placeTile(chanceToGetTiles_Direct, newGrid[x,y]);
                }
                else if (grid[x,y] == 1) {
                    if (Random.Range(0f,1f) < .08f){
                        newGrid[x,y] = 4;
                    }
                }
            }
        }
        copyGrid(newGrid,grid);
        PerformSecondCAPass();
    }

    public int placeTile(float chance, int value){
        
        if (Random.Range(0f, 1f) < chance){
            return 4;
        }
        /*else if (Random.Range(0f,1f) < .1f){
            return 5;
        }
        else if (Random.Range(0f,1f) < .1f){
            return 6;
        }*/
        else {
            return value;
        }
    }

    public void PerformSecondCAPass (){
        for (int x = 1; x < grid.GetLength(0) - 1; x++){
            for (int y = 1; y < grid.GetLength(1) - 1; y++){
                newGrid[x,y] = nextSecondCAValue(x,y,grid);
            }
        }
        copyGrid(newGrid,grid);
    }

    public int nextSecondCAValue(int x, int y, int[,] grid){
        int aliveNeighbors = direct_CountAliveNeighbors(x,y, grid);

         if (grid[x, y] == 4) //if wall is alive/a solid wall
            {
                if (aliveNeighbors < DirectstarvationLimit) //death limit is 3
                {
                    return 0;
                }
                else if (aliveNeighbors > DirectoverpopLimit)
                {
                    return 0;
                }
                else
                {
                    return 4;
                }
            }
        else if (grid[x,y] == 0) //if wall is dead/empty
            {
                if (aliveNeighbors > DirectbirthLimit) //birth limit is 4
                {
                    return 4;
                }
                else
                {
                    return 0;
                }
            }
        else {
            return grid[x,y];
        }
    }

    public int direct_CountAliveNeighbors(int x, int y, int[,] grid){
        int count = 0;
        for (int i = -1; i < 2; i++){
            for (int j = -1; j < 2; j++){
                int neighbor_x = x + i;
                int neighbor_y = y + j;

                if (i == 0 && j == 0){

                }
                else if (neighbor_x < 0 || neighbor_y < 0 || neighbor_x >= grid.GetLength(0) || neighbor_y >= grid.GetLength(1)){
                    count++;
                }
                else if (grid[neighbor_x,neighbor_y] == 4) {
                    count++;
                }
            }
        }
        return count;
    }
    
    public void createTextFile(int[,] grid, string identifier){
   
        string path = Application.dataPath + "/Resources/Gabe/Rooms/Editor/pregeneratedRoom_" + identifier + ".txt";
        string content = "";
        for (int x = 0; x < grid.GetLength(0); x++){
            for (int y = 0; y < grid.GetLength(1); y++){
                content = content + grid[x,y].ToString();

                if (y < grid.GetLength(1) - 1){
                    content = content + ",";
                }
            }
            if (x < grid.GetLength(0) - 1){
                content = content + "\n";
            }
        }
        
        if (!File.Exists(path)){
            File.WriteAllText(path, content);
        } else {
            File.Delete(path);
            File.WriteAllText(path, content);
        }
    }

    public void copyGrid(int[,] grid1, int[,] grid2){
        for (int x = 0; x < grid1.GetLength(0); x++) {
            for (int y = 0; y < grid1.GetLength(1); y++){
                grid2[x,y] = grid1[x,y];
            }
        }
    }

    public void ClearGrid(int[,] grid){
        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++){
                grid[x,y] = 0;
            }
        }
    }
}
