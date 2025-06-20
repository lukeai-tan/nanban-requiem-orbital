extends Control
class_name MapSelector

@onready var maps: Array = [$MapIcon, $MapIcon2, $MapIcon3]
var current_map: int = 0
var move_tween: Tween

func _ready() -> void:
	# sets default map to index 0 (aka Map 1)
	$PlayerIcon.global_position = maps[current_map].global_position

func _input(event):
	if move_tween and move_tween.is_running():
		return

	if event.is_action_pressed("ui_left") and current_map > 0:
		current_map -= 1
		tween_icon()
	
	if event.is_action_pressed("ui_right") and current_map < maps.size() - 1:
		current_map += 1
		tween_icon()

	if event.is_action_pressed("ui_accept"):
		print("Map clicked")

	'''
	if event.is_action_pressed("ui_accept"):
		if maps[current_map].map_selector_scene:
			get_tree().get_root().add_child(maps[current_map].map_selector_scene)
			get_tree().current_scene = maps[current_map].map_selector_scene
			get_tree().get_root().remove_child(self)
	'''

func tween_icon():
	move_tween = get_tree().create_tween()
	move_tween.tween_property($PlayerIcon, "global_position", maps[current_map].global_position, 0.5).set_trans(Tween.TRANS_SINE)
