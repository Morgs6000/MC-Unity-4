using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelColor {
    public static Color GetColor(EnumVoxels voxelID, int voxelSide) {
        // GRASS
        if(voxelID == EnumVoxels.grass) {
            if(voxelSide == (int)VoxelSide.TOP) {
                //string hex = "#79C05A"; // Foreste Biome
                string hex = "#59C93C"; // Jungle Biome
                Color color;

                if(ColorUtility.TryParseHtmlString(hex, out color)) {
                    return color;
                }
            }

            return Color.white;
        }

        return Color.white;
    }
}
