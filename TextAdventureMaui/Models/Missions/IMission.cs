using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Missions;

public interface IMission
{
    string Title { get; }        // Short label, e.g. "Fight the Spaghetti Bandit"
    string Description { get; }  // Narrative context

    bool IsCompleted { get; }    // Mission success state
    bool IsFailed { get; }       // Optional: failure state

    Task StartAsync();           // Begin the mission (can trigger UI)
    void Complete();             // Mark as completed
    void Fail();                 // Mark as failed
}
