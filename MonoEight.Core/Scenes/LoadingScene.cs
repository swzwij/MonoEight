using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core.Sprite;

namespace MonoEight.Core.Scenes;

/// <summary>
/// <see cref="Scene"/> showing the MonoEight intro animation.
/// </summary>
public class LoadingScene : Scene
{
    private const float WAIT_TIME = 1f;

    private float _timer;
    private bool _isAnimationFinished;

    protected override void LoadContent()
    {
        Camera.BackgroundColor = MEColors.Black;

        GameObject logo = new();
        Animator animator = new(new SpriteSheet(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64))
        {
            FrameDuration = 0.05f,
            Loop = false,
            Scale = (int)Math.Max(1, Math.Min(MEWindow.Resolution.X / 32f, MEWindow.Resolution.Y / 32f) / 2)
        };

        logo.AddComponent(animator);
        animator.OnStopped += () => _isAnimationFinished = true;
        animator.Play();

        Add(logo);
    }

    protected override void Update()
    {
        if (!_isAnimationFinished)
            return;

        _timer += Time.DeltaTime;

        // if (_timer >= WAIT_TIME)
        //     SceneManager.Load("Test 1");
    }
}
