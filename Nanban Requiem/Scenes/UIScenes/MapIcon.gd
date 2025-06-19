@tool
extends Control

@export var map_index: int = 1


func _ready() -> void:
	$Label.text = "Map " + str(map_index)


func _process(_delta: float) -> void:
	if Engine.is_editor_hint():
		$Label.text = "Map " + str(map_index)
