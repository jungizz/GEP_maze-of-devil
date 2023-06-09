using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenDoor : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase closeDoorTile1;
    public TileBase closeDoorTile2;
    public TileBase closeDoorTile3;
    public TileBase closeDoorTile4;
    public TileBase openDoorTile1;
    public TileBase openDoorTile2;
    public TileBase openDoorTile3;
    public TileBase openDoorTile4;

    public bool isOpen;

    private void Update()
    {
        if (isOpen)
        {
            tilemap.SwapTile(closeDoorTile1, openDoorTile1);
            tilemap.SwapTile(closeDoorTile2, openDoorTile2);
            tilemap.SwapTile(closeDoorTile3, openDoorTile3);
            tilemap.SwapTile(closeDoorTile4, openDoorTile4);
            isOpen = false;
            
        }
    }
}
