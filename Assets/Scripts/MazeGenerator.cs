#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    [MenuItem("MazeRunner/Build Maze")]
    public static void BuildMaze()
    {
        // ── GRID DEFINITION ──
        // 0 = wall, 1 = open corridor
        // 27 rows x 27 cols
        // Read left-to-right = West-to-East (X axis)
        // Read top-to-bottom = North-to-South (Z axis)
        // Center cell [13][13] = Unity (0,0,0)

        int[,] grid = new int[27, 27]
        {
            // col: 0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26
            /* r0  */{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* r1  */{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, // P1 top
            /* r2  */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r3  */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r4  */{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, // P2
            /* r5  */{0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r6  */{0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r7  */{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, // P3
            /* r8  */{0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r9  */{0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r10 */{0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r11 */{0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r12 */{0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r13 */{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // P4 CENTER + entry/exit
            /* r14 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            /* r15 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            /* r16 */{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, // P5
            /* r17 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r18 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r19 */{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, // P6
            /* r20 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r21 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r22 */{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, // P7
            /* r23 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r24 */{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            /* r25 */{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, // P8 bottom
            /* r26 */{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };

        // Special cells (on top of open corridors)
        // Start:      row13, col1  (Unity X=-12, Z=0)
        // Finish:     row13, col25 (Unity X=+12, Z=0)
        // Checkpoint: row13, col13 (Unity X=0,   Z=0)
        // Obstacle1:  row4,  col16 (Unity X=3,   Z=-9) — on P2
        // Obstacle2:  row16, col10 (Unity X=-3,  Z=3)  — on P5
        // Obstacle3:  row22, col16 (Unity X=3,   Z=9)  — on P7
        // AI:         row13, col8  (Unity X=-5,  Z=0)  — on P4 center highway

        // ── DESTROY OLD MAZE ──
        GameObject old = GameObject.Find("Maze");
        if (old != null) DestroyImmediate(old);

        GameObject maze = new GameObject("Maze");

        // ── MATERIALS ──
        Material wallMat   = MakeMat(new Color32( 60,  80, 120, 255)); // blue-grey wall
        Material floorMat  = MakeMat(new Color32( 30,  30,  35, 255)); // near-black floor
        Material startMat  = MakeMat(new Color32(  0, 180,  80, 255)); // green
        Material finishMat = MakeMat(new Color32(210,  30,  30, 255)); // red
        Material checkMat  = MakeMat(new Color32(240, 180,  10, 255)); // yellow
        Material obsMat    = MakeMat(new Color32(200,  40,  40, 255)); // red obstacle
        Material aiMat     = MakeMat(new Color32(120,  40, 220, 255)); // purple AI

        int ROWS = 27, COLS = 27;
        int centerRow = 13, centerCol = 13;

        // ── BUILD WALLS FROM GRID ──
        // Instead of placing one cube per wall cell (slow),
        // we merge adjacent wall runs into single long cubes.

        // Horizontal runs (scan each row)
        for (int r = 0; r < ROWS; r++)
        {
            int c = 0;
            while (c < COLS)
            {
                if (grid[r, c] == 0)
                {
                    // find run length
                    int start = c;
                    while (c < COLS && grid[r, c] == 0) c++;
                    int length = c - start;
                    float midCol = start + (length - 1) / 2.0f;
                    float ux = midCol - centerCol;
                    float uz = r - centerRow;

                    // Only place if this is a purely horizontal internal run
                    // We use a simple approach: place all walls as individual cubes
                    // but skip — instead we do merged runs below
                }
                else c++;
            }
        }

        // Simpler & more accurate: place each wall cell as its own cube
        // Then Unity batching handles performance fine for this size
        for (int r = 0; r < ROWS; r++)
        {
            for (int c = 0; c < COLS; c++)
            {
                if (grid[r, c] == 0)
                {
                    float ux = c - centerCol;
                    float uz = r - centerRow;
                    Cube(maze, $"W_{r}_{c}",
                        new Vector3(ux, 0.5f, uz),
                        new Vector3(1, 1, 1),
                        wallMat);
                }
            }
        }

        // ── FLOOR ──
        Cube(maze, "Floor",
            new Vector3(0, -0.01f, 0),
            new Vector3(27, 0.02f, 27),
            floorMat);

        // ── START ZONE ──
        Zone(maze, "StartZone",
            new Vector3(-12f, 0.02f, 0f),
            new Vector3(1f, 0.02f, 3f),
            startMat);

        // ── FINISH ZONE ──
        Zone(maze, "FinishZone",
            new Vector3(12f, 0.02f, 0f),
            new Vector3(1f, 0.02f, 3f),
            finishMat);

        // ── CHECKPOINT ── on center highway row13 col13 = (0,0)
        Zone(maze, "Checkpoint",
            new Vector3(0f, 0.02f, 0f),
            new Vector3(1f, 0.02f, 1f),
            checkMat);

        // ── STATIC OBSTACLES ── placed on open corridor cells
        Obstacle(maze, "Obstacle_Static_1", new Vector3( 3f, 0.5f, -9f), obsMat); // row4 col16
        Obstacle(maze, "Obstacle_Static_2", new Vector3(-3f, 0.5f,  3f), obsMat); // row16 col10
        Obstacle(maze, "Obstacle_Static_3", new Vector3( 3f, 0.5f,  9f), obsMat); // row22 col16

        // ── AI OBSTACLE ── on center highway
        Obstacle(maze, "Obstacle_AI", new Vector3(-5f, 0.5f, 0f), aiMat);

        Debug.Log("✅ Maze built from grid — exact match to layout map!");
    }

    static Material MakeMat(Color32 color)
    {
        var m = new Material(Shader.Find("Standard"));
        m.color = color;
        return m;
    }

    static void Cube(GameObject parent, string name, Vector3 pos, Vector3 scale, Material mat)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.position   = pos;
        obj.transform.localScale = scale;
        obj.transform.SetParent(parent.transform);
        obj.GetComponent<Renderer>().material = mat;
    }

    static void Zone(GameObject parent, string name, Vector3 pos, Vector3 scale, Material mat)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.position   = pos;
        obj.transform.localScale = scale;
        obj.transform.SetParent(parent.transform);
        obj.GetComponent<Renderer>().material = mat;
        obj.GetComponent<BoxCollider>().isTrigger = true;
    }

    static void Obstacle(GameObject parent, string name, Vector3 pos, Material mat)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.position   = pos;
        obj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        obj.transform.SetParent(parent.transform);
        obj.GetComponent<Renderer>().material = mat;
        obj.tag = "Obstacle";
    }
}
#endif