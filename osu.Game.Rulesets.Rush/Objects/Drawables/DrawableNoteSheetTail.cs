// Copyright (c) Shane Woolcock. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Rush.Objects.Drawables
{
    public class DrawableNoteSheetTail : DrawableNoteSheetCap<NoteSheetTail>
    {
        internal const double RELEASE_WINDOW_LENIENCE = 3;

        public DrawableNoteSheetTail(DrawableNoteSheet noteSheet)
            : base(noteSheet, noteSheet.HitObject.Tail)
        {
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            var overallMissed = NoteSheet.Result.Type == HitResult.Miss;

            // Apply tail miss at its time when the entire note sheet has already been judged as missed.
            if (overallMissed && timeOffset >= 0)
            {
                ApplyResult(r => r.Type = HitResult.Miss);
                return;
            }

            // for now let's give the player an automatic perfect if they hold the note (like in a certain other rhythm game)
            if (!userTriggered)
            {
                if (timeOffset >= 0)
                    ApplyResult(r => r.Type = HitResult.Perfect);
                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);
            if (result == HitResult.None)
                return;

            // ...and an automatic perfect if they release within any "hit" judged period
            ApplyResult(r => r.Type = HitResult.Perfect);
        }

        // FIXME: should logically be TrailingAnchor, not sure why it renders incorrectly
        protected override void OnDirectionChanged(ValueChangedEvent<ScrollingDirection> e) => Anchor = LeadingAnchor;
    }
}
