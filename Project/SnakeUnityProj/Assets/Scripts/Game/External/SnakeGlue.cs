using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class SnakeGlue : IDisposable
{
    public SnakeGlue(IntVector2 movementDirection, int maxbodyPieceCount, IntVector2[] snakeBody)
    {
        Impl = Snake_Create(movementDirection, maxbodyPieceCount, snakeBody);
    }

    public void Dispose()
    {
        Snake_Destroy(Impl);
        Impl = IntPtr.Zero;
    }

    public void SetNewDirection(IntVector2 dir)
    {
        Snake_SetNewDirection(dir, Impl);
    }

    public IntVector2 GetDirection()
    {
        return Snake_GetDirection(Impl);
    }

    public void UpdateBodyPosition()
    {
        Snake_UpdateBodyPosition(Impl);
    }

    public int Expand()
    {
        return Snake_Expand(Impl);
    }

    public bool CheckCollision(IntVector2 index)
    {
        return Snake_CheckCollision(index, Impl);
    }

    public IntPtr Impl { get; private set; }

    [DllImport("SnakePlugin.dll")]
    static extern IntPtr Snake_Create(IntVector2 movementDirection, int maxbodyPieceCount, [MarshalAs(UnmanagedType.SafeArray)] IntVector2[] snakeBody);
    [DllImport("SnakePlugin.dll")]
    static extern void Snake_Destroy(IntPtr snake);
    [DllImport("SnakePlugin.dll")]
    static extern void Snake_SetNewDirection(IntVector2 dir, IntPtr snake);
    [DllImport("SnakePlugin.dll")]
    static extern IntVector2 Snake_GetDirection(IntPtr snake);
    [DllImport("SnakePlugin.dll")]
    static extern void Snake_UpdateBodyPosition(IntPtr snake);
    [DllImport("SnakePlugin.dll")]
    static extern int Snake_Expand(IntPtr snake);
    [DllImport("SnakePlugin.dll")]
    static extern bool Snake_CheckCollision(IntVector2 index, IntPtr snake);
}