#include "pch.h"
#include "SnakeAPI.h"
#include "Snake.h"


PLUGIN_API void* Snake_Create(CIntVector2 movementDirection, int maxBodyPieceCount, CIntVector2* snakeBody)
{
    return new Snake(IntVector2(movementDirection.x, movementDirection.y), maxBodyPieceCount, (IntVector2*)snakeBody);
}

PLUGIN_API void Snake_Destroy(void* snakePtr)
{
    Snake* snake = static_cast<Snake*>(snakePtr);

    if (snake != nullptr)
    {
        delete snake;
        snake = nullptr;
    }
}

PLUGIN_API void Snake_SetNewDirection(CIntVector2 moveDir, void* snakePtr)
{
    Snake* snake = static_cast<Snake*>(snakePtr);

    snake->SetNewDirection(IntVector2(moveDir.x, moveDir.y));
}

PLUGIN_API void Snake_UpdateBodyPosition(void* snakePtr)
{
    Snake* snake = static_cast<Snake*>(snakePtr);

    snake->UpdateBodyPosition();
}

PLUGIN_API int Snake_Expand(void* snakePtr)
{
    Snake* snake = static_cast<Snake*>(snakePtr);

    return snake->Expand();
}

PLUGIN_API bool Snake_CheckCollision(CIntVector2 index, void* snakePtr)
{
    Snake* snake = static_cast<Snake*>(snakePtr);

    return snake->CheckCollision(IntVector2(index.x, index.y));
}

