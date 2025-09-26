// using System;
// using System.Collections.Generic;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace MonoEight;

// public class Animator
// {
//     private readonly Dictionary<string, Animation> _animations;
//     private Animation _activeAnimation;

//     public Animation this[string trigger] => Get(trigger);

//     public Animator(AnimationBinding[] animationBindings)
//     {
//         _animations = [];

//         foreach (AnimationBinding binding in animationBindings)
//             _animations.Add(binding.Trigger, binding.Animation);
//     }

//     public Animation Get(string trigger)
//     {
//         if (!_animations.TryGetValue(trigger, out Animation animation))
//             throw new IndexOutOfRangeException($"Trigger: '{trigger}' was not found in the animator");

//         return animation;
//     }

//     public void Play(string trigger)
//     {
//         if (!_animations.TryGetValue(trigger, out Animation animation))
//             throw new IndexOutOfRangeException($"Trigger: '{trigger}' was not found in the animator");

//         _activeAnimation?.Stop();
//         _activeAnimation = animation;
//     }

//     public void Stop()
//     {
//         _activeAnimation?.Stop();
//     }

//     public void Update(GameTime gameTime)
//     {
//         _activeAnimation?.Update(gameTime);
//     }

//     public void Draw(SpriteBatch spriteBatch, Point position)
//     {
//         _activeAnimation?.Draw(spriteBatch, position);
//     }
// }