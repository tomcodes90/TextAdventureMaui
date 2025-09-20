using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.Services;

public class DialogueService
{
    public void Play(Dictionary<int, DialogueNode> nodes, int startNodeId)
    {
        int currentId = startNodeId;

        while (nodes.ContainsKey(currentId))
        {
            var node = nodes[currentId];
            Console.WriteLine($"{node.Line.Speaker}: {node.Line.Text}");

            if (!node.HasChoices)
            {
                // End if no branching
                break;
            }

            // Show choices
            for (int i = 0; i < node.Choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {node.Choices[i].Text}");
            }

            // Read input
            string input = Console.ReadLine() ?? "1";
            if (int.TryParse(input, out int choiceIndex) &&
                choiceIndex >= 1 &&
                choiceIndex <= node.Choices.Count)
            {
                currentId = node.Choices[choiceIndex - 1].NextNodeId;
            }
            else
            {
                Console.WriteLine("Invalid choice. Defaulting to first option.");
                currentId = node.Choices[0].NextNodeId;
            }
        }
    }
}
