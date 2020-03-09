using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_validatedRoomPicker : Room
{
    public GameObject[] roomChoices;

    public override Room createRoom(ExitConstraint requiredExits){
        
        List<Room> roomsThatMeetConstraints = new List<Room>();

        foreach (GameObject roomPrefab in roomChoices){
            gfg_validatedRoom validateRoom = roomPrefab.GetComponent<gfg_validatedRoom>();

            if (validateRoom == null){
                throw new UnityException("tried to fuck up daddy");;
            }

            if (validateRoom.obeysExitConstraints(requiredExits)){{
                roomsThatMeetConstraints.Add(validateRoom);
            }}
        }

        /*if (roomsThatMeetConstraints.Count <= 0){
            
        }*/

        Room roomToCreate = GlobalFuncs.randElem(roomsThatMeetConstraints);
        return roomToCreate.createRoom(requiredExits);

    }
}
