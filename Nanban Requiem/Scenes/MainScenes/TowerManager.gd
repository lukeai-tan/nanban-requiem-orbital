extends Node2D

signal tower_count_changed(current_count, max_count)

var max_tower_count: int = 5
var current_tower_count: int = 0

var ui
var map_node


func _process(_delta):
	if not map_node:
		return

	var new_tower_count = map_node.get_node("Towers").get_child_count()
	if new_tower_count != current_tower_count:
		current_tower_count = new_tower_count
		emit_signal("tower_count_changed", current_tower_count, max_tower_count)

## sets or changes the max tower limit for the map
func change_deployment(count: int):
	max_tower_count = count
	emit_signal("tower_count_changed", current_tower_count, max_tower_count)


func get_tower_count() -> int:
	return map_node.get_node("Towers").get_child_count()


func can_place_tower() -> bool:
	return current_tower_count < max_tower_count

func set_ui(_ui):
	ui = _ui
	tower_count_changed.connect(_on_tower_count_changed)
	_on_tower_count_changed(current_tower_count, max_tower_count)


func _on_tower_count_changed(current_count, max_count):
	if ui:
		ui.update_tower_count(current_count, max_count)
	
