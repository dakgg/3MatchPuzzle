using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    bool m_isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move((int)this.transform.position.x + 1, (int)this.transform.position.y, 1f);
        }
    }

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move(int destX, int destY, float timeToMove)
    {
        if(m_isMoving == false)
            StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));
    }

    IEnumerator MoveRoutine(Vector3 dest, float timeToMove)
    {
        Vector3 startPos = transform.position;
        bool reachDest = false;
        float elapsedTime = 0f;
        m_isMoving = true;
       
        while (reachDest == false)
        {
            // do something
            if(Vector3.Distance(transform.position, dest) < 0.01f)
            {
                reachDest = true;
                transform.position = dest;
                SetCoord((int)dest.x, (int)dest.y);
                m_isMoving = false;
            }

            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            this.transform.position = Vector3.Lerp(startPos, dest, t);

            // wait until next frame
            yield return null;
        }

        yield break;
    }

}
