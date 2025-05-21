extends Node2D
'''
var map_node: Node2D
var build_mode: bool = false
var build_valid: bool = false
var build_location: Vector2
var build_type: String: Node2D

# Called when the node enters the scene tree for the first time.
# Sets up the scene
func _ready():
	# Can turn this into a variable that changes based on selected map
	# For now we just set it to the only map we have
	map_node = get_node("Map1")
	for i in get_tree().get_nodes_in_group("tower_buttons"):
		# connects signal from pressing respective buttons to call initiate build mode
		# inputs the tower name associated with the button as the tower_type parameter
		i.pressed.connect(inititate_build_mode.bind(i.name))

# constantly update location of tower while in build mode
# location of tower is based on mouse location
# eg. when dragging it around the map
func _process(delta: float):
	if build_mode:
		update_tower_preview()
	
# listen for mouse clicks
func _unhandled_input(event):
	# TODO
	pass
	
func initiate_build_mode(tower_type):
	# Base case where build mode is initiated during build mode
	if build_mode:
		return
		
	build_type = tower_type
	build_mode = true
	get_node("UI").set_tower_preview(build_type, get_global_mouse_position())

func update_tower_preview():
	# TODO
	pass

func cancel_build_mode():
	build_mode = false
	build_valid = false
	get_node("UI/TowerPreview").queue_free()

# IF build location is valid, instantiate selected tower scene
# position it at stored tile
func verify_and_build():
	# TODO
	pass
'''
