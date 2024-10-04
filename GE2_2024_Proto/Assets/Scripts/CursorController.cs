using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorDefault;
    public Texture2D cursorAttack;

    private Transform hover;
    private RaycastHit raycastHit;

    private void Awake()
    {
        ChangeCursor(cursorDefault);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);

    }


    void Update()
    {
        // hover
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            hover = raycastHit.transform;
            if (hover.CompareTag("Enemy"))
            {
                if (hover.gameObject.GetComponent<Outline>() != null)
                {
                    ChangeCursor(cursorAttack);
                }
                else
                {
                    Outline outline = hover.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }
            }
            else
            {
                hover = null;
                ChangeCursor(cursorDefault);
            }
        }

        // attack, load scene
        if (Input.GetMouseButtonDown(0))
        {
            if (hover)
            {
                SceneManager.LoadScene("TestBattle", LoadSceneMode.Single);
            }
        }



    }
}
