using UnityEngine;

public class Dragable : MonoBehaviour
{
    Vector2 initialMousePosition;
    Vector3 initialObjectPosition;
    float yOffset = 2.65f; // Adjust this value to set the margin/offset

    private void OnMouseDown()
    {
        initialMousePosition = Input.mousePosition;
        initialObjectPosition = transform.position;

        // Adjust the initial mouse position to account for the yOffset
        initialMousePosition.y -= yOffset * Screen.height / Camera.main.pixelHeight;
    }

    private void OnMouseDrag()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        currentMousePosition.y -= yOffset * Screen.height / Camera.main.pixelHeight;

        Vector2 difference = currentMousePosition - initialMousePosition;

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(currentMousePosition);
        newPosition.z = 0f; // Set the z-coordinate to 0 if your game is in 2D

        transform.position = initialObjectPosition + (newPosition - initialObjectPosition);
        ClampToScreenBounds();
    }

    void ClampToScreenBounds()
    {
        Vector3 clampedPosition = transform.position;

        float minX = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        float minY = Camera.main.ScreenToWorldPoint(Vector2.zero).y + yOffset; // Adjusted for margin/offset
        float maxX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        float maxY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;

        // Clamp the new position to screen bounds in world space
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        transform.position = clampedPosition;
    }
}
