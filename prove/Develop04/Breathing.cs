using System;

public class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing",
        "This activity will help you relax by walking you through " +
        "breathing in and out slowly. Clear your mind and focus " +
        "on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            int remaining = (int)Math.Ceiling(
                (endTime - DateTime.Now).TotalSeconds);

            if (remaining <= 0)
            {
                break;
            }

            Console.Write("Breathe in... ");
            ShowCountdown(Math.Min(4, remaining));

            remaining = (int)Math.Ceiling(
                (endTime - DateTime.Now).TotalSeconds);

            if (remaining <= 0)
            {
                break;
            }

            Console.Write("Breathe out... ");
            ShowCountdown(Math.Min(4, remaining));

            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}
