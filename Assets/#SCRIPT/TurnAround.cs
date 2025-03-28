using System.Collections;
using UnityEngine;
public class TurnAround : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        StartCoroutine(OBjectTurner());
    }

    private IEnumerator OBjectTurner()
    {
        Vector2 mousesPos = _camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, 0)); // (0,0) is not the cordinate of center
        if (Input.GetMouseButtonDown(0) && mousesPos.x > 0)
        {
            for (var i =0;i<5;i++)
            {
                transform.Rotate(0, 0, -10);
                yield return new WaitForSeconds(0.05f); 
            }
            transform.Rotate(0, 0, -10);
        }
        else if (Input.GetMouseButtonDown(0) && mousesPos.x < 0)
        {
            for (var i = 0; i < 5; i++)
            {
                transform.Rotate(0, 0, 10);
                yield return new WaitForSeconds(0.05f);
            }
            transform.Rotate(0, 0, 10);
        }
    }
}
