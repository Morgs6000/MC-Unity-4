using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData {
    public static Vector3[,] vertices = new Vector3[,] {
        /* TOP */ {
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(1, 1, 0)
        }
    };

    public static int[] triangles = new int[] {
        // Primeiro Triangulo
        0, 1, 2, 
        
        // Segundo Triangulo
        0, 2, 3
    };
}
