extends "res://Scenes/MainScenes/UI.gd"




func _on_quit_button_pressed() -> void:
	quit_window.visible = false
	get_tree().paused = false