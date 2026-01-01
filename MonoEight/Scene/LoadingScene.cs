using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class LoadingScene : Scene
{
    private const float WAIT_TIME = 1f;

    private float _timer = 0;
    private bool _isAnimationFinished;

    protected override void LoadContent()
    {
        Camera.BackgroundColor = MEColors.Black;

        GameObject logo = new();
        Animator animator = new(new SpriteSheet(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64))
        {
            FrameDuration = 0.05f,
            Loop = false,
            Scale = Math.Min(MEWindow.Resolution.X / 32, MEWindow.Resolution.Y / 32) / 2
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

        if (_timer >= WAIT_TIME)
            SceneManager.Load("Test 1");
    }
}
