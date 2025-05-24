extends CanvasLayer


func set_tower_preview(tower_type, mouse_position):
	'''
	#var drag_tower = load("res://Scenes/Towers/" + tower_type + ".tscn").instantiate()
	var drag_tower := Sprite2D.new()
	drag_tower.texture = load("res://Assets/Towers/" + tower_type + "_base.png")
	drag_tower.texture = load("res://Assets/Towers/" + tower_type + "_turret.png")
	drag_tower.set_name("DragTower")
	drag_tower.modulate = Color("ad54ff")
	
	var range_texture = Sprite2D.new()
	range_texture.position = Vector2.ZERO
	var scaling : float = GameData.tower_data[tower_type]["range"] / 600.0
	range_texture.scale = Vector2(scaling, scaling)
	var texture = load("res://Assets/Towers/range_overlay.png")
	range_texture.texture = texture
	range_texture.modulate = Color("ad54ff")
	
	var control = Control.new()
	control.add_child(drag_tower, true)
	control.add_child(range_texture, true)
	control.set_position(mouse_position)
	control.set_name("TowerPreview")
	add_child(control, true)
	move_child(get_node("TowerPreview"), 0) 
	'''
	var base_sprite := Sprite2D.new()
	base_sprite.texture = load("res://Assets/Towers/" + tower_type + "_base.png")

	var turret_sprite := Sprite2D.new()
	turret_sprite.texture = load("res://Assets/Towers/" + tower_type + "_turret.png")

	var range_texture := Sprite2D.new()
	range_texture.texture = load("res://Assets/Towers/range_overlay.png")
	range_texture.position = Vector2.ZERO

	var scaling : float = GameData.tower_data[tower_type]["range"] / 600.0
	range_texture.scale = Vector2(scaling, scaling)

	var control := Control.new()
	control.set_name("TowerPreview")
	control.set_position(mouse_position)

	control.add_child(range_texture)
	control.add_child(base_sprite)
	control.add_child(turret_sprite)

	add_child(control)
	move_child(control, 0)
	
	base_sprite.set_name("BaseSprite")
	turret_sprite.set_name("TurretSprite")
	range_texture.set_name("RangeOverlay")


func update_tower_preview(new_position, color):
	'''
	get_node("TowerPreview").set_position(new_position)
	if get_node("TowerPreview/DragTower").modulate != Color(color):
		get_node("TowerPreview/DragTower").modulate = Color(color)
		# range indicator (attack radius)
		get_node("TowerPreview/Sprite2D").modulate = Color(color)
	'''
	var preview := get_node("TowerPreview")
	preview.position = new_position
	
	var color_value := Color(color)
	
	if preview.has_node("BaseSprite"):
		var base_sprite := preview.get_node("BaseSprite") as Sprite2D
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
	if get_parent().build_mode:
		get_parent().cancel_build_mode()
	if get_tree().is_paused():
		get_tree().paused = false
	else:
		get_tree().paused = true

# Fast Forward button (x1/x10)
func _on_fast_forward_pressed() -> void:
	if get_parent().build_mode:
		get_parent().cancel_build_mode()
	if Engine.get_time_scale() == 10.0:
		Engine.set_time_scale(1.0)
	else:
		Engine.set_time_scale(10.0)

# Restart current scene Button
func _on_restart_pressed() -> void:
	get_tree().reload_current_scene()
