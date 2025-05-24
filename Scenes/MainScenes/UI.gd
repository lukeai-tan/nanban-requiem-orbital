extends CanvasLayer


func set_tower_preview(tower_type, mouse_position):
	var drag_tower = load("res://Scenes/Towers/" + tower_type + ".tscn").instantiate()
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


func update_tower_preview(new_position, color):
	get_node("TowerPreview").set_position(new_position)
	if get_node("TowerPreview/DragTower").modulate != Color(color):
		get_node("TowerPreview/DragTower").modulate = Color(color)
		# range indicator (attack radius)
		get_node("TowerPreview/Sprite2D").modulate = Color(color)


func _on_pause_play_pressed() -> void:
	if get_tree().is_paused():
		get_tree().paused = false
	else:
		get_tree().paused = true


func _on_fast_forward_pressed() -> void:
	if Engine.get_time_scale() == 10.0:
		Engine.set_time_scale(1.0)
	else:
		Engine.set_time_scale(10.0)
