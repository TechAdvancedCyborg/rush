// Copyright (c) Shane Woolcock. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Judgements;
using osuTK;

namespace osu.Game.Rulesets.Rush.Objects.Drawables
{
    public class DrawableRushJudgement : DrawableJudgement
    {
        [Resolved]
        private OsuColour colours { get; set; }

        protected override double FadeOutDelay => 200f;

        public DrawableRushJudgement(JudgementResult result, DrawableRushHitObject judgedObject)
            : base(result, judgedObject)
        {
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore store)
        {
            InternalChild = JudgementBody = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Child = JudgementText = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = Result.Type.GetDescription().ToUpperInvariant(),
                    Font = OsuFont.Numeric.With(size: 20),
                    Colour = colours.ForHitResult(Result.Type),
                    Scale = new Vector2(0.85f, 1f)
                }
            };
        }
    }
}
