extends CanvasLayer

@onready var base_hp_bar = get_node("HUD/InfoBar/HBoxContainer/BaseHPBar")
var tower_builder

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
	if Engine.get_time_scale() == 2.0:
		Engine.set_time_scale(1.0)
	else:
		Engine.set_time_scale(2.0)

# Restart current scene Button
func _on_restart_pressed() -> void:
	get_tree().reload_current_scene()
	

func update_health_bar(base_health):
	var hp_bar_tween = base_hp_bar.create_tween()
	hp_bar_tween.tween_property(base_hp_bar, "value", base_health, 0.1)
	if base_health >= 5.0:
		base_hp_bar.tint_progress = Color("3cc510") # Green
	elif base_health <= 4.0 and base_health >= 2.0:
		base_hp_bar.tint_progress = Color("e1be32") # Orange
	else:
		base_hp_bar.tint_progress = Color("e11e1e") # Red
