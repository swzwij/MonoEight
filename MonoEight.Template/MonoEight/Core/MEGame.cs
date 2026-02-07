using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoEight.Core.Scenes;
using MonoEight.Core.UserInput;

namespace MonoEight.Core;

/// <summary>
/// The core game class that manages the main game loop, rendering pipeline, and engine initialization.
/// </summary>
/// <remarks>
/// Inherit from this class to create your specific game entry point.
/// </remarks>
public class MEGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch? _spriteBatch;
    private RenderTarget2D? _renderTarget;
    private Rectangle _displayRect;

    protected MEGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        MonoEight.Core.Content.Initialize(Content, "Game/Content");
        MEWindow.StartFullscreen = false;
        MEWindow.Resolution = new Point(128, 96);
        MEWindow.Initialize(_graphics, Window);
        Debugger.Initialize(_graphics);
        PlayerPrefs.Initialize();

        SceneManager.Add("Loading", new LoadingScene());
        SceneManager.Load("Loading");

        OnGameInitialize();

        base.Initialize();
    }

    /// <summary>
    /// Called after the engine systems are initialized but before content is loaded.
    /// Override this to register your own scenes or configure window settings.
    /// </summary>
    protected virtual void OnGameInitialize()
    {

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        int width = MEWindow.Resolution.X > 0 ? MEWindow.Resolution.X : 128;
        int height = MEWindow.Resolution.Y > 0 ? MEWindow.Resolution.Y : 96;

        _renderTarget = new RenderTarget2D(GraphicsDevice, width, height);

        OnLoadContent();
    }

    /// <summary>
    /// Called when the game content is being loaded.
    /// Override this to load global assets or fonts.
    /// </summary>
    protected virtual void OnLoadContent()
    {

    }

    protected override void Update(GameTime gameTime)
    {
        _displayRect = GraphicsHelper.CalculateDisplayRect(GraphicsDevice);

        Input.Update(_displayRect);
        Time.Update(gameTime);

        if (Input.IsKeyPressed(Keys.F11))
            MEWindow.ToggleFullscreen();

        SceneManager.Update();

        OnUpdate();

        base.Update(gameTime);
    }

    /// <summary>
    /// Called every frame after the engine has updated the scene.
    /// Override this to add global logic that runs regardless of the scene.
    /// </summary>
    protected virtual void OnUpdate()
    {

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(SceneManager.ActiveScene?.Camera.BackgroundColor ?? Color.Black);

        _spriteBatch?.Begin
        (
            SpriteSortMode.FrontToBack,
            null,
            SamplerState.PointClamp,
            null,
            null,
            null,
            SceneManager.ActiveScene?.Camera.Transform ?? new Matrix()
        );

        SceneManager.Draw(_spriteBatch!);

        _spriteBatch?.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch?.Begin(samplerState: SamplerState.PointClamp);

        _spriteBatch?.Draw(_renderTarget, _displayRect, Color.White);
        _spriteBatch?.End();

        base.Draw(gameTime);
    }
}
