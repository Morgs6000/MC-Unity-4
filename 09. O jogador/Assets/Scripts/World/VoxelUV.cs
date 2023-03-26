using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelUV {
    public static Vector2 GetUV(EnumVoxels voxelID, int voxelSide) {
        // STONE
        if(voxelID == EnumVoxels.stone) {
            return new Vector2(1, 0);
        }

        // GRASS
        if(voxelID == EnumVoxels.grass) {
            if(voxelSide == (int)VoxelSide.TOP) {
                return new Vector2(0, 0);
            }
            if(voxelSide == (int)VoxelSide.BOTTOM) {
                return new Vector2(2, 0);
            }
                
            return new Vector2(3, 0);
        }

        return default;
    }
}
