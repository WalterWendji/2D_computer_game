using System;
//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Vector2 = System.Numerics.Vector2;

namespace CODE_OF_STORY.Core;

/* Implements the player character. 
Used to load, draw, and update the character. */
public class Player
{
    private Texture2D texture;
    private Vector2 position;
    private float speed;

    public Player(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.speed = 200f;
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (state.IsKeyDown(Keys.A))
            position.X -= speed * deltaTime;
            System.Diagnostics.Debug.WriteLine("Bewegt nach links");
        if (state.IsKeyDown(Keys.D))
            position.X += speed * deltaTime;
        /*if (state.IsKeyDown(Keys.Space))
            springen
        if (state.IsKeyDown(Keys.F))
            interagieren
        if (state.IsKeyDown(Keys.Q))
            Waffewechseln
        if (mouseState.LeftButton == ButtonState.Pressed)
            angreifen
        */
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}
