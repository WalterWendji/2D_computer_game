using CODE_OF_STORY.Managers;
using CODE_OF_STORY.Scenes.Gateway;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace CODE_OF_STORY.Core;

/* Implements major game components 
such as content and level loading, 
HUD management and display, and game 
object updating. That is also the "Game1" class. */
public class Game1 : Game
{
    public static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameStateManager gameStateManager;
    private GatewaysManager gatewaysManager;
    private Camera _camera;
    public static MouseState currentMouseState, oldMouseState;
    public static Rectangle currentMouseStateRectangle;
    public static int xPosition;
    public static int yPosition;
    public static Texture2D backButton;
    public static Texture2D backButtonColored;
    public static Rectangle backButtonRect;
    public static Rectangle backButtonRectColored;
    private Vector2 backButtonPosition;

    SoundEffect menuSong;
    SoundEffectInstance menuSongInstance;

    private Rectangle _worldBounds = new Rectangle(0, 0, 3200, 896);
    private Rectangle _viewportBounds;
    GameTime gameTime1;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;  
    }

    protected override void Initialize()
    {
        gameTime1 = new GameTime();
        _graphics.PreferredBackBufferWidth = Data.screenW;
        _graphics.PreferredBackBufferHeight = Data.screenH;
        _graphics.ApplyChanges();

        _viewportBounds = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _camera = new Camera(_worldBounds, _viewportBounds);

        gameStateManager = new GameStateManager();
        gatewaysManager = new GatewaysManager();

        ControlSettingsManager.LoadControls();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        gameStateManager.LoadContent(Content);
        gatewaysManager.LoadContent(Content);

        Viewport viewport = _graphics.GraphicsDevice.Viewport;

        backButtonPosition = new Vector2(viewport.Width - (viewport.Width - 10), viewport.Height - 60);

        xPosition = (int)backButtonPosition.X;
        yPosition = (int)backButtonPosition.Y;

        backButton = Content.Load<Texture2D>("Buttons/Back_Square_Button");
        backButtonColored = Content.Load<Texture2D>("Buttons/ColoredButtons/Back_col_Square_Button");

        backButtonRect = new Rectangle(xPosition, yPosition, backButton.Width / 4, backButton.Height / 4);
        backButtonRectColored = new Rectangle(xPosition, yPosition, backButtonColored.Width / 4, backButtonColored.Height / 4);

        menuSong = Content.Load<SoundEffect>("Audio/11-Fight2");
        menuSongInstance = menuSong.CreateInstance();
    }

    protected override void Update(GameTime gameTime)
    {
        switch(Data.currentState)
        {
            case Data.Scenes.StoneAge:
                HandleStoneAgeInput();
                break;
            case Data.Scenes.MiddleAge:
                HandleMiddleAgeInput();
                _camera.Update(gameTime);
                break;
            case Data.Scenes.ModernAge:
                HandleModernAgeInput();
                break;
            case Data.Scenes.Future:
                HandleFutureInput();
                break;
        }       


        menuSongInstance.Play();
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);

        if (Data.Exit)
            Exit();

        gameStateManager.Update(gameTime);

        if (getTheCurrentStateOfGateway())
        {
            gatewaysManager.Update(gameTime);
            menuSongInstance.Stop();
        }

        if (!Player.checkIsAlive && PausePopupMenu.isClicked || !Player.checkIsAlive && GameOver.isResetButtonClicked)
        {
            gatewaysManager.RestartCurrentLevel();
            PausePopupMenu.isClicked = false;
            GameOver.isResetButtonClicked = false;
        }

        base.Update(gameTime);
    }
    
    private void HandleMenuInput()
    {
        // Example: Start Level 2 when a key is pressed
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            //_currentGameState = GameState.Level2;
        }
    }

    private void HandleStoneAgeInput()
    {
        // Level 1 specific input handling
    }

    private void HandleMiddleAgeInput()
    {
        // Example: Move the camera with arrow keys in Level 2
        var keyboardState = Keyboard.GetState();
        float moveSpeed = (float)gameTime1.ElapsedGameTime.TotalSeconds * 200;
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            _camera.Move(new Vector2(moveSpeed, 0));
        }
        if (keyboardState.IsKeyDown(Keys.Left))
        {
            _camera.Move(new Vector2(-moveSpeed, 0));
        }
        if (keyboardState.IsKeyDown(Keys.Up))
        {
           _camera.Move(new Vector2(0, -moveSpeed));
        }
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            _camera.Move(new Vector2(0, moveSpeed));
        }
    }

    private void HandleModernAgeInput()
    {
        // Level 3 specific input handling
    }

    private void HandleFutureInput()
    {
        // Level 4 specific input handling
    }

    public GameWindow GetGameWindow()
    {
        return Window;
    }
    private static bool getTheCurrentStateOfGateway()
    {
        return Data.currentState == Data.Scenes.StoneAge || Data.currentState == Data.Scenes.MiddleAge || Data.currentState == Data.Scenes.ModernAge || Data.currentState == Data.Scenes.Future;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(/* transformMatrix:_camera.Transform */);
        gameStateManager.Draw(_spriteBatch);
        if (getTheCurrentStateOfGateway())
            gatewaysManager.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
