using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_validatedRoom : Room
{
    public bool has_up_exit;
    public bool has_right_exit;
    public bool has_down_exit;
    public bool has_left_exit;

    public bool has_up_right_path;
    public bool has_up_left_path;
    public bool has_up_down_path;
    public bool has_left_right_path;
    public bool has_down_right_path;
    public bool has_down_left_path;


       public void validateRoom() {
        int[,] indexGrid = loadIndexGrid();

        Vector2Int upExit = new Vector2Int(LevelGenerator.ROOM_WIDTH / 2, LevelGenerator.ROOM_HEIGHT - 1);
        Vector2Int rightExit = new Vector2Int(LevelGenerator.ROOM_WIDTH - 1, LevelGenerator.ROOM_HEIGHT / 2);
        Vector2Int downExit = new Vector2Int(LevelGenerator.ROOM_WIDTH / 2, 0);
        Vector2Int leftExit = new Vector2Int(0, LevelGenerator.ROOM_HEIGHT / 2);

        has_up_exit = canOccupyPoint(indexGrid, upExit);
        has_right_exit = canOccupyPoint(indexGrid, rightExit);
        has_down_exit = canOccupyPoint(indexGrid, downExit);
        has_left_exit = canOccupyPoint(indexGrid, leftExit);

        has_up_right_path = doesPathExist(indexGrid, upExit, rightExit);
        has_up_down_path = doesPathExist(indexGrid, upExit, downExit);
        has_up_left_path = doesPathExist(indexGrid, upExit, leftExit);
        has_down_right_path = doesPathExist(indexGrid, rightExit, downExit);
        has_left_right_path = doesPathExist(indexGrid, rightExit, leftExit);
        has_down_left_path = doesPathExist(indexGrid, downExit, leftExit);

    }

    public int[,] loadIndexGrid() {
        string initialGridString = designedRoomFile.text;
        string[] rows = initialGridString.Trim().Split('\n');
        int width = rows[0].Trim().Split(',').Length;
        int height = rows.Length;
        if (height != LevelGenerator.ROOM_HEIGHT) {
            throw new UnityException(string.Format("Error in room by {0}. Wrong height, Expected: {1}, Got: {2}", roomAuthor, LevelGenerator.ROOM_HEIGHT, height));
        }
        if (width != LevelGenerator.ROOM_WIDTH) {
            throw new UnityException(string.Format("Error in room by {0}. Wrong width, Expected: {1}, Got: {2}", roomAuthor, LevelGenerator.ROOM_WIDTH, width));
        }
        int[,] indexGrid = new int[width, height];
        for (int r = 0; r < height; r++) {
            string row = rows[height - r - 1];
            string[] cols = row.Trim().Split(',');
            for (int c = 0; c < width; c++) {
                indexGrid[c, r] = int.Parse(cols[c]);
            }
        }
        return indexGrid;
    }

    public bool inGrid(Vector2Int point) {
        return point.x >= 0 && point.x < LevelGenerator.ROOM_WIDTH && point.y >= 0 && point.y < LevelGenerator.ROOM_HEIGHT;
    }

    public bool isEmptyPoint(int[,] indexGrid, Vector2Int point) {
        return (indexGrid[point.x, point.y] == 0 || indexGrid[point.x, point.y] == 4);
    }

    public bool canOccupyPoint(int[,] indexGrid, Vector2Int point) {
        return inGrid(point) && isEmptyPoint(indexGrid, point);
    }

    public bool doesPathExist(int[,] indexGrid, Vector2Int startPoint, Vector2Int endPoint) {

        List<Vector2Int> frontier = new List<Vector2Int>();
        List<Vector2Int> closed = new List<Vector2Int>();

        if (canOccupyPoint(indexGrid, startPoint)) {
            frontier.Add(startPoint);
        }

        while (frontier.Count > 0) {

            Vector2Int currentPoint = frontier[0];
            frontier.RemoveAt(0);
            if (currentPoint == endPoint) {
                return true; // We found a path, hooray!
            }
            closed.Add(currentPoint);

            // Add all the neighbors we can reach to the frontier. 
            Vector2Int upNeighbor = new Vector2Int(currentPoint.x, currentPoint.y + 1);
            if (canOccupyPoint(indexGrid, upNeighbor)
                && !closed.Contains(upNeighbor)
                && !frontier.Contains(upNeighbor)) {
                frontier.Add(upNeighbor);
            }

            Vector2Int rightNeighbor = new Vector2Int(currentPoint.x + 1, currentPoint.y);
            if (canOccupyPoint(indexGrid, rightNeighbor)
                && !closed.Contains(rightNeighbor)
                && !frontier.Contains(rightNeighbor)) {
                frontier.Add(rightNeighbor);
            }

            Vector2Int downNeighbor = new Vector2Int(currentPoint.x, currentPoint.y - 1);
            if (canOccupyPoint(indexGrid, downNeighbor)
                && !closed.Contains(downNeighbor)
                && !frontier.Contains(downNeighbor)) {
                frontier.Add(downNeighbor);
            }

            Vector2Int leftNeighbor = new Vector2Int(currentPoint.x - 1, currentPoint.y);
            if (canOccupyPoint(indexGrid, leftNeighbor)
                && !closed.Contains(leftNeighbor)
                && !frontier.Contains(leftNeighbor)) {
                frontier.Add(leftNeighbor);
            }
        }

        // If the frontier was emptied and we never found our target, there is no path! 
        return false;
    }
    public bool obeysExitConstraints(ExitConstraint constraint){
        if (constraint.upExitRequired && !has_up_exit){
            return false;
        }
        if (constraint.rightExitRequired && !has_right_exit){
            return false;
        }
        if (constraint.leftExitRequired && !has_left_exit){
            return false;
        }
        if (constraint.downExitRequired && !has_down_exit){
            return false;
        }
        if (constraint.upExitRequired && constraint.rightExitRequired && !has_up_right_path){
            return false;
        }
        if (constraint.upExitRequired && constraint.downExitRequired && !has_up_down_path){
            return false;
        }
        if (constraint.upExitRequired && constraint.leftExitRequired && !has_up_left_path){
            return false;
        }
        if (constraint.leftExitRequired && constraint.rightExitRequired && !has_left_right_path){
            return false;
        }
        if (constraint.downExitRequired && constraint.rightExitRequired && !has_down_right_path){
            return false;
        }
        if (constraint.downExitRequired && constraint.leftExitRequired && !has_down_left_path){
            return false;
        }
        return true;
    }
}
