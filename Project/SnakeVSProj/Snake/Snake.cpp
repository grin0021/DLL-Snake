#include "pch.h"
#include "Snake.h"


Snake::Snake(IntVector2 dir, int maxBodyPieceCount, IntVector2* snakeBody)
{
    m_moveDirection = dir;
    m_maxCellCount = maxBodyPieceCount;
    m_SnakeBody = snakeBody;
    m_currentCellCount = 1;
}

Snake::~Snake()
{
    m_SnakeBody = nullptr;
}

void Snake::SetNewDirection(IntVector2 dir)
{
    if (m_currentCellCount <= 1)
    {
        m_moveDirection = dir;
        return;
    }
    else
    {
        if (dir == IntVector2(0, 1) && m_moveDirection != IntVector2(0, -1))
        {
            m_moveDirection = dir;
        }
        else if (dir == IntVector2(0, -1) && m_moveDirection != IntVector2(0, 1))
        {
            m_moveDirection = dir;
        }
        else if (dir == IntVector2(1, 0) && m_moveDirection != IntVector2(-1, 0))
        {
            m_moveDirection = dir;
        }
        else if (dir == IntVector2(-1, 0) && m_moveDirection != IntVector2(1, 0))
        {
            m_moveDirection = dir;
        }
    }
}

void Snake::UpdateBodyPosition()
{
    IntVector2 index = m_SnakeBody[0];
    IntVector2 newIndex = index + m_moveDirection;

    if (newIndex.x > 19)
    {
        newIndex.x = 0;
    }

    if (newIndex.x < 0)
    {
        newIndex.x = 19;
    }

    if (newIndex.y > 19)
    {
        newIndex.y = 0;
    }

    if (newIndex.y < 0)
    {
        newIndex.y = 19;
    }

    // Set new head position
    m_SnakeBody[0] = newIndex;

    // Update remaining body pieces
    for (int i = 1; i < m_currentCellCount; i++)
    {
        // Save current body position
        IntVector2 bodyIndex = m_SnakeBody[i];

        // Assign previous body position to the next cell
        m_SnakeBody[i] = index;

        // Pass position to the next body piece
        index = bodyIndex;
    }
}

int Snake::Expand()
{
    IntVector2 lastCellIndex = IntVector2(0, 0);
    if (m_currentCellCount > 1)
    {
        lastCellIndex = m_SnakeBody[m_currentCellCount - 1];
    }
    else
    {
        lastCellIndex = m_SnakeBody[0];
    }

    lastCellIndex += m_moveDirection * -1;

    m_SnakeBody[m_currentCellCount] = lastCellIndex;

    m_currentCellCount++;

    return m_currentCellCount;
}

bool Snake::CheckCollision(IntVector2 index)
{
    if (m_currentCellCount >= m_maxCellCount)
    {
        return false;
    }

    return m_SnakeBody[0] == index;
}
