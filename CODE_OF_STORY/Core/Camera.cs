using System;
using Microsoft.Xna.Framework;

namespace CODE_OF_STORY.Core;

/* This is useful for following 
player characters or creating parallax effects, 
among other things. */
public class Camera
{
    public Matrix Transform { get; private set; }
    public Vector2 Position { get; set; }
    public float Zoom { get; set; } = 1.0f;
    public Rectangle WorldBounds { get; set; }
    public Rectangle ViewportBounds { get; set; }
    Vector2 newPosition;
    public Camera(Rectangle worldBounds, Rectangle viewportBounds)
    {
        WorldBounds = worldBounds;
        ViewportBounds = viewportBounds;

        newPosition = Position;
    }

    public void Move(Vector2 offset)
    {
        Position += offset;
        ClampPosition();
    }

    private void ClampPosition()
    {

        // Calculate the minimum and maximum positions to clamp the camera with the world bounds
        float minX = ViewportBounds.Width / 2f;
        float minY = ViewportBounds.Height / 2f;
        float maxX = WorldBounds.Width - ViewportBounds.Width / 2f;
        float maxY = WorldBounds.Height - ViewportBounds.Height / 2f;
        // Clamp the x and y positions
        newPosition.X = MathHelper.Clamp(Position.X, minX, maxX);
        newPosition.Y = MathHelper.Clamp(Position.Y, minY, maxY);
    }



    public void Update(GameTime gameTime)
    {
        // Update the transformation matrix
        Transform = Matrix.CreateTranslation(new Vector3(-newPosition.X, -newPosition.Y, 0));
                    
    }
}
