extends Control

signal return_to_main_menu



func _on_back_to_main_pressed() -> void:
	emit_signal("return_to_main_menu")
	queue_free()
