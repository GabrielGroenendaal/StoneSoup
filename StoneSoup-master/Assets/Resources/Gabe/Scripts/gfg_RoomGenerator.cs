using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_RoomGenerator : Room {

    
    //public GameObject[] roomChoices;

    public GameObject[] roomChoices;

    public override Room createRoom(ExitConstraint requiredExits) {
		GameObject roomPrefab = GlobalFuncs.randElem(roomChoices);
		return roomPrefab.GetComponent<Room>().createRoom(requiredExits);
	}

    

}