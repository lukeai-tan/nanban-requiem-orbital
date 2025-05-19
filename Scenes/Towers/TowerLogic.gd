extends Node2D

@onready var turret = $Turret 

func _physics_process(_delta):
	turn()

# rotation logic of turrets on Ranged towers
func turn():
	if turret:
		var target_pos = get_global_mouse_position()
		turret.rotation = (target_pos - turret.global_position).angle()
	else:
		print("No towers found")
