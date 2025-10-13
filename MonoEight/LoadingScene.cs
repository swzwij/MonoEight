using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class LoadingScene : Scene
{
    private const float WAIT_TIME = 1f;

    private float _timer;
    private bool _isAnimationFinished;

    private void Initialize()
    {
        Console.WriteLine("Loading Scene Initialized");

        _timer = 0;
    }

    private void LoadContent()
    {
        Console.WriteLine("Loading Scene Content");

        Camera.BackgroundColor = MEColors.Black;

        GameObject logo = new();
        Animator animator = new(new SpriteSheet(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64))
        {
            FrameDuration = 0.05f,
            Loop = false,
            Scale = Math.Min(MEWindow.Width / 32, MEWindow.Height / 32) / 2
        };

        logo.AddComponent(animator);
        animator.OnStopped += () => _isAnimationFinished = true;
        animator.Play();

        Add(logo);
    }

    private void Update()
    {
        Console.WriteLine($"Scene '{Name}' Update");

        if (Input.IsKeyDown(Keys.Space))
            SceneManager.Load("Test 1");

        if (_isAnimationFinished)
        {
            _timer += Time.DeltaTime;

            if (_timer >= WAIT_TIME)
                SceneManager.Load("Test 1");
        }
    }
}