using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_UpdateRate = 0.3f;
        m_AccumulatedTime = m_UpdateRate;
      
        m_MaxBodyPieceCount = 10;
       
        m_BodyPieceCount = 1;

        m_SnakeBody = new IntVector2[m_MaxBodyPieceCount];

        //Setting initial head position
        m_SnakeBody[0] = new IntVector2(0, 0);

        m_Snake = new SnakeGlue(new IntVector2(0, 0), m_MaxBodyPieceCount, m_SnakeBody);
    }

    void OnDestroy()
    {
        //Dispose of IDisposible classes here
        m_Snake.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        m_AccumulatedTime += Time.deltaTime;
        if(m_AccumulatedTime >= m_UpdateRate && !m_bGameOver)
        {
            m_AccumulatedTime = 0.0f;
        
            m_Snake.UpdateBodyPosition();
           
            if (m_Snake.CheckCollision(Grid.Instance.GetAppleIndex()))
            {
                m_BodyPieceCount = m_Snake.Expand();

                bool isOnSnake = false;

                do
                {
                    Grid.Instance.SpawnApple();

                    for (int i = 0; i < m_BodyPieceCount; i++)
                    {
                        if (Grid.Instance.GetAppleIndex() == m_SnakeBody[i])
                        {
                            isOnSnake = true;
                            break;
                        }
                    }

                } while (isOnSnake);

                //Speed increase by 20%
                m_UpdateRate *= 0.8f;
            }

            Grid.Instance.DrawSnake(m_SnakeBody, m_BodyPieceCount);

            if (m_BodyPieceCount > 4)
            {
                for (int i = 1; i < m_BodyPieceCount; i++)
                {
                    if (m_Snake.CheckCollision(m_SnakeBody[i]))
                    {
                        m_bGameOver = true;
                    }
                }
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Snake.SetNewDirection(new IntVector2(0, 1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_Snake.SetNewDirection(new IntVector2(0, -1));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_Snake.SetNewDirection(new IntVector2(1, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_Snake.SetNewDirection(new IntVector2(-1, 0));
        }
    }

    bool m_bGameOver = false;
    float m_AccumulatedTime;
    float m_UpdateRate;
    int m_BodyPieceCount;
    int m_MaxBodyPieceCount;
    IntVector2[] m_SnakeBody;
    SnakeGlue m_Snake;
}
