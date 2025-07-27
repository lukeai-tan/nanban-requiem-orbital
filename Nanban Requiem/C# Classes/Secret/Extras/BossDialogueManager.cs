using Godot;
using System;
using System.Collections.Generic;

public partial class BossDialogueManager : CanvasLayer
{

	public event EventHandler DialogueComplete;
	protected Label dialogueLabel;

    public override void _Ready()
    {
        dialogueLabel = GetNode("Panel").GetNodeOrNull<Label>("Dialogues");
	}

	private List<List<string>> dialogueLines = [
	[
		"Trying to cheese my game?",
		"I'm not upset.",
		"Just disappointed, Oracle."
	],
	[
		"Trying to cheese my game?",
		"Unfortunately,",
		"I make the rules."
	]];

	private List<string> currentDialogue = [];

	private int currentLine = 0;

	public void PlayDialogue(int i)
	{
		if (i < dialogueLines.Count)
		{
			this.Visible = true;
			currentDialogue = dialogueLines[i];
			ShowCurrentLine();
		}
	}

	public override void _Input(InputEvent @event)
    {
        if (this.Visible && @event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            AdvanceDialogue();
        }
    }

	private void ShowCurrentLine()
	{
		if (currentLine < currentDialogue.Count)
		{
			dialogueLabel.Text = currentDialogue[currentLine];
		}
	}

	private void AdvanceDialogue()
	{
		currentLine++;
		if (currentLine < currentDialogue.Count)
		{
			ShowCurrentLine();
		}
		else
		{
			this.Visible = false;
			currentLine = 0;
			this.DialogueComplete?.Invoke(this, EventArgs.Empty);
		}
	}
}
