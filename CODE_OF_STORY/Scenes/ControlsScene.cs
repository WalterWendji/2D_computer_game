using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Scenes;

internal class ControlsScene : Component
{
    private SpriteFont font;
    private Texture2D menuBackground;
    private Rectangle BgRectangle;
    private string[] actions = {"Move Left", " Move Right", "Jump", "Interact","Switch Attack Mode", "Use Item"};
    private Keys[] defaultKeys = {Keys.A, Keys.D, Keys.Space, Keys.F, Keys.Q, Keys.E};
    private Keys[] currentKeys;

    private Rectangle[] keyRects;
    private Rectangle backButtonRect;
    private Texture2D buttonTexture;
    private MouseState currentMousState, oldMouseState;
    private Rectangle currentMousRect;
    private int selectedAction = -1;

    public ControlsScene()
    {
        currentKeys = (Keys[])defaultKeys.Clone();
        keyRects = new Rectangle[actions.Length];
    }

    internal override void LoadContent(ContentManager Content)
    {
        menuBackground = Content.Load<Texture2D>("Items/bg_desertM");
        buttonTexture = Content.Load<Texture2D>("Buttons/KeyButton");
        font = Content.Load<SpriteFont>("Spritefonts/Arial");

        BgRectangle = new Rectangle(0, 0, menuBackground.Width * 6, menuBackground.Height * 3);


        Viewport viewport = Game1._graphics.GraphicsDevice.Viewport;
        int screenWidth = viewport.Width;
        int screenHeight = viewport.Height;

        int buttonWidth = 200;
        int buttonHeight = 50;
        int xPosition = (screenWidth - buttonWidth) / 2;
        int startY = (screenHeight - (actions.Length * (buttonHeight + 20))) / 2;

        for(int i = 0; i < actions.Length; i++)
        {   
            keyRects[i] = new Rectangle(xPosition, startY + i * (buttonHeight + 20), buttonWidth, buttonHeight);
        }

        backButtonRect = new Rectangle(xPosition, startY + actions.Length * (buttonHeight + 20) + 40, buttonWidth, buttonHeight);
    }

    internal override void Update(GameTime gameTime)
    {
        oldMouseState = currentMousState;
        currentMousState = Mouse.GetState();
        currentMousRect = new Rectangle(currentMousState.X, currentMousState.Y, 1, 1);

        if(selectedAction >= 0)
        {
            var pressedKeys = Keyboard.GetState().GetPressedKeys();
            if(pressedKeys.Length > 0)
            {
                currentKeys[selectedAction] = pressedKeys[0];
                selectedAction = -1;
            }
        }
        else if(currentMousState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
        {
            for(int i = 0; i < keyRects.Length; i++)
            {
                if(currentMousRect.Intersects(keyRects[i]))
                {
                    selectedAction = i;
                    break;
                }
            }

            if(currentMousRect.Intersects(backButtonRect))
            {
                Data.currentState = Data.Scenes.Settings;
            }
        }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(menuBackground, BgRectangle, Color.White);
        for(int i = 0; i < actions.Length; i++)
        {
            Color buttonColor = selectedAction == i ? Color.Yellow : Color.White;
            spriteBatch.Draw(buttonTexture, keyRects[i], buttonColor);

            Vector2 actionTextPosition = new Vector2(keyRects[i].X + 10, keyRects[i].Y + 10);
            Vector2 keyTextPosition = new Vector2(keyRects[i].X + keyRects[i].Width - 60, keyRects[i].Y +10);
            spriteBatch.DrawString(font, actions[i], actionTextPosition, Color.Black);
            spriteBatch.DrawString(font, currentKeys[i].ToString(), keyTextPosition , Color.Black);
        }

        spriteBatch.Draw(buttonTexture, backButtonRect, Color.White);
        spriteBatch.DrawString(font, "Back", new Vector2(backButtonRect.X + 10, backButtonRect.Y +10), Color.Black);
    }
}
