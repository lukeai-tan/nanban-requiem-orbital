extends "res://Scenes/MainScenes/UI.gd"

var fourth_wall: bool = false

func _on_restart_pressed() -> void:
    if !fourth_wall:
        var label = quit_window.get_node("Label")
        label.text += "\n" + UserInfo.username
        fourth_wall = true
    super._on_restart_pressed()
    

func _on_quit_button_pressed() -> void:
    quit_window.visible = false
    get_tree().paused = false