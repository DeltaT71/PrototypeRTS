using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorUp;
    [SerializeField] private Texture2D cursorDown;
    [SerializeField] private Texture2D cursorRight;
    [SerializeField] private Texture2D cursorLeft;

    CursorArrow currentCursor = CursorArrow.DEFAULT;

    public void ChangeMouseCursor(CursorArrow newCursor)
    {
        if (currentCursor != newCursor)
        {
            switch (newCursor)
            {
                case CursorArrow.UP:
                    Cursor.SetCursor(cursorUp, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorArrow.DOWN:
                    Cursor.SetCursor(cursorDown, new Vector2(cursorDown.width, cursorDown.height), CursorMode.Auto);
                    break;
                case CursorArrow.RIGHT:
                    Cursor.SetCursor(cursorRight, new Vector2(cursorRight.width, cursorRight.height), CursorMode.Auto);
                    break;
                case CursorArrow.LEFT:
                    Cursor.SetCursor(cursorLeft, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorArrow.DEFAULT:
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }
        currentCursor = newCursor;
    }
}
