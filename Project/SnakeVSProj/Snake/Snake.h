#pragma once


class Snake
{
public:
    Snake(IntVector2 movementDirection, int maxBodyPieceCount, IntVector2* snakeBody);

    virtual ~Snake();

    void SetNewDirection(IntVector2 dir);

    void UpdateBodyPosition();

    int Expand();

    bool CheckCollision(IntVector2 index);

private:
    IntVector2 m_moveDirection;
    IntVector2* m_SnakeBody;
    int m_maxCellCount;
    int m_currentCellCount;
};

