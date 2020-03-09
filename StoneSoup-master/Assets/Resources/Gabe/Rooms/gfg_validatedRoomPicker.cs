using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class gfg_validatedRoomPicker : Room
{
    //public GameObject[] roomChoices;
    
    public GameObject[] roomOptions;

    public GameObject defaultRoomPrefab;

/*    void Start() {
        // Find all Texture2Ds that have 'co' in their filename, that are labelled with 'architecture' and are placed in 'MyAwesomeProps' folder
        mapChoices = Resources.LoadAll("Assets/Rooms/Editor", typeof(TextAsset));

        foreach (Object roomOrient in mapChoices){
            gfg_validatedRoom oneOption = defaultRoomPrefab.GetComponent<gfg_validatedRoom>();
            oneOption.designedRoomFile = (TextAsset)roomOrient;
            roomOptions.Add(oneOption);
        }
    }*/
    public override Room createRoom(ExitConstraint requiredExits){
        return defaultRoomPrefab.GetComponent<Room>().createRoom(requiredExits);
        /*List<Room> roomsThatMeetConstraints = new List<Room>();

        foreach (GameObject roomPrefab in roomOptions){
            gfg_validatedRoom validatedRoom = roomPrefab.GetComponent<gfg_validatedRoom>();
            if (roomPrefab == null){
                throw new UnityException("tried to fuck up daddy");;
            }
            validatedRoom.validateRoom();
            if (validatedRoom.obeysExitConstraints(requiredExits)){{
                roomsThatMeetConstraints.Add(validatedRoom);
            }}
        }

        if (roomsThatMeetConstraints.Count <= 0){
            return defaultRoomPrefab.GetComponent<Room>().createRoom(requiredExits);
        }

        Room roomToCreate = GlobalFuncs.randElem(roomsThatMeetConstraints);
        return roomToCreate.createRoom(requiredExits);*/

    }
}
