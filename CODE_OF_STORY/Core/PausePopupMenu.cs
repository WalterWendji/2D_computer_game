using System;
using CODE_OF_STORY.Scenes;
using CODE_OF_STORY.Scenes.Gateway;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static CODE_OF_STORY.Core.Data;

namespace CODE_OF_STORY.Core;

internal class PausePopupMenu : Component
{
    private string[] buttonNames = { "Continue_Button", "Newgame_Button", "Menu_Button", "Settings_Button", "Exit_Button" };
    private string[] buttonNameColored = { "Continue_col_Button", "New_Game_col_Button", "Menu_col_Button", "Settings_col_Button", "Exit_col_Button" };

    private Texture2D homeButton;
    private Texture2D homeButtonColored;
    private Rectangle homeButtonRect;
    private Rectangle homeButtonColoredRect;
    private Vector2 homeButtonPosition;
    private int xPositionHomeButton;
    private int yPositionHomeButton;

    private Texture2D[] buttons;
    private Texture2D[] btnColored;

    private Rectangle[] btnRects;

    private int buttonHeight;
    private int totalHeight;
    private int xPosition;
    private int yPosition;
    public static bool isClicked;
    public static bool isMouseStateRelease;

    private MouseState currentMouseState, oldMouseState;
    private Rectangle currentMouseStateRectangle;

    Viewport viewport;


    public PausePopupMenu()
    {
        buttons = new Texture2D[buttonNames.Length];
        btnColored = new Texture2D[buttonNameColored.Length];
        btnRects = new Rectangle[buttonNames.Length];
        isClicked = false;
        isMouseStateRelease = false;
        viewport = Game1._graphics.GraphicsDevice.Viewport;
    }

    internal override void LoadContent(ContentManager Content)
    {

        int screenWidth = viewport.Width;
        int screenHeight = viewport.Height;

        homeButton = Content.Load<Texture2D>("Buttons/Home_Square_Button");
        homeButtonColored = Content.Load<Texture2D>("Buttons/ColoredButtons/Home_col_Square_Button");
        homeButtonPosition = new Vector2(viewport.Width - 60, viewport.Height - (viewport.Height - 20));

        xPositionHomeButton = (int)homeButtonPosition.X;
        yPositionHomeButton = (int)homeButtonPosition.Y;

        homeButtonRect = new Rectangle(xPositionHomeButton, yPositionHomeButton, homeButton.Width / 4, homeButton.Height / 4);
        homeButtonColoredRect = new Rectangle(xPositionHomeButton, yPositionHomeButton, homeButtonColored.Width / 4, homeButtonColored.Height / 4);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = Content.Load<Texture2D>($"Buttons/{buttonNames[i]}");
            buttonHeight = buttons[0].Height / 4;
            totalHeight = buttonHeight * buttons.Length;
            xPosition = (screenWidth - (buttons[i].Width / 4)) / 2;
            yPosition = (screenHeight - totalHeight) / 2 + i * (buttonHeight + 60);
            btnRects[i] = new Rectangle(xPosition, yPosition, buttons[i].Width / 4, buttons[i].Height / 4);
        }
        for (int i = 0; i < btnColored.Length; i++)
        {
            btnColored[i] = Content.Load<Texture2D>($"Buttons/ColoredButtons/{buttonNameColored[i]}");
            buttonHeight = btnColored[0].Height / 4;
            totalHeight = buttonHeight * btnColored.Length * 2;
            xPosition = (screenWidth - (btnColored[i].Width / 4)) / 2;
            yPosition = (screenHeight - totalHeight) / 2 + i * (buttonHeight + 60);
            btnRects[i] = new Rectangle(xPosition, yPosition, btnColored[i].Width / 4, btnColored[i].Height / 4);
        }
    }

    internal override void Update(GameTime gameTime)
    {
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);

        if (currentMouseState.LeftButton == ButtonState.Pressed)
        {
            if (currentMouseStateRectangle.Intersects(btnRects[0]) || currentMouseStateRectangle.Intersects(btnRects[1])
                || currentMouseStateRectangle.Intersects(btnRects[2]) || currentMouseStateRectangle.Intersects(btnRects[3])
                || currentMouseStateRectangle.Intersects(btnRects[4]) || currentMouseStateRectangle.Intersects(homeButtonRect))
            {
                StoneAge.popUpMenuTriggerd = false;
            }
            
            if (currentMouseStateRectangle.Intersects(btnRects[0]))
                Data.currentGameState = Data.GameState.Playing;
            else if (currentMouseStateRectangle.Intersects(btnRects[1]))
            {
                isClicked = true;
                currentGameState = GameState.Playing;
                Data.currentState = Data.Scenes.StoneAge;
            }
            else if (currentMouseStateRectangle.Intersects(btnRects[2]))
                Data.currentState = Data.Scenes.Menu;
            else if (currentMouseStateRectangle.Intersects(btnRects[3]))
                Data.currentState = Data.Scenes.Settings;
            else if (currentMouseStateRectangle.Intersects(btnRects[4]))
                Data.Exit = true;
            else if (currentMouseStateRectangle.Intersects(homeButtonRect))
                Data.currentState = Data.Scenes.Gateways;
        }

    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(homeButton, homeButtonRect, Color.White);
        if (currentMouseStateRectangle.Intersects(homeButtonRect))
            spriteBatch.Draw(homeButtonColored, homeButtonColoredRect, Color.White);

        for (int i = 0; i < buttons.Length; i++)
        {
            spriteBatch.Draw(buttons[i], btnRects[i], Color.White);

            if (currentMouseStateRectangle.Intersects(btnRects[i]))
                spriteBatch.Draw(btnColored[i], btnRects[i], Color.White);

        }
    }
}
