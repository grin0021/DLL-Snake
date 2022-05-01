#pragma once


#ifdef SNAKE_EXPORTS 
#define PLUGIN_API __declspec(dllexport)
#else
#define PLUGIN_API __declspec(dllimport)
#endif

extern "C"
{
    PLUGIN_API void* Snake_Create(CIntVector2 movementDirection, int maxBodyPieceCount, CIntVector2* snakeBody);
    PLUGIN_API void Snake_Destroy(void* snakePtr);
    PLUGIN_API void Snake_SetNewDirection(CIntVector2 moveDir, void* snakePtr);
    PLUGIN_API void Snake_UpdateBodyPosition(void* snakePtr);
    PLUGIN_API int Snake_Expand(void* snakePtr);
    PLUGIN_API bool Snake_CheckCollision(CIntVector2 index, void* snakePtr);
}


