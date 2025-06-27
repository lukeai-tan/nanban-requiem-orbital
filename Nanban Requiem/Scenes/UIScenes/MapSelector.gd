extends Control
class_name MapSelector

@onready var maps: Array = [$MapIcon1, $MapIcon2, $MapIcon3, $BossMapIcon]
var current_map: int = 0
var move_tween: Tween
var map_name: String

signal map_selected(map_name: String)

func _ready() -> void:
	# sets default map to index 0 (aka Map 1)
	$PlayerIcon.global_position = maps[current_map].global_position
	maps[current_map].get_node("Label").self_modulate = Color(1, 1, 0)


func _input(event):
	if move_tween and move_tween.is_running():
		return

	if event.is_action_pressed("ui_left") and current_map > 0:
		maps[current_map].get_node("Label").self_modulate = Color(1, 1, 1)
		current_map -= 1
		tween_icon()
	
	if event.is_action_pressed("ui_right") and current_map < maps.size() - 1:
		maps[current_map].get_node("Label").self_modulate = Color(1, 1, 1)
		current_map += 1
		tween_icon()

	if event.is_action_pressed("ui_accept"):
		print("Map clicked")
		if current_map == 3:
			map_name = "BossMap"
		else:
			map_name = "Map%d" % (current_map + 1)
		print("Selected map node: ", map_name)
		emit_signal("map_selected", map_name)
		queue_free()


func tween_icon():
	move_tween = get_tree().create_tween()
	move_tween.tween_property($PlayerIcon, "global_position", maps[current_map].global_position, 0.3).set_trans(Tween.TRANS_SINE)
	maps[current_map].get_node("Label").self_modulate = Color(1, 1, 0)
