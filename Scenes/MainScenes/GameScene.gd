extends Node2D

var map_node: Node2D
var build_mode: bool = false
var build_valid: bool = false
var build_tile
var build_location: Vector2
var build_type: String


# Called when the node enters the scene tree for the first time.
# Sets up the scene
func _ready():
	# Can turn this into a variable that changes based on selected map
	# For now we just set it to the only map we have
	map_node = get_node("Map1")
	for i in get_tree().get_nodes_in_group("tower_options"):
		# connects signal from pressing respective buttons to call initiate build mode
		# inputs the tower name associated with the button as the tower_type parameter
		i.pressed.connect(initiate_build_mode.bind(i.name))


# constantly update location of tower while in build mode
# location of tower is based on mouse location
# eg. when dragging it around the map
func _process(_delta: float):
	if build_mode:
		update_tower_preview()


# listen for mouse clicks
func _unhandled_input(event):
	if event.is_action_released("ui_cancel") and build_mode == true:
		cancel_build_mode()
	if event.is_action_released("ui_accept") and build_mode == true:
		verify_and_build()
		cancel_build_mode()


func initiate_build_mode(tower_type):
	# Base case where build mode is initiated during build mode
	if build_mode:
		cancel_build_mode()
		
	build_type = tower_type
	build_mode = true
	get_node("UI").set_tower_preview(build_type, get_global_mouse_position())


func update_tower_preview():
	var mouse_position = get_global_mouse_position()
	
	# tile layers that don't allow tower placements
	var tower_exclusions = map_node.get_node("TowerExclusions") # props
	var path_layer = map_node.get_node("Path") # path
	
	# check if current tile is from tower exclusion tilemaplayer
	var current_tile = tower_exclusions.local_to_map(mouse_position)
	var tile_position = tower_exclusions.map_to_local(current_tile)
	var invalid_by_exclusion : bool = tower_exclusions.get_cell_source_id(current_tile) != -1
	
	# check if current tile is from path tilemaplayer
	var path_tile = path_layer.local_to_map(mouse_position)
	var invalid_by_path : bool = path_layer.get_cell_source_id(path_tile) != -1

	if not invalid_by_exclusion and not invalid_by_path:
		get_node("UI").update_tower_preview(tile_position, "fff")
		build_valid = true 
		build_location = tile_position
		build_tile = current_tile
	else:
		get_node("UI").update_tower_preview(tile_position, "f00")
		build_valid = false


func cancel_build_mode():
	build_mode = false
	build_valid = false
	get_node("UI/TowerPreview").free()


# IF build location is valid, instantiate selected tower scene
# position it at stored tile
func verify_and_build():
	if build_valid:
		var new_tower = load("res://Scenes/Towers/" + build_type + ".tscn").instantiate()
		new_tower.position = build_location
		new_tower.built = true;
		map_node.get_node("Towers").add_child(new_tower, true)
		# create a dummy tile to act as the tower exclusion in the tower position
		map_node.get_node("TowerExclusions").set_cell(build_tile, 2, Vector2(1, 0))
