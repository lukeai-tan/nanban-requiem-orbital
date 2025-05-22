extends VBoxContainer


func _ready():
	for button in get_tree().get_nodes_in_group("mainmenu_buttons"):
		button.connect("mouse_entered", Callable(ButtonSoundManager, "play_hover_sound"))
		button.connect("pressed", Callable(ButtonSoundManager, "play_press_sound"))
