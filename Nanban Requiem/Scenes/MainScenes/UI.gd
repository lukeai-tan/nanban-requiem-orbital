extends CanvasLayer

@onready var ui = get_node("HUD")
@onready var base_hp_bar = get_node("HUD/InfoBar/HBoxContainer/BaseHPBar")
@onready var tower_count = get_node("HUD/InfoBar/Tower Count")
@onready var build_bar = get_node("HUD/BuildBar")
@onready var quit_window = get_node("HUD/QuitWindow")

var tower_builder
var tower_manager
var color_change = 0


func set_tower_preview(tower_type, mouse_position):
	
	var base_sprite := Sprite2D.new()
	base_sprite.texture = load("res://Assets/Towers/" + tower_type + "_base.png")
	base_sprite.set_name("BaseSprite")
	
	var range_texture := Sprite2D.new()
	range_texture.texture = load("res://Assets/Towers/range_overlay.png")
	range_texture.set_name("RangeOverlay")
	range_texture.position = Vector2.ZERO

	var scaling : float = GameData.tower_data[tower_type]["range"] / 600.0
	range_texture.scale = Vector2(scaling, scaling)

	var control := Control.new()
	control.set_name("TowerPreview")
	control.set_position(mouse_position)

	control.add_child(range_texture)
	control.add_child(base_sprite)
	
	if tower_type.begins_with("RangedTower"):
		var texture_path: String = "res://Assets/Towers/" + tower_type + "_turret.png"
		
		if FileAccess.file_exists(texture_path):
			var turret_sprite := Sprite2D.new()
			turret_sprite.texture = load(texture_path)
			turret_sprite.name = "TurretSprite"
			control.add_child(turret_sprite)
		##else:
			##print("Error: Turret texture not found at path:", texture_path)

	add_child(control)
	move_child(control, 0)
	return control


func update_tower_preview(new_position, color):
	var preview := get_node("TowerPreview")
	preview.position = new_position
	
	var color_value := Color(color)
	
	if preview.has_node("BaseSprite"):
		var base_sprite := preview.get_node("BaseSprite") as Sprite2D
		base_sprite.texture_filter = CanvasItem.TEXTURE_FILTER_NEAREST
		if base_sprite.modulate != color_value:
			base_sprite.modulate = color_value
			
	if preview.has_node("TurretSprite"):
		var turret_sprite := preview.get_node("TurretSprite") as Sprite2D
		turret_sprite.modulate = color_value
		
	if preview.has_node("RangeOverlay"):
		var range_sprite := preview.get_node("RangeOverlay") as Sprite2D
		range_sprite.modulate = color_value
	
	
## Main Controls
# Pause/Play Button
func _on_pause_play_pressed() -> void:
	if tower_builder.build_mode:
		tower_builder.cancel_build_mode()
	if get_tree().is_paused():
		get_tree().paused = false
	else:
		get_tree().paused = true

# Fast Forward button (x1/x10)
func _on_fast_forward_pressed() -> void:
	if tower_builder.build_mode:
		tower_builder.cancel_build_mode()

	var game_speed = 2.0 # made this for debugging reasons, default max is 2.0
	if Engine.get_time_scale() == game_speed:
		Engine.set_time_scale(1.0)
		GameData.time_scale = 1.0
	else:
		Engine.set_time_scale(game_speed)
		GameData.time_scale = game_speed

# Restart current scene Button
func _on_restart_pressed() -> void:
	quit_window.visible = true


func update_health_bar(base_health):
	var hp_bar_tween = base_hp_bar.create_tween()
	hp_bar_tween.tween_property(base_hp_bar, "value", base_health, 0.1)
	update_color()


func update_color():
	var ratio : float = base_hp_bar.value / base_hp_bar.max_value;
	if color_change == 0 and ratio <= 0.5:
		base_hp_bar.tint_progress = Color("e1be32") # Orange
		color_change += 1
	elif color_change == 1 and ratio <= 0.25:
		base_hp_bar.tint_progress = Color("e11e1e") # Red


func set_hp(hp):
	base_hp_bar.max_value = hp
	base_hp_bar.value = hp
	base_hp_bar.tint_progress = Color("3cc510") # Green


func update_tower_count(current_count: int, max_count: int) -> void:
	tower_count.text = "%d / %d" % [current_count, max_count]

func enable_build_bar():
	build_bar.visible = true

func disable_build_bar():
	if tower_builder.build_mode:
		tower_builder.cancel_build_mode()
	build_bar.visible = false

func toggle_ui():
	ui.visible = !ui.visible


func _on_no_button_pressed() -> void:
	quit_window.visible = false


func _on_quit_button_pressed() -> void:
	quit_window.visible = false
	get_tree().paused = false
	get_tree().reload_current_scene()
	Engine.set_time_scale(1.0)
