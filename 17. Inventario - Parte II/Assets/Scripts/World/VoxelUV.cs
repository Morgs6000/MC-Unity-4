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

        // DIRT
        if(voxelID == EnumVoxels.dirt) {
            return new Vector2(2, 0);
        }

        // COOBBLESTONE
        if(voxelID == EnumVoxels.cobblestone) {
            return new Vector2(0, 1);
        }

        // PLANKS
        if(voxelID == EnumVoxels.planks) {
            return new Vector2(4, 0);
        }

        // SAPLING
        if(voxelID == EnumVoxels.sapling) {
            return new Vector2(15, 0);
        }

        // BEDROCK
        if(voxelID == EnumVoxels.bedrock) {
            return new Vector2(1, 1);
        }

        // FLOWING WATER
        // WATER
        // FLOWING LAVA
        // LAVA

        // SAND
        if(voxelID == EnumVoxels.sand) {
            return new Vector2(2, 1);
        }

        // GRAVEL
        if(voxelID == EnumVoxels.gravel) {
            return new Vector2(3, 1);
        }

        // GOLD ORE
        if(voxelID == EnumVoxels.gold_ore) {
            return new Vector2(0, 2);
        }

        // IRON ORE
        if(voxelID == EnumVoxels.iron_ore) {
            return new Vector2(1, 2);
        }

        // COAL ORE
        if(voxelID == EnumVoxels.coal_ore) {
            return new Vector2(2, 2);
        }

        // LOG
        if(voxelID == EnumVoxels.log) {
            if(
                voxelSide == (int)VoxelSide.TOP || 
                voxelSide == (int)VoxelSide.BOTTOM
            ) {
                return new Vector2(5, 1);
            }

            return new Vector2(4, 1);
        }

        // LEAVES
        if(voxelID == EnumVoxels.leaves) {
            return new Vector2(5, 3);
        }

        //...

        // DESTROY
        if(voxelID == EnumVoxels.destroy_0) {
            return new Vector2(0, 15);
        } 
        if(voxelID == EnumVoxels.destroy_1) {
            return new Vector2(1, 15);
        } 
        if(voxelID == EnumVoxels.destroy_2) {
            return new Vector2(2, 15);
        }
        if(voxelID == EnumVoxels.destroy_3) {
            return new Vector2(3, 15);
        }
        if(voxelID == EnumVoxels.destroy_4) {
            return new Vector2(4, 15);
        }
        if(voxelID == EnumVoxels.destroy_5) {
            return new Vector2(5, 15);
        }
        if(voxelID == EnumVoxels.destroy_6) {
            return new Vector2(6, 15);
        }
        if(voxelID == EnumVoxels.destroy_7) {
            return new Vector2(7, 15);
        }
        if(voxelID == EnumVoxels.destroy_8) {
            return new Vector2(8, 15);
        }
        if(voxelID == EnumVoxels.destroy_9) {
            return new Vector2(9, 15);
        }

        return default;
    }
}
