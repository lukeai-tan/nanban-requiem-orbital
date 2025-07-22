extends Control
class_name MapSelector

@onready var maps: Array = [$MapIcon1, $MapIcon2, $MapIcon3, $BossMapIcon, $MapIcon5]
var current_map: int = 0
var move_tween: Tween
var map_name: String

signal map_selected(map_name: String)
signal return_to_main_menu

func _ready() -> void:
	# sets default map to index 0 (aka Map 1)
	$PlayerIcon.global_position = maps[current_map].global_position
	maps[current_map].get_node("Label").self_modulate = Color(1, 1, 0)
	for i in range(maps.size()):
		maps[i].connect("gui_input", Callable(self, "_on_map_icon_gui_input").bind(i))


func _input(event):
	if move_tween and move_tween.is_running():
		return

	if event is InputEventMouseButton:
		return
	
	if event.is_action_pressed("ui_left") and current_map > 0:
		_select_map(current_map - 1)
	
	if event.is_action_pressed("ui_right") and current_map < maps.size() - 1:
		_select_map(current_map + 1)

	if event.is_action_pressed("ui_accept") and (not move_tween or not move_tween.is_running()):
		print("Map clicked")
		_confirm_map()


func _on_map_icon_gui_input(event: InputEvent, index: int) -> void:
	if event is InputEventMouseButton and event.pressed and event.button_index == MOUSE_BUTTON_LEFT:
		if current_map == index:
			_confirm_map()
		else:
			_select_map(index)


func _confirm_map() -> void:
	if current_map == 3:
		map_name = "BossMap"
	else:
		map_name = "Map%d" % (current_map + 1)
	print("Selected map node: ", map_name)
	emit_signal("map_selected", map_name)

	var cleanup_tween := get_tree().create_tween()
	cleanup_tween.tween_interval(0.1)
	cleanup_tween.tween_callback(Callable(self, "queue_free"))


func _select_map(index: int) -> void:
	if move_tween and move_tween.is_running():
		return

	maps[current_map].get_node("Label").self_modulate = Color(1, 1, 1)
	current_map = index
	maps[current_map].get_node("Label").self_modulate = Color(1, 1, 0)
	tween_icon()


func tween_icon():
	move_tween = get_tree().create_tween()
	move_tween.tween_property($PlayerIcon, "global_position", maps[current_map].global_position, 0.3).set_trans(Tween.TRANS_SINE)
	maps[current_map].get_node("Label").self_modulate = Color(1, 1, 0)



func _on_back_to_main_pressed() -> void:
	emit_signal("return_to_main_menu")
	queue_free()
