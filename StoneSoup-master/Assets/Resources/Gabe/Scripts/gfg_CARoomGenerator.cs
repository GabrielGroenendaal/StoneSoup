using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_CARoomGenerator : Room
{
    public int numOfWalls;
    public int numOfDirects;
    public GameObject directTile;

     public override void fillRoom(LevelGenerator ourGenerator, ExitConstraint requiredExits) {
        for (int x = 0; x < LevelGenerator.ROOM_WIDTH; x++) {
            for (int y = 0; y < LevelGenerator.ROOM_HEIGHT; y++) {
                
                // figure out if this location is at the edge of the room.
                bool isEdgeLocation = x == 0
                    || y == 0
                    || x == LevelGenerator.ROOM_WIDTH - 1
                    || y == LevelGenerator.ROOM_HEIGHT - 1;
                if (!isEdgeLocation ){
                    if (Random.Range(0f,1f) < .10f){
                        Tile.spawnTile(directTile, transform, x, y);
                    }
                }
                if (isEdgeLocation && !isExitLocation(x, y, requiredExits)) {
                    Tile.spawnTile(ourGenerator.normalWallPrefab, transform, x, y);
                } 
            }
        }

    }

    public bool isExitLocation(int x, int y, ExitConstraint requiredExits) {

        int upExitX = LevelGenerator.ROOM_WIDTH / 2;
        int upExitY = LevelGenerator.ROOM_HEIGHT - 1;

        int rightExitX = LevelGenerator.ROOM_WIDTH - 1;
        int rightExitY = LevelGenerator.ROOM_HEIGHT / 2;

        int downExitX = LevelGenerator.ROOM_WIDTH / 2;
        int downExitY = 0;

        int leftExitX = 0;
        int leftExitY = LevelGenerator.ROOM_HEIGHT / 2;

        if (requiredExits.upExitRequired && x == upExitX && y == upExitY) {
            return true;
        }
        if (requiredExits.rightExitRequired && x == rightExitX && y == rightExitY) {
            return true;
        }
        if (requiredExits.downExitRequired && x == downExitX && y == downExitY) {
            return true;
        }
        if (requiredExits.leftExitRequired && x == leftExitX && y == leftExitY) {
            return true;
        }

        return false;

    }

}
