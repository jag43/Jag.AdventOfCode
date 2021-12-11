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
        public static string ToMessage(this SubmitResponse submitResponse)
        {
            return submitResponse switch
            {
                SubmitResponse.Correct => "That's the right answer! ⭐",
                SubmitResponse.Incorrect => "That's not the right answer. 😢",
                SubmitResponse.Timeout => "Timeout! Slow down. 🕑",
                SubmitResponse.AlreadySolved => "Answer already submitted. ⚠",
                _ => throw new InvalidOperationException($"Unknown {nameof(SubmitResponse)}: {submitResponse}")
            };
        }
    }
}