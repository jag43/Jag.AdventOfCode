using System;

namespace Jag.AdventOfCode
{
    public enum SubmitResponse
    {
        Unknown,
        Correct,
        Incorrect,
        Timeout,
        AlreadySolved
    }

    public static class SubmitResponseExtensions
    {
        public static string ToMessage(this SubmitResponse submitResponse, int part)
        {
            return (submitResponse, part) switch
            {
                (SubmitResponse.Correct, 1) => "⭐ That's the right answer! ⭐",
                (SubmitResponse.Correct, 2) => "⭐⭐ That's the right answer! ⭐⭐",
                (SubmitResponse.Incorrect, _) => "That's not the right answer. 😢",
                (SubmitResponse.Timeout, _) => "Timeout! Slow down. 🕑",
                (SubmitResponse.AlreadySolved, _) => "Answer already submitted. ⚠",
                _ => throw new InvalidOperationException($"Unknown {nameof(SubmitResponse)}: {submitResponse}")
            };
        }
    }
}