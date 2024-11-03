using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements playback of 
the animation stored by Animation.
*/
public class AnimationPlayer
{
    private Texture2D spriteSheet;
    private int frameWidth;
    private int frameHeight;
    private int currentFrame;
    private int totalFrames;
    private float frameTime;
    private float timer;

    public AnimationPlayer(Texture2D texture, int frameCount, float animationSpeed)
    {
        spriteSheet = texture;
        frameWidth = texture.Width / frameCount;
        frameHeight = texture.Height;
        totalFrames = frameCount;
        frameTime = animationSpeed;
        currentFrame = 0;
        timer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(timer >= frameTime)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            timer = 0f;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);
        spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White);
    }
}
